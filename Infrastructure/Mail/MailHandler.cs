using Infrastructure.Security;
using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Collections.Concurrent;

namespace Infrastructure.Mail
{
    public class MailHandler : IMailHandler
    {
        public struct MailConfigurationKeys
        {
            public const string Section = "Mail";
            public const string SystemDeliveryAddress = "system_sender_anonymous_mail";
            public struct Imap
            {
                public const string Section = $"{MailConfigurationKeys.Section}:IMAP";
                public static Properties Properties = new Properties();
            }
            public struct Smtp
            {
                public const string Section = $"{MailConfigurationKeys.Section}:SMTP";
                public static Properties Properties = new Properties();
            }

            public class Properties
            {
                public readonly string User = "user";
                public readonly string Password = "password";
                public readonly string Server = "server";
                public readonly string Port = "port";
                public readonly string SecureSocketOptions = "secure_socket_options";
                public readonly string TimeoutMs = "timeout_ms";
                public readonly string LogFile = "logger_file_name";
            }
        }
        private ConcurrentDictionary<string, ConcurrentQueue<MimeMessage>> _queue = new ConcurrentDictionary<string, ConcurrentQueue<MimeMessage>>();

        public ConcurrentDictionary<string, ConcurrentQueue<MimeMessage>> Queue
        {
            get
            {
                return _queue;
            }
        }

        public void Enqueue(string key, MimeMessage mail)//add a MimeMessage to List by given key
        {
            if (!_queue.ContainsKey(key))
            {
                _queue.TryAdd(key, new ConcurrentQueue<MimeMessage>());
            }
            _queue[key].Enqueue(mail);
        }

        public MimeMessage Dequeue(string key)//returns the first item of List<MimeMessage> by given key and remove the item of list
        {
            if (_queue.ContainsKey(key))
            {
                _queue[key].TryDequeue(out MimeMessage mail);
                return mail;
            }
            return null;
        }
        public MimeMessage Peek(string key)//return the first item of List<MimeMessage> by given key
        {
            if (_queue.ContainsKey(key))
            {
                _queue[key].TryPeek(out MimeMessage mail);
                return mail;
            }
            return null;
        }

        private IMailFolder _currentFolder = null;
        public readonly string ServerImap;
        public readonly int PortImap;
        public readonly SecureSocketOptions SecureSocketOptionsImap;
        protected string UserImap;
        protected string PasswordImap;
        public readonly int TimeoutImap;
        public readonly string LoggerFileImapFolder;
        public static string LoggerFileNamePostfixImap = "imap.log";
        public string LoggerFileImapPath
        {
            get
            {
                string fileName = string.Format("{0}-{1}.{2}", DateTime.UtcNow.Year, DateTime.UtcNow.Month, LoggerFileNamePostfixImap);
                return Path.Combine(LoggerFileImapFolder, fileName);
            }
        }

        public readonly string ServerSmtp;
        public readonly int PortSmtp;
        public readonly SecureSocketOptions SecureSocketOptionsSmtp;
        protected string UserSmtp;
        protected string PasswordSmtp;
        public readonly int TimeoutSmtp;
        public readonly string LoggerFileSmtpFolder;
        public static string LoggerFilePostfixSmtp = "smtp.log";
        public string LoggerFileSmtpPath
        {
            get
            {
                string fileName = string.Format("{0}-{1}.{2}", DateTime.UtcNow.Year, DateTime.UtcNow.Month, LoggerFilePostfixSmtp);
                return Path.Combine(LoggerFileSmtpFolder, fileName);
            }
        }

        private readonly ILogger<MailHandler> _logger;

        public IMailFolder CurrentFolder
        {
            get
            {
                return _currentFolder;
            }
            set
            {
                _currentFolder = value;
            }
        }

