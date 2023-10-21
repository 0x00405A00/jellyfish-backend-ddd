using Infrastructure.Abstractions;
using Infrastructure.Mail;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.IO;
using System.Text;

namespace Infrastructure.HostedService.Backgroundservice
{
    internal class MailHostedService : BackgroundService
    {
        internal static int MailSentIntervalInSeconds = 60;
        private readonly ILogger<MailHostedService> _logger;
        private readonly IMailHandler _mailHandler;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMailoutboxRepository _mailoutboxRepository;
        private readonly CancellationToken _cancelationToken;

        public MailHostedService(
            ILogger<MailHostedService> logger,
            IMailHandler mailHandler,
            IUnitOfWork unitOfWork,
            IMailoutboxRepository mailoutboxRepository)
        {
            _logger = logger;
            _mailHandler = mailHandler;
            this.unitOfWork = unitOfWork;
            this._mailoutboxRepository = mailoutboxRepository;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


            using PeriodicTimer timer = new(TimeSpan.FromSeconds(MailSentIntervalInSeconds));

            try
            {
                do
                {
                    await ExecuteTask();
                }
                while (await timer.WaitForNextTickAsync(stoppingToken));
            }
            catch (OperationCanceledException)
            {

            }
        }
        private async Task ExecuteTask()
        {
            var mails = await _mailoutboxRepository.ListAsync();
            if (mails.Count == 0)
                return;

            foreach(var mail in mails)
            {
                var mailIsHtml = Convert.ToBoolean(mail.BodyIsHtml);
                var from = new MailboxAddress(mail.From,mail.From);
                InternetAddressList recipientsToList = new InternetAddressList();
                InternetAddressList recipientsCcList = new InternetAddressList();
                InternetAddressList recipientsBccList = new InternetAddressList();

                mail.MailOutboxRecipients.ToList().ForEach(recipient =>
                {
                    switch(recipient.EmailTypeUu.Type)
                    {
                        case MailHandler.MailType.To:
                            recipientsToList.Add(MimeKit.InternetAddress.Parse(recipient.Email));
                            break;
                        case MailHandler.MailType.Cc:
                            recipientsCcList.Add(MimeKit.InternetAddress.Parse(recipient.Email));
                            break;
                        case MailHandler.MailType.Bcc:
                            recipientsBccList.Add(MimeKit.InternetAddress.Parse(recipient.Email));
                            break;
                    }
                });
                string subject = mail.Subject??String.Empty;
                string body = Encoding.UTF8.GetString(mail.Body??new byte[0]);
                MimeKit.AttachmentCollection mailAttachments = new MimeKit.AttachmentCollection();
                MimeKit.AttachmentCollection mailEmbeddedInHtmlMedia = new MimeKit.AttachmentCollection();
                foreach(var attachment in mail.MailOutboxAttachments)
                {
                    var isEmbedded = Convert.ToBoolean(attachment.IsEmbeddedInHtml);
                    string filePath = Path.Combine(Environment.CurrentDirectory,"Mail","Attachments", attachment.Filename);
                    await File.WriteAllBytesAsync(filePath, attachment.Attachment, _cancelationToken);
                    if (isEmbedded)
                    {
                        var builder = new BodyBuilder();
                        var image = builder.LinkedResources.Add(filePath);
                        image.ContentId = attachment.MimeCid;
                        mailEmbeddedInHtmlMedia.Add(image);
                    }
                    else
                    {
                        using (FileStream fs = File.OpenRead(filePath))
                        {
                            var mediaAttachent = new MimePart(attachment.MimeMediatype, attachment.MimeMediasubtype)
                            {

                                Content = new MimeContent(fs, ContentEncoding.Default),
                                ContentDisposition = new ContentDisposition(isEmbedded ? ContentDisposition.Inline : ContentDisposition.Attachment),
                                ContentTransferEncoding = ContentEncoding.Base64,
                                FileName = Path.GetFileName(attachment.Filename),
                                ContentId = attachment.MimeCid,
                            };
                            mailAttachments.Add(mediaAttachent);
                            fs.Close();
                        }

                    }
                }
                var mailMessage = _mailHandler.CreateMimeMessage(
                    recipientsToList,
                    recipientsCcList,
                    recipientsBccList,
                    from,
                    subject,
                    body,
                    mailEmbeddedInHtmlMedia,
                    mailAttachments,
                    mailIsHtml);
                await _mailHandler.SendMail(mailMessage);
                _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}-mailservice send mail");
                _mailoutboxRepository.Remove(mail);
            }
            await unitOfWork.SaveChangesAsync();
        }

    }
}
