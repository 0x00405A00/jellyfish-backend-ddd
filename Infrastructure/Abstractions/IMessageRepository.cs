using Infrastructure.Repository;

namespace Infrastructure.Abstractions
{
    public interface IMessageRepository : IGenericRepository<Domain.Entities.Message.Message, Infrastructure.DatabaseEntity.Message>
    {

    }
}