        public MailHandler(ILogger<MailHandler> logger, IConfiguration configuration)
        {
           
            var configImap = configuration.GetSection(MailHandler.MailConfigurationKeys.Imap.Section);
            var configSmtp = configuration.GetSection(MailHandler.MailConfigurationKeys.Smtp.Section);

            #region IMAP
            UserImap = configImap.GetValue<string>(MailHandler.MailConfigurationKeys.Imap.Properties.User);
            PasswordImap = configImap.GetValue<string>(MailHandler.MailConfigurationKeys.Imap.Properties.Password);
            ServerImap = configImap.GetValue<string>(MailHandler.MailConfigurationKeys.Imap.Properties.Server);
            PortImap = configImap.GetValue<int>(MailHandler.MailConfigurationKeys.Imap.Properties.Port);
            SecureSocketOptionsImap = configImap.GetValue<SecureSocketOptions>(MailHandler.MailConfigurationKeys.Imap.Properties.SecureSocketOptions);
            TimeoutImap = configImap.GetValue<int>(MailHandler.MailConfigurationKeys.Imap.Properties.TimeoutMs);
            LoggerFileImapFolder = configImap.GetValue<string>(MailHandler.MailConfigurationKeys.Imap.Properties.LogFile);
            #endregion

            #region SMTP
            UserSmtp = configSmtp.GetValue<string>(MailHandler.MailConfigurationKeys.Smtp.Properties.User);
            PasswordSmtp = configSmtp.GetValue<string>(MailHandler.MailConfigurationKeys.Smtp.Properties.Password);
            ServerSmtp = configSmtp.GetValue<string>(MailHandler.MailConfigurationKeys.Smtp.Properties.Server);
            PortSmtp = configSmtp.GetValue<int>(MailHandler.MailConfigurationKeys.Smtp.Properties.Port);
            SecureSocketOptionsSmtp = configSmtp.GetValue<SecureSocketOptions>(MailHandler.MailConfigurationKeys.Smtp.Properties.SecureSocketOptions);
            TimeoutSmtp = configSmtp.GetValue<int>(MailHandler.MailConfigurationKeys.Smtp.Properties.TimeoutMs);
            LoggerFileSmtpFolder = configSmtp.GetValue<string>(MailHandler.MailConfigurationKeys.Smtp.Properties.LogFile);
            #endregion

            _logger = logger;
            Init();
        }

        private async void Init()
        {
            _logger.LogInformation("mailhandler init, smtp & imap services are avaible when are configured");

        }
        public byte[] GetAttachmentBinary(MimeEntity x)
        {
            using (var measure = new MemoryStream())
            {
                if (x is MessagePart)
                {
                    var rfc822 = (MessagePart)x;
                    rfc822.Message.WriteTo(measure);
                }
                else if (x is MimePart)
                {
                    var part = (MimePart)x;
                    part.Content.DecodeTo(measure);
                }
                else if (x is Multipart)
                {
                    var part = (Multipart)x;
                    var bodyContent = part;

                }
                byte[] response = measure.ToArray();
                return response;
            }
        }
        /// <summary>
        /// Returns the Size in kB from a given MimeEntity
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public long GetMimePartSize(MimeEntity x)
        {
            var binary = GetAttachmentBinary(x);
            return binary.Length / 1000;
        }

