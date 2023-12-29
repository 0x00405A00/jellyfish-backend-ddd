using Infrastructure.Repository;
using Domain.Entities.Mails;

namespace Infrastructure.Abstractions
{
    public interface IMailoutboxRepository : IGenericRepository<MailOutbox>
    {

    }
    public interface IMailoutboxRepositoryMailService : IMailoutboxRepository
    {
    }
}