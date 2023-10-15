using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;

namespace Infrastructure.Mapper.Concrete
{
    internal class MessageMapper : AbstractMapper<Domain.Entities.Message.Message, Message>
    {
        public override Message MapToDatabaseEntity(Domain.Entities.Message.Message entity, bool mapRelationObjects)
        {
            Message message = new Message();
            message.Uuid = entity.Uuid.Id;
            message.ChatUuid = entity.Chat.ToGuid();
            message.Text = entity.Text;
            message.BinaryContent = entity.MediaContent?.Data;
            message.CreatedTime = entity.CreatedTime;
            message.LastModifiedTime = entity.LastModifiedTime;
            message.DeletedTime = entity.DeletedTime;
            message.Deleted = Convert.ToSByte(entity.DeletedTime != DateTime.MinValue);
            message.MessageOwner = entity.Owner.Uuid.Id;
            return message;
        }

        public override Domain.Entities.Message.Message MapToDomainEntity(Message entity, bool withRelations)
        {
            if (entity == null)
                return null;
            var messageId = entity.Uuid.ToIdentification<Domain.Entities.Message.MessageId>();
            Domain.Entities.Chats.ChatId chatId = null;
            Domain.Entities.User.User user= null;
            if (withRelations)
            {
                chatId = new Domain.Entities.Chats.ChatId(entity.ChatUu.Uuid);
                user = entity.MessageOwnerNavigation.MapToDomainEntity<Domain.Entities.User.User, User>(false);
            }
            var mediaContent = MediaContent.Parse(entity.BinaryContent);

            return Domain.Entities.Message.Message.Create(
                messageId,
                chatId,
                user,
                entity.Text,
                mediaContent,
                (DateTime)entity.CreatedTime!,
                entity.LastModifiedTime,
                entity.DeletedTime);
        }
    }
}
