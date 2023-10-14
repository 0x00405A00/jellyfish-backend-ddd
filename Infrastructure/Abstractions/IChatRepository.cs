using Infrastructure.DatabaseEntity;
using Infrastructure.Repository;

namespace Infrastructure.Abstractions
{
    public interface IChatRepository : IGenericRepository<Domain.Entities.Chats.Chat, Chat>
    {

    }
}