        public async Task<Dictionary<string, MimeEntity>> GetMimeMessageAttachements(string storeFolder, MimeMessage message)
        {
            Dictionary<string, MimeEntity> response = new Dictionary<string, MimeEntity>();
            foreach (var x in message.Attachments.ToList())
            {
                using (EncryptionHandler encryp = new EncryptionHandler())
                {

                    string fileName = "tmp_" + (x.ContentDisposition?.FileName ?? x.ContentType.Name ?? "" + DateTime.UtcNow.Ticks);

                    string path = Path.Combine(storeFolder, fileName);
                    using (FileStream stream = File.Create(path))
                    {
                        if (x is MessagePart)
                        {
                            var rfc822 = (MessagePart)x;

                            rfc822.Message.WriteTo(stream);
                        }
                        else
                        {
                            var part = (MimePart)x;

                            part.Content.DecodeTo(stream);
                        }
                    }
                    var s = File.OpenRead(path);

                    string hash = await encryp.MD5Async(s);
                    if (!response.ContainsKey(hash))
                        response.Add(path, x);

                    s.Close();
                    s.Dispose();
                }
            }
            return response;
        }
        public async Task<IMailFolder> GetMailFolder(string folderName, string user = null, string password = null, ImapClient imapClient = null)
        {
            bool wasNotInit = imapClient != null;
            imapClient = wasNotInit ? imapClient : await CreateImapConnection(user, password);
            IMailFolder mailFolder = await imapClient.GetFolderAsync(folderName);

            return mailFolder;
        }
        public async Task<Dictionary<UniqueId, MimeMessage>> DownloadMails(SearchQuery searchQuery, string folderName = null, string user = null, string password = null, ImapClient imapClient = null)
        {
            bool wasNotInit = imapClient != null;
            Dictionary<UniqueId, MimeMessage> response = new Dictionary<UniqueId, MimeMessage>();
            try
            {
                imapClient = wasNotInit ? imapClient : await CreateImapConnection(user, password);
                IMailFolder mailFolder = folderName == null ?
                        imapClient.Inbox : await imapClient.GetFolderAsync(folderName);

                FolderAccess acces = await mailFolder.OpenAsync(FolderAccess.ReadOnly);
                CurrentFolder = mailFolder;

                IList<UniqueId> uids = await mailFolder.SearchAsync(searchQuery);
                /*System.Collections.Concurrent.BlockingCollection<UniqueId> threadSafeList = new System.Collections.Concurrent.BlockingCollection<UniqueId>();
                uids.ToList().ForEach(x => threadSafeList.Add(x));
                threadSafeList..ForEach(async (x) => response.Add(x, await Utils.CallAsyncFunc<UniqueId, MimeMessage>(x, async (x) => await mailFolder.GetMessageAsync(x))));
                */
                foreach (UniqueId uid in uids)
                {
                    var mime = await mailFolder.GetMessageAsync(uid);
                    response.Add(uid, mime);
                }
                await mailFolder.CloseAsync();
                if (!wasNotInit)
                    await imapClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {

            }
            return response;
        }

        public async Task<List<UniqueId>> SearchInFolder(IMailFolder mailFolder, SearchQuery searchQuery, string user = null, string password = null, ImapClient imapClient = null)
        {
            List<UniqueId> response = new List<UniqueId>();
            bool wasNotInit = imapClient != null;
            imapClient = wasNotInit ? imapClient : await CreateImapConnection(user, password);
            FolderAccess acces = await mailFolder.OpenAsync(FolderAccess.ReadOnly);

            IList<UniqueId> uids = await mailFolder.SearchAsync(searchQuery);
            await mailFolder.CloseAsync();
            if (!wasNotInit)
                await imapClient.DisconnectAsync(true);
            return response;
        }
        public async Task<List<IMailFolder>> DeleteFolder(IMailFolder mailFolder, string user = null, string password = null, ImapClient imapClient = null)
        {
            return await DeleteFolders(new List<IMailFolder> { mailFolder }, user, password, imapClient);
        }
        public async Task<List<IMailFolder>> DeleteFolders(List<IMailFolder> mailFolder, string user = null, string password = null, ImapClient imapClient = null)
        {
            bool wasNotInit = imapClient != null;
            imapClient = wasNotInit ? imapClient : await CreateImapConnection(user, password);
            mailFolder.ForEach(async (x) =>
            {
                try
                {
                    FolderAccess folderAccessSource = await x.OpenAsync(FolderAccess.ReadWrite);
                    await x.DeleteAsync();
                    mailFolder.Remove(x);
                    x.Close();
                }
                catch (Exception ex)
                {

                }

            });
            if (!wasNotInit)
                await imapClient.DisconnectAsync(true);
            return mailFolder;
        }
        public async Task<List<UniqueId>> DeleteMail(IMailFolder mailFolder, UniqueId uniqueId, string user = null, string password = null, ImapClient imapClient = null)
        {

            return await MarkMailsAs(mailFolder, new List<UniqueId> { uniqueId }, MessageFlags.Deleted, user, password, imapClient);
        }
        public async Task<List<UniqueId>> DeleteMails(IMailFolder mailFolder, List<UniqueId> uniqueIds, string user = null, string password = null, ImapClient imapClient = null)
        {

            return await MarkMailsAs(mailFolder, uniqueIds, MessageFlags.Deleted, user, password, imapClient);
        }
        public async Task<MimeMessage> SendMail(MimeMessage mimeMessage, string user = null, string password = null, SmtpClient smtpClient = null)
        {
            List<MimeMessage> messages = await SendMails(new List<MimeMessage> { mimeMessage }, user, password, smtpClient);
            return messages == null || messages.Count == 0 ? null : messages[0];
        }
        public async Task<List<MimeMessage>> SendMails(List<MimeMessage> mimeMessages, string user = null, string password = null, SmtpClient smtpClient = null)
        {
            bool wasNotInit = smtpClient != null;
            try
            {
                smtpClient = wasNotInit ? smtpClient : await CreateSmtpConnection(user, password);
                if (smtpClient == null)
                    return null;
                mimeMessages.ForEach((x) =>
                {
                    try
                    {
                        smtpClient.Send(x);
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }

                });
                if (!wasNotInit)
                    await smtpClient.DisconnectAsync(true);
            }
            catch (Exception ex)
            {
                throw;
            }
            return mimeMessages;
        }
        public MimeMessage CreateMimeMessage(InternetAddressList to, InternetAddressList cc, InternetAddressList bcc, MailboxAddress sender, string subject, string messageStr,AttachmentCollection embeddedHtmlContent,AttachmentCollection mailAttachments,bool messageStrIsHtml =false)
        {
            MimeMessage message = new MimeMessage();
            if (to.Count > 0)
            {
                message.To.AddRange(to);
            }
            if (cc != null && cc.Count > 0)
            {
                message.Cc.AddRange(cc);
            }
            if (bcc != null && bcc.Count > 0)
            {
                message.Bcc.AddRange(bcc);
            }
            if (sender != null)
            {
                message.From.Add(sender);
            }
            else
                throw new Exception();
            message.Sender = sender;

            message.Subject = subject;

            var builder = new BodyBuilder();
            if(embeddedHtmlContent!=null)
            {
                foreach(var media in embeddedHtmlContent)
                {
                    builder.LinkedResources.Add(media);
                }
            }
            // Set the plain-text version of the message text
            builder.TextBody = messageStr;
           
            if(messageStrIsHtml)
            {
                builder.HtmlBody = messageStr;
            }
            if (mailAttachments != null)
            {
                foreach (var media in mailAttachments)
                {
                    builder.Attachments.Add(media);
                }
            }
            /*var pathImage = Path.Combine(@"C:\Users\Mika\Desktop\jellyfish_image.svg");
            var image = builder.LinkedResources.Add(pathImage);
            image.ContentId = "xyzmyid";
            builder.HtmlBody += string.Format(@"<p>Hey!</p><img src=""cid:{0}"">", image.ContentId);*/
            message.Body = builder.ToMessageBody();
            return message;
        }
        public MimeMessage PrepareReply(MimeMessage message, string replyMessage, bool replyToAll, string customSubject = null, string user = null, string password = null)
        {
            MimeMessage reply = new MimeMessage();
            // reply to the sender of the message
            if (message.ReplyTo.Count > 0)
            {
                reply.To.AddRange(message.ReplyTo);
            }
            else if (message.From.Count > 0)
            {
                reply.To.AddRange(message.From);
            }
            else if (message.Sender != null)
            {
                reply.To.Add(message.Sender);
            }

            if (replyToAll)
            {
                reply.To.AddRange(message.To);
                reply.Cc.AddRange(message.Cc);
            }
            if (customSubject == null)
            {
                if (!message.Subject.StartsWith("Aw:", StringComparison.OrdinalIgnoreCase))
                    reply.Subject = "Aw:" + message.Subject;
                else
                    reply.Subject = message.Subject;
            }
            else
            {
                reply.Subject = customSubject;
            }

            if (!string.IsNullOrEmpty(message.MessageId))
            {
                reply.InReplyTo = message.MessageId;
                foreach (var id in message.References)
                    reply.References.Add(id);
                reply.References.Add(message.MessageId);
            }

            using (var quoted = new StringWriter())
            {
                var sender = message.Sender ?? message.From.Mailboxes.FirstOrDefault();
                quoted.Write(replyMessage + "\n");
                quoted.WriteLine("Am {0}, {1} schrieb:", message.Date.ToString("f"), !string.IsNullOrEmpty(sender.Name) ? sender.Name : sender.Address);
                using (var reader = new StringReader(message.TextBody))
                {
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        quoted.Write("> ");
                        quoted.WriteLine(line);
                    }
                }

                reply.Body = new TextPart("plain")
                {
                    Text = quoted.ToString()
                };
            }
            return reply;
        }
        public async Task<List<UniqueId>> MoveMails(IMailFolder source, List<UniqueId> uniqueIds, IMailFolder destination, string user = null, string password = null, ImapClient imapClient = null)
        {
            bool wasNotInit = imapClient != null;
            List<UniqueId> response = new List<UniqueId>();
            imapClient = wasNotInit ? imapClient : await CreateImapConnection(user, password);
            FolderAccess folderAccessSource = await source.OpenAsync(FolderAccess.ReadWrite);
            //FolderAccess folderAccessDest = await destination.OpenAsync(FolderAccess.ReadWrite);
            foreach (UniqueId uniqueId in uniqueIds)
            {
                try
                {
                    UniqueId? tmp = await source.MoveToAsync(uniqueId, destination);
                    response.Add(tmp ?? throw new Exception());
                }
                catch (Exception ex)
                {

                }
            }
            await source.CloseAsync();
            //destination.Close();
            if (!wasNotInit)
                await imapClient.DisconnectAsync(true);
            return response;
        }
        public async Task<List<UniqueId>> MarkMailsAs(IMailFolder mailFolder, List<UniqueId> uniqueIds, MessageFlags messageFlags, string user = null, string password = null, ImapClient imapClient = null)
        {
            bool wasNotInit = imapClient != null;
            imapClient = wasNotInit ? imapClient : await CreateImapConnection(user, password);
            FolderAccess folderAccessDest = await mailFolder.OpenAsync(FolderAccess.ReadWrite);
            uniqueIds.ForEach(async (x) =>
            {
                try
                {
                    await mailFolder.AddFlagsAsync(x, messageFlags, true);
                    uniqueIds.Remove(x);//als response der funktion werden alle  zurückgegeben wo es nicht geklappt hat
                }
                catch (Exception ex)
                {

                }

            });
            await mailFolder.CloseAsync();
            if (!wasNotInit)
                await imapClient.DisconnectAsync(true);
            return uniqueIds;
        }

