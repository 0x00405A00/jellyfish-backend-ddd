using Application.Abstractions.Messaging;
using Domain.Const;
using Domain.Entities.Mails;
using Domain.Entities.Users.Events;
using Domain.Extension;
using Domain.Primitives.Ids;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.Mail;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MimeKit.Utils;
using System.Data.Common;
using System.Text;

namespace Application.CQS.User.EventHandler
{
    internal sealed class UserCreatedDomainEventHandler :
        INotificationHandler<UserCreatedDomainEvent>
    {
        private readonly IServiceProvider serviceProvider;

        public UserCreatedDomainEventHandler(
            IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public async Task Handle(UserCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            using(var scope = serviceProvider.CreateAsyncScope())
            {
                var mailoutboxRepository = scope.ServiceProvider.GetRequiredService<IMailoutboxRepository>();
                var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
                var userRepository = scope.ServiceProvider.GetRequiredService<IUserRepository>();
                var emailTypeRepository = scope.ServiceProvider.GetRequiredService<IEmailTypeRepository>();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var user = await userRepository.GetAsync(x=>x.Id == notification.UserId);

                var mailSection = configuration.GetSection(MailHandler.MailConfigurationKeys.Section);
                var userSection = configuration.GetSection("Infrastructure:User:Registration");
                var activationLink = string.Format("{0}{1}", userSection.GetValue<string>("account_activation_link"), user.ActivationToken);

                var mailSender = mailSection.GetValue<string>(MailHandler.MailConfigurationKeys.SystemDeliveryAddress);
                Email systemEmail = Email.Parse(mailSender);
                var mailOutboxId = MailOutbox.NewId();
                var imageUuid = Guid.NewGuid();

                var imageJellyfishPath = Path.Combine(Environment.CurrentDirectory, "Media", "Static", "jellyfish_image.png");

                MailOutboxAttachment jellyfishIcon = MailOutboxAttachment.Create(
                    id: MailOutboxAttachment.NewId(), // Annahme: Generierung einer eindeutigen GUID
                    mailOutboxId: mailOutboxId,
                    mimeMediaType: "image",
                    mimeMediaSubType: "png",
                    fileName: "jellyfish_image.png",
                    mimeCid: MimeUtils.GenerateMessageId(),
                    order: 1,
                    attachmentPath: imageJellyfishPath,
                    attachmentSha1: "xyz",
                    isEmbededInHtml: true,
                    createdDateTime: DateTime.UtcNow.ToTypedDateTime(),// Annahme: Aktuelles Datum und Uhrzeit
                    modifiedDateTime: null,
                    deletedDateTime: null
                );


                var emailType = await emailTypeRepository.GetAsync(x => x.Id == EmailConst.Type.To.ToIdentification<EmailTypeId>());
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
                                        <h1>Willkommen bei Jellyfish!</h1>
                                        <p>Vielen Dank, dass du dich registriert hast. Wir freuen uns, dich als Teil unserer Jellyfish-Community begrüßen zu dürfen.</p>
                                        <p>Um alle Funktionen nutzen zu können musst du deinen Account erst einmal aktivieren.</p>
                                        <a alt=""{0}"" href=""{0}"" class=""cta-button"">Hier geht es zur Akitivierung</a>
                                        <p>
                                            <img class=""footer-img"" alt=""{2}"" src=""cid:{1}"">
                                        </p>
                                    </div>
                                </body>
                                </html>",
                activationLink, jellyfishIcon.MimeCid, jellyfishIcon.Filename);

                var body = Encoding.UTF8.GetBytes(bodyHtml);
                var systemUser = Domain.Entities.Users.User.GetSystemUser();
                string subject = @"Willkommen bei Jellyfish " + user.UserName + " \U0001F44B";
                bool bodyIsHtml = true;

                var recipient = MailOutboxRecipient.Create(
                    MailOutboxRecipient.NewId(),
                    mailOutboxId,
                    emailType.Id,
                    user.Email.EmailValue,
                    createdDateTime: DateTime.UtcNow.ToTypedDateTime(),// Annahme: Aktuelles Datum und Uhrzeit
                    modifiedDateTime: null,
                    deletedDateTime: null);

                List<MailOutboxRecipient> recipients = new List<MailOutboxRecipient>()
                {
                    recipient
                };
                List<MailOutboxAttachment> attachments = new List<MailOutboxAttachment>()
                {
                    jellyfishIcon
                };
                var mail = MailOutbox.Create(
                    mailOutboxId,
                    systemEmail.EmailValue,
                    subject,
                    bodyHtml,
                    bodyIsHtml,
                    DateTime.UtcNow.ToTypedDateTime(),
                    null,
                    null);

                foreach (var item in recipients)
                {
                    mail.AddRecipient(item);
                }
                foreach (var item in attachments)
                {
                    mail.AddAttachment(item);
                }
                mailoutboxRepository.Add(mail);
                await unitOfWork.SaveChangesAsync();
            }

        }
    }
}
