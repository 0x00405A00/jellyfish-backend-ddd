using Infrastructure.Repository;
using Domain.Entities.Message;

namespace Infrastructure.Abstractions
{
    public interface IMessageRepository : IGenericRepository<Message>
    {

    }
}
