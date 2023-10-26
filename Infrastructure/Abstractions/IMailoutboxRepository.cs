using Infrastructure.Repository;
using Infrastructure.DatabaseEntity;

namespace Infrastructure.Abstractions
{
    public interface IMailoutboxRepository : IGenericRepository<MailOutbox>
    {
        Task<EmailType> GetEmailType(string type);
        void InsertMailAttachment(List<MailOutboxAttachment> mailOutboxAttachments);
        void InsertMailRecipients(List<MailOutboxRecipient> mailOutboxRecipients);
    }
    public interface IMailoutboxRepositoryMailService : IMailoutboxRepository
    {
    }
}