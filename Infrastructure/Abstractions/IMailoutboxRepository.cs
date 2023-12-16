using Infrastructure.Repository;
using Domain.Entities.MailoutBox;

namespace Infrastructure.Abstractions
{
    public interface IMailoutboxRepository : IGenericRepository<MailOutbox>
    {

    }
    public interface IMailoutboxRepositoryMailService : IMailoutboxRepository
    {
    }
}