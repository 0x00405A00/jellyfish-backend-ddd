using Domain.Entities.Mails;
using Domain.Primitives;
using System.Linq.Expressions;

namespace Domain.Repository
{
    public interface IMailoutboxRepository : IGenericRepository<MailOutbox>, IPessimisticLockingRepository<MailOutbox>
    {

    }
    public interface IMailoutboxRepositoryMailService : IMailoutboxRepository
    {
    }
}