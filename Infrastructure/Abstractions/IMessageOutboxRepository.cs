using Infrastructure.Repository;
using Domain.Entities.Messages;

namespace Infrastructure.Abstractions
{
    public interface IMessageOutboxRepository : IGenericRepository<MessageOutbox>
    {

    }
}
