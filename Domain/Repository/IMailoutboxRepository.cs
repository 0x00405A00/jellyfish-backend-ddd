using Domain.Entities.Mails;

namespace Domain.Repository
{
    public interface IMailoutboxRepository : IGenericRepository<MailOutbox>
    {

    }
    public interface IMailoutboxRepositoryMailService : IMailoutboxRepository
    {
    }
}