using Domain.Const;
using Domain.Entities.Mails;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.Mail;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MimeKit;
using Shared.Logger;
using System.Data;
using System.Linq.Expressions;

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
                _logger.LogDebug($"operation canceled");
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
                using var transaction = dbContext.Database.BeginTransaction();
                {
                    try
                    {
                        var mails = await mailoutboxRepository.ListWithPessimisticLockAsync();

                        if (mails.Count == 0)
                        {
                            _logger.LogDebug("no mails available to send");
                            return;
                        }

                        _logger.LogDebug("delivery: prepare delivery for {Count}",mails.Count);
                        foreach (var mail in mails)
                        {
                            _logger.LogDebugForObject<MailOutbox>(x=>x.Id.ToString(), "delivery:preparation:start:", mail);
                            var mailIsHtml = Convert.ToBoolean(mail.IsBodyHtml);
                            var from = new MailboxAddress(mail.From, mail.From);
                            InternetAddressList recipientsToList, recipientsCcList, recipientsBccList;
                            recipientsToList = recipientsCcList = recipientsBccList = new();

                            SetRecipients(mail.Recipients, ref recipientsToList, ref recipientsCcList, ref recipientsBccList);
                            _logger.LogDebug("delivery: set recipients");

                            string subject = mail.Subject ?? String.Empty;
                            string body = mail.Body;
                            MimeKit.AttachmentCollection mailAttachments = new MimeKit.AttachmentCollection();
                            MimeKit.AttachmentCollection mailEmbeddedInHtmlMedia = new MimeKit.AttachmentCollection();
                            
                            SetAttachments(mail.Attachments,ref mailAttachments, ref mailEmbeddedInHtmlMedia);
                            _logger.LogDebug("delivery: set attachments");

                            _logger.LogDebug("delivery: start to create mime-message");
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
                            _logger.LogDebug("delivery: mime-message created");

                            await mailHandler.SendMail(mailMessage);
                            _logger.LogInformation($"delivery: {DateTime.UtcNow.ToLongTimeString()}-mailservice send mail with id {mail.Id}");
                            mailoutboxRepository.Remove(mail);
                            _logger.LogInformation($"delivery: remove mail with id {mail.Id} from repository");
                        }

                        _logger.LogDebug($"start to save changes");
                        await unitOfWork.SaveChangesAsync();
                        _logger.LogDebug($"save changes successfull");
                        _logger.LogDebug($"start committing");
                        transaction.Commit();
                        _logger.LogDebug($"committing successfull");
                    }
                    catch(DBConcurrencyException ex)
                    {

                        _logger.LogException(ex);
                        transaction.Rollback();
                        _logger.LogDebug($"rollback");
                    }
                    catch(Exception ex)
                    {
                        _logger.LogException(ex);
                        transaction.Rollback();
                        _logger.LogDebug($"rollback");
                    }

                    
                }
                
            }
        }

        private bool SetAttachments(IReadOnlyCollection<MailOutboxAttachment> mailAttachments,ref AttachmentCollection attachments, ref AttachmentCollection attachmentsEmbeded)
        {
            foreach (var attachment in mailAttachments)
            {
                var isEmbedded = Convert.ToBoolean(attachment.IsEmbededInHtml);
                string filePath = attachment.AttachmentPath;
                if (isEmbedded)
                {
                    var builder = new BodyBuilder();
                    var image = builder.LinkedResources.Add(filePath);
                    image.ContentId = attachment.MimeCid;
                    attachmentsEmbeded.Add(image);
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
                        attachments.Add(mediaAttachent);
                        fs.Close();
                    }

                }
            }
            return true;
        }

        private bool SetRecipients(IReadOnlyCollection<MailOutboxRecipient> recipients, ref InternetAddressList toList, ref InternetAddressList ccList, ref InternetAddressList bccList)
        {

            foreach (var recipient in recipients)
            {
                if (recipient.IsReceiver())
                {
                    toList.Add(MimeKit.InternetAddress.Parse(recipient.Email));
                }
                else if (recipient.IsCopy())
                {
                    ccList.Add(MimeKit.InternetAddress.Parse(recipient.Email));
                }
                else if (recipient.IsBlindCopy())
                {
                    bccList.Add(MimeKit.InternetAddress.Parse(recipient.Email));
                }
            }
            return true;
        }

    }
}
