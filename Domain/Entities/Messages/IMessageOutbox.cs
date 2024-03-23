using Domain.Entities.Users;
using Domain.Primitives.Ids;

namespace Domain.Entities.Messages
{
    public interface IMessageOutbox
    {
        Message Message { get; }
        MessageId MessageForeignKey { get; }
        User User { get; }
        UserId UserForeignKey { get; }
    }
}
