using Infrastructure.Repository;
using Domain.Entities.Messages;

namespace Infrastructure.Abstractions
{
    public interface IMessageRepository : IGenericRepository<Message>
    {

    }
}
