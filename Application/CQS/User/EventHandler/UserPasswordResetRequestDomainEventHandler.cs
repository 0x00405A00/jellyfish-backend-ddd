using Domain.Entities.User.Event;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Infrastructure.Mail;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MimeKit.Utils;
using System.Security.Policy;
using System.Text;

namespace Application.CQS.User.EventHandler
{
    internal sealed class UserPasswordResetRequestDomainEventHandler :
        INotificationHandler<UserPasswordResetRequestDomainEvent>
    {
        private readonly IServiceProvider serviceProvider;

        public UserPasswordResetRequestDomainEventHandler(
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task Handle(UserPasswordResetRequestDomainEvent notification, CancellationToken cancellationToken)
        {
            using(var scope = serviceProvider.CreateScope())
            {

                var mailoutboxRepository = scope.ServiceProvider.GetRequiredService<IMailoutboxRepository>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var userSection = configuration.GetSection("Infrastructure:User:PasswordReset");
                var mailSection = configuration.GetSection("Infrastructure:Mail");

                var resetPasswordBaseUrl = userSection.GetValue<string>("reset_password_link");
                var mailSender = mailSection.GetValue<string>("system_sender_anonymous_mail");
                var mailUuid = Guid.NewGuid();

                var imageJellyfishPath = Path.Combine(Environment.CurrentDirectory, "Media", "Static", "jellyfish_image.png");

                string resetUrl = $"{resetPasswordBaseUrl}{notification.e.PasswordResetToken}";

                MailOutboxAttachment jellyfishIcon = new MailOutboxAttachment
                {
                    MimeMediatype = "image",
                    MimeMediasubtype = "png",
                    MailUuid = mailUuid,
                    AttachmentPath = imageJellyfishPath,
                    AttachmentSha1 = "xyz",
                    Filename = "jellyfish_image.png",
                    Order = 1,
                    Uuid = Guid.NewGuid(),
                    IsEmbeddedInHtml = Convert.ToSByte(true),
                    MimeCid = MimeUtils.GenerateMessageId()
                };

                var emailType = await mailoutboxRepository.GetEmailType(MailHandler.MailType.To);
                string bodyHtml = string.Format(@"
                                <!DOCTYPE html>
                                <html lang=""de"">
                                <head>
                                    <meta charset=""UTF-8"">
                                    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
                                    <title>Password-Reset</title>
                                    <style>
                                        body {{
                                            font-family: Arial, sans-serif;
                                            background-color: #f2f2f2;
                                            margin: 0;
                                            padding: 0;
                                        }}
                                        .container {{
                                            max-width: 600px;
                                            margin: 0 auto;
                                            padding: 20px;
                                            background-color: #fff;
                                            box-shadow: 0 4px 6px rgba(0, 0, 0, 0.1);
                                            border-radius: 8px;
                                            margin-top: 20px;
                                        }}
                                        h1 {{
                                            color: #333;
                                        }}
                                        p {{
                                            color: #666;
                                        }}
                                        .cta-button {{
                                            display: inline-block;
                                            padding: 10px 20px;
                                            background: linear-gradient(to right, #9f61a6, #d05ea1);
                                            color: #fff;
                                            text-decoration: none;
                                            border-radius: 5px;
                                            margin-top: 20px;
                                        }}
                                        .footer-img {{
                                            width: 50%;
                                            height: 50%;
                                        }}
                                        .appstore-img {{
                                            width: 50%;
                                            height: 25%;
                                        }}
                                        .inline-order {{
                                            display: inline;
                                        }}
                                    </style>
                                </head>
                                <body>
                                    <div class=""container"">
                                        <h1>In wenigen Schritten hast du wieder Zugang zu Jellyfish!</h1>
                                        <p>Es scheint so als wenn du dein Passwort zurücksetzen möchtest?</p>
                                        <p>Sollte dem nicht so sein ignoriere dies Mail!</p>
                                        <p></p>
                                        <p>Klicke auf diesen Link um dein Passwort zurückzusetzen: {2}</p>
                                        <p>Code: {3}</p>
                                        <p>
                                            <img class=""footer-img"" alt=""{0}"" src=""cid:{1}"">
                                        </p>
                                    </div>
                                </body>
                                </html>",
                jellyfishIcon.Filename, jellyfishIcon.MimeCid, resetUrl, notification.e.PasswordResetCode);


                using (var transaction = await unitOfWork.BeginTransaction())
                {
                    try
                    {

                        var recipients = new List<MailOutboxRecipient>()
                        {
                            new MailOutboxRecipient
                            {
                                CreatedTime = DateTime.Now,
                                Email=notification.e.Email.EmailValue,
                                MailUuid=mailUuid,
                                EmailTypeUuid=emailType.Uuid
                            }
                        };
                        var attachments = new List<MailOutboxAttachment>() {
                                    jellyfishIcon
                                };
                        var body = Encoding.UTF8.GetBytes(bodyHtml);
                        var mail = new MailOutbox
                        {
                            Uuid = mailUuid,
                            CreatedTime = DateTime.Now,
                            From = mailSender,
                            Subject = @"Zurücksetzen deines Passworts bei Jellyfish " + notification.e.UserName+"",
                            Body = body,
                            BodyIsHtml = Convert.ToSByte(true),
                            MailOutboxAttachments = attachments,
                            MailOutboxRecipients = recipients,
                        };
                        await mailoutboxRepository.AddAsync(mail);
                        mail.MailOutboxAttachments.ToList().ForEach(y => System.Diagnostics.Debug.WriteLine(y.Uuid));
                        await unitOfWork.SaveChangesAsync();
                        transaction.Commit();
                    }
                    catch(Exception ex)
                    {
                        transaction.Rollback();
                    }

                }

            }

        }
    }
}
