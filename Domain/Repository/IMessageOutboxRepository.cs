using Domain.Entities.Messages;

namespace Domain.Repository
{
    public interface IMessageOutboxRepository : IGenericRepository<MessageOutbox>, IPessimisticLockingRepository<MessageOutbox>
    {

    }
}
