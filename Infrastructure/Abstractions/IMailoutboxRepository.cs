using Infrastructure.Repository;
using Infrastructure.DatabaseEntity;

namespace Infrastructure.Abstractions
{
    public interface IMailoutboxRepository : IGenericRepository<MailOutbox>
    {
        Task<EmailType> GetEmailType(string type);
    }
}