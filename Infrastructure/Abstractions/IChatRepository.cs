using Domain.Entities.Chats;
using Infrastructure.Repository;

namespace Infrastructure.Abstractions
{
    public interface IChatRepository : IGenericRepository<Chat>
    {

    }
}
