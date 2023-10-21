using Domain.Entities.User.Event;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Infrastructure.Mail;
using MediatR;
using Microsoft.Extensions.Configuration;
using MimeKit.Utils;
using System.Configuration;
using System.Text;

namespace Application.CQS.User.EventHandler
{
    internal sealed class UserActivatedDomainEventHandler :
        INotificationHandler<UserActivatedDomainEvent>
    {
        private readonly IConfiguration configuration;
        private readonly IMailoutboxRepositoryScoped mailoutboxRepository;

        public UserActivatedDomainEventHandler(
            IConfiguration configuration,
            IMailoutboxRepositoryScoped mailoutboxRepository)
        {
            this.configuration = configuration;
            this.mailoutboxRepository = mailoutboxRepository;
        }

        public async Task Handle(UserActivatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var mailSection = configuration.GetSection("Infrastructure:Mail");

            var mailSender = mailSection.GetValue<string>("system_sender_anonymous_mail");
            var mailUuid = Guid.NewGuid();

            var imageJellyfish = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "Media", "Static", "jellyfish_image.png"));
            var imagePlaystore = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "Media", "Static", "playstore_icon.png"));
            var imageAppstore = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory, "Media", "Static", "appstore_icon.png"));

            MailOutboxAttachment jellyfishIcon = new MailOutboxAttachment
            {
                MimeMediatype = "image",
                MimeMediasubtype = "png",
                MailUuid = mailUuid,
                Attachment = imageJellyfish,
                AttachmentSha1 = "xyz",
                Filename = "jellyfish_image.png",
                Order = 0,
                Uuid = Guid.NewGuid(),
                IsEmbeddedInHtml = Convert.ToSByte(true),
                MimeCid = MimeUtils.GenerateMessageId()
            };
            MailOutboxAttachment appStore = new MailOutboxAttachment
            {
                MimeMediatype = "image",
                MimeMediasubtype = "png",
                MailUuid = mailUuid,
                Attachment = imageAppstore,
                AttachmentSha1 = "xyz",
                Filename = "appstore_icon.png",
                Order = 1,
                Uuid = Guid.NewGuid(),
                IsEmbeddedInHtml = Convert.ToSByte(true),
                MimeCid = MimeUtils.GenerateMessageId()
            };
            MailOutboxAttachment playStore = new MailOutboxAttachment
            {
                MimeMediatype = "image",
                MimeMediasubtype = "png",
                MailUuid = mailUuid,
                Attachment = imageAppstore,
                AttachmentSha1 = "xyz",
                Filename = "playstore_icon.png",
                Order = 2,
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
                                    <title>Da bist du ja!</title>
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
                                    </style>
                                </head>
                                <body>
                                    <div class=""container"">
                                        <h1>Du hast die Registrierung abgeschlossen!</h1>
                                        <p>Lad dir jetzt die Jellyfish runter. Egal ob Android oder IOS.</p>
                                        <p>
                                            <img class=""footer-img"" alt=""{4}"" src=""cid:{5}"">
                                        </p>
                                        <p>
                                            <img class=""footer-img"" alt=""{2}"" src=""cid:{3}"">
                                        </p>
                                        <p>
                                            <img class=""footer-img"" alt=""{0}"" src=""cid:{1}"">
                                        </p>
                                    </div>
                                </body>
                                </html>",
            jellyfishIcon.Filename, jellyfishIcon.MimeCid, appStore.Filename,appStore.MimeCid, playStore.Filename, playStore.MimeCid);
            
            var body = Encoding.UTF8.GetBytes(bodyHtml);
            var mail = new MailOutbox
            {
                Uuid = mailUuid,
                CreatedTime = DateTime.Now,
                From = mailSender,
                Subject = @"Registrierung abgeschlossen "+notification.e.UserName + ", Jellyfish im vollem Umfang nutzen \u2764",
                Body = body,
                BodyIsHtml = Convert.ToSByte(true),
                MailOutboxRecipients = new List<MailOutboxRecipient>()
                {
                    new MailOutboxRecipient 
                    { 
                        CreatedTime = DateTime.Now, 
                        Email="mika_roos@web.de", 
                        MailUuid=mailUuid, 
                        EmailTypeUuid=emailType.Uuid 
                    }
                },
                MailOutboxAttachments = new List<MailOutboxAttachment>()
                {
                }

            };
            mail.MailOutboxAttachments.Add(playStore);
            mail.MailOutboxAttachments.Add(appStore);
            mail.MailOutboxAttachments.Add(jellyfishIcon);
            mailoutboxRepository.Add(mail);

        }
    }
}
