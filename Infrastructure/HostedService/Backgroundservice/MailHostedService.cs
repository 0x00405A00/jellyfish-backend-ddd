using Domain.Const;
using Infrastructure.Abstractions;
using Infrastructure.Mail;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;

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
                    var mailIsHtml = Convert.ToBoolean(mail.IsBodyHtml);
                    var from = new MailboxAddress(mail.From, mail.From);
                    InternetAddressList recipientsToList = new InternetAddressList();
                    InternetAddressList recipientsCcList = new InternetAddressList();
                    InternetAddressList recipientsBccList = new InternetAddressList();

                    foreach (var recipient in mail.Recipients)
                    {
                        if(recipient.IsReceiver())
                        {
                            recipientsToList.Add(MimeKit.InternetAddress.Parse(recipient.Email));
                        }
                        else if(recipient.IsCopy())
                        {
                            recipientsCcList.Add(MimeKit.InternetAddress.Parse(recipient.Email));
                        }
                        else if(recipient.IsBlindCopy())
                        {
                            recipientsBccList.Add(MimeKit.InternetAddress.Parse(recipient.Email));
                        }
                    }
                    string subject = mail.Subject ?? String.Empty;
                    string body = mail.Body;
                    MimeKit.AttachmentCollection mailAttachments = new MimeKit.AttachmentCollection();
                    MimeKit.AttachmentCollection mailEmbeddedInHtmlMedia = new MimeKit.AttachmentCollection();
                    foreach (var attachment in mail.Attachments)
                    {
                        var isEmbedded = Convert.ToBoolean(attachment.IsEmbededInHtml);
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
                                var mediaAttachent = new MimePart(attachment.MimeMediaType, attachment.MimeMediaSubType)
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
                    _logger.LogInformation($"{DateTime.UtcNow.ToLongTimeString()}-mailservice send mail");
                    mailoutboxRepository.Remove(mail);
                }
                await unitOfWork.SaveChangesAsync();
            }
        }

    }
}
