using MailKit;
using MailKit.Net.Imap;
using MailKit.Net.Smtp;
using MailKit.Search;
using MimeKit;
using System.Collections.Concurrent;

namespace Infrastructure.Mail
{
    public interface IMailHandler
    {
        public MimeMessage CreateMimeMessage(InternetAddressList to, InternetAddressList cc, InternetAddressList bcc, MailboxAddress sender, string subject, string messageStr, AttachmentCollection embeddedHtmlContent, AttachmentCollection mailAttachments, bool messageStrIsHtml = false);
        public Task<Dictionary<UniqueId, MimeMessage>> DownloadMails(SearchQuery searchQuery, string folderName = null, string user = null, string password = null, ImapClient imapClient = null);
        public Task<List<UniqueId>> SearchInFolder(IMailFolder mailFolder, SearchQuery searchQuery, string user = null, string password = null, ImapClient imapClient = null);
        public Task<List<IMailFolder>> DeleteFolder(IMailFolder mailFolder, string user = null, string password = null, ImapClient imapClient = null);
        public Task<List<IMailFolder>> DeleteFolders(List<IMailFolder> mailFolder, string user = null, string password = null, ImapClient imapClient = null);
        public Task<List<UniqueId>> DeleteMail(IMailFolder mailFolder, UniqueId uniqueId, string user = null, string password = null, ImapClient imapClient = null);
        public Task<List<UniqueId>> DeleteMails(IMailFolder mailFolder, List<UniqueId> uniqueIds, string user = null, string password = null, ImapClient imapClient = null);
        public Task<MimeMessage> SendMail(MimeMessage mimeMessage, string user = null, string password = null, SmtpClient smtpClient = null);
        public Task<List<MimeMessage>> SendMails(List<MimeMessage> mimeMessages, string user = null, string password = null, SmtpClient smtpClient = null);
        public MimeMessage PrepareReply(MimeMessage message, string replyMessage, bool replyToAll, string customSubject = null, string user = null, string password = null);
        public Task<List<UniqueId>> MoveMails(IMailFolder source, List<UniqueId> uniqueIds, IMailFolder destination, string user = null, string password = null, ImapClient imapClient = null);
        public Task<List<UniqueId>> MarkMailsAs(IMailFolder mailFolder, List<UniqueId> uniqueIds, MessageFlags messageFlags, string user = null, string password = null, ImapClient imapClient = null);
        public Task<Dictionary<string, MimeEntity>> GetMimeMessageAttachements(string storeFolder, MimeMessage message);
        public Task<ImapClient> CreateImapConnection(string user = null, string password = null);
        public Task<SmtpClient> CreateSmtpConnection(string user = null, string password = null);
        public Task<IMailFolder> GetMailFolder(string folderName, string user = null, string password = null, ImapClient imapClient = null);
        public IMailFolder CurrentFolder { get; set; }
        public long GetMimePartSize(MimeEntity part);
        public byte[] GetAttachmentBinary(MimeEntity x);

        //MailQueuing
        public ConcurrentDictionary<string, ConcurrentQueue<MimeMessage>> Queue { get; }
        public void Enqueue(string key, MimeMessage mail);//add a MimeMessage to List by given key
        public MimeMessage Dequeue(string key);//returns the first item of List<MimeMessage> by given key and remove the item of list
        public MimeMessage Peek(string key);//return the first item of List<MimeMessage> by given key
    }
}
