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
    internal sealed class UserCreatedDomainEventHandler :
        INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IConfiguration configuration;
        private readonly IMailoutboxRepository mailoutboxRepository;

        public UserCreatedDomainEventHandler(
            IConfiguration configuration,
            IMailoutboxRepository mailoutboxRepository)
        {
            this.configuration = configuration;
            this.mailoutboxRepository = mailoutboxRepository;
        }

        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            var mailSection = configuration.GetSection("Infrastructure:Mail");
            var mailSender = mailSection.GetValue<string>("system_sender_anonymous_mail");
            var mailUuid = Guid.NewGuid();
            var imageUuid = Guid.NewGuid();

            var image = File.ReadAllBytes(Path.Combine(Environment.CurrentDirectory,"Media","Static","jellyfish_image.svg"));//nur für test

            MailOutboxAttachment jellyfishIcon = new MailOutboxAttachment
            {
                MimeMediatype = "image",
                MimeMediasubtype = "svg+xml",
                MailUuid = mailUuid,
                Attachment = image,
                AttachmentSha1 ="xyz",
                Filename = "jellyfish_image.svg",
                Order = 0,
                Uuid=imageUuid,
                IsEmbeddedInHtml=Convert.ToSByte(true),
                MimeCid= MimeUtils.GenerateMessageId()
            };

            var emailType = await mailoutboxRepository.GetEmailType(MailHandler.MailType.To);
            string bodyHtml = string.Format(@"
<!DOCTYPE html>
<html lang=""de"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Willkommen bei Jellyfish</title>
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
            background-color: #d05ea1;
            color: #fff;
            text-decoration: none;
            border-radius: 5px;
            margin-top: 20px;
        }}
    </style>
</head>
<body>
    <div class=""container"">
        <h1>Willkommen bei Jellyfish!</h1>
        <p>Vielen Dank, dass du dich registriert hast. Wir freuen uns, dich als Teil unserer Jellyfish-Community begrüßen zu dürfen.</p>
        <p>Um alle Funktionen nutzen zu können musst du deinen Account erst einmal aktivieren.</p>
        <a alt=""{0}"" href=""{0}"" class=""cta-button"">Hier geht es zur Akitivierung</a>
        <p>
            <img alt=""{2}"" src=""cid:{1}"">
        </p>
    </div>
</body>
</html>", "www", jellyfishIcon.MimeCid,jellyfishIcon.Filename);
            
            var body = Encoding.UTF8.GetBytes(bodyHtml);
            var mail = new MailOutbox
            {
                Uuid = mailUuid,
                CreatedTime = DateTime.Now,
                From = mailSender,
                Subject = @"Willkommen bei Jellyfish "+notification.e.UserName + "\U0001F44B",// 
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
                    jellyfishIcon
                }
            };
            mailoutboxRepository.Add(mail);

        }
    }
}
