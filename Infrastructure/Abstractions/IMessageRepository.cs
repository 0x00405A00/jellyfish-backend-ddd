using Infrastructure.DatabaseEntity;

namespace Infrastructure.Abstractions
{
    public interface IMessageRepository
    {
        void Insert(Message product);
        Message Get(Guid id);
        void Update(Message product);
    }
}
