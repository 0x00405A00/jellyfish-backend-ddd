using Infrastructure.Abstractions;
using Infrastructure.Mail;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        internal static int MailSentIntervalInSeconds = 3*60;
        private readonly IServiceProvider serviceProvider;
        private readonly ILogger<MailHostedService> _logger;
        private readonly CancellationToken _cancelationToken;

        public MailHostedService(
            IServiceProvider serviceProvider,
            ILogger<MailHostedService> logger)
        {
            this.serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {


            using PeriodicTimer timer = new(TimeSpan.FromSeconds(MailSentIntervalInSeconds));

            try
            {
                do
                {
                    await TExecuteTask();
                }
                while (await timer.WaitForNextTickAsync(stoppingToken));
            }
            catch (OperationCanceledException)
            {

            }
        }
        private async Task TExecuteTask()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContextMailService>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWorkMailService>();
                var mailoutboxRepository = scope.ServiceProvider.GetRequiredService<IMailoutboxRepositoryMailService>();
                var mailHandler = scope.ServiceProvider.GetRequiredService<IMailHandler>();

                var mails = await mailoutboxRepository.ListAsync();
                if (mails.Count == 0)
                    return;

                foreach (var mail in mails)
                {
                    var mailIsHtml = Convert.ToBoolean(mail.BodyIsHtml);
                    var from = new MailboxAddress(mail.From, mail.From);
                    InternetAddressList recipientsToList = new InternetAddressList();
                    InternetAddressList recipientsCcList = new InternetAddressList();
                    InternetAddressList recipientsBccList = new InternetAddressList();

                    foreach (var recipient in mail.MailOutboxRecipients)
                    {

                        switch (recipient.EmailTypeUu.Type)
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
                    }
                    string subject = mail.Subject ?? String.Empty;
                    string body = Encoding.UTF8.GetString(mail.Body ?? new byte[0]);
                    MimeKit.AttachmentCollection mailAttachments = new MimeKit.AttachmentCollection();
                    MimeKit.AttachmentCollection mailEmbeddedInHtmlMedia = new MimeKit.AttachmentCollection();
                    foreach (var attachment in mail.MailOutboxAttachments)
                    {
                        var isEmbedded = Convert.ToBoolean(attachment.IsEmbeddedInHtml);
                        string filePath = attachment.AttachmentPath;
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
                    var mailMessage = mailHandler.CreateMimeMessage(
                        recipientsToList,
                        recipientsCcList,
                        recipientsBccList,
                        from,
                        subject,
                        body,
                        mailEmbeddedInHtmlMedia,
                        mailAttachments,
                        mailIsHtml);
                    await mailHandler.SendMail(mailMessage);
                    _logger.LogInformation($"{DateTime.Now.ToLongTimeString()}-mailservice send mail");
                    mailoutboxRepository.Remove(mail);
                }
                await unitOfWork.SaveChangesAsync();
            }
        }

    }
}