        public async Task<ImapClient> CreateImapConnection(string user, string password)
        {
            try
            {

                ImapClient imapClient = null;
                try
                {
                    if (!Directory.Exists(LoggerFileImapFolder))
                    {
                        Directory.CreateDirectory(LoggerFileImapFolder);
                    }
                    imapClient = new ImapClient(new ProtocolLogger(LoggerFileImapPath, true));
                }
                catch (Exception ex)
                {
                    FileStream fileStream = File.Create(LoggerFileImapPath);
                    imapClient = new ImapClient(new ProtocolLogger(fileStream, true));
                }

                imapClient.Timeout = TimeoutImap;
                UserImap = UserImap ?? user;
                PasswordImap = PasswordImap ?? password;
                await imapClient.ConnectAsync(ServerImap, PortImap, SecureSocketOptionsImap);
                await imapClient.AuthenticateAsync(UserImap, PasswordImap);
                return imapClient;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<SmtpClient> CreateSmtpConnection(string user, string password)
        {
            try
            {
                SmtpClient smtpClient = null;
                try
                {
                    if (!Directory.Exists(LoggerFileSmtpFolder))
                    {
                        Directory.CreateDirectory(LoggerFileSmtpFolder);
                    }
                    smtpClient = new SmtpClient();
                }
                catch (Exception ex)
                {
                    DateTime dateTime = DateTime.UtcNow;
                    FileStream fileStream = File.Create(LoggerFileSmtpPath);
                    smtpClient = new SmtpClient(new ProtocolLogger(fileStream, true));
                }
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtpClient.Timeout = TimeoutSmtp;

                UserSmtp = UserSmtp ?? user;
                PasswordSmtp = PasswordSmtp ?? password;
                smtpClient.ServerCertificateValidationCallback = (s, c, h, e) => true;
                await smtpClient.ConnectAsync(ServerSmtp, PortSmtp, SecureSocketOptions.SslOnConnect);
                await smtpClient.AuthenticateAsync(UserSmtp, PasswordSmtp);
                return smtpClient;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


    }
    public static class MailHandlerExtension
    {
        public static void Reply(this MimeMessage message, SmtpClient smtpClient, MimeMessage reply)
        {
            using (smtpClient)
            {
                smtpClient.Send(reply);
                smtpClient.Disconnect(true);
            }
        }
    }
}
