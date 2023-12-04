using Domain.Extension;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Infrastructure.FileSys;

namespace Infrastructure.Mapper.Concrete
{
    internal class MessageMapper : AbstractMapper<Domain.Entities.Message.Message, Message>
    {
        public override async Task<Message> MapToDatabaseEntity(Domain.Entities.Message.Message entity, bool mapRelationObjects)
        {
            Message message = new Message();
            message.Uuid = entity.Uuid.Id;
            message.ChatUuid = entity.Chat.ToGuid();
            message.Text = entity.Text;
            message.BinaryContentPath = entity.MediaContent?.FilePath;
            message.BinaryContentFileExt = entity.MediaContent?.FileExtension;
            message.CreatedTime = entity.CreatedTime;
            message.LastModifiedTime = entity.LastModifiedTime;
            message.DeletedTime = entity.DeletedTime;
            message.MessageOwner = entity.Owner.Uuid.Id;
            return message;
        }

        public override async Task<Domain.Entities.Message.Message> MapToDomainEntity(Message entity, bool withRelations)
        {
            if (entity == null)
                return null;
            var messageId = entity.Uuid.ToIdentification<Domain.Entities.Message.MessageId>();
            Domain.Entities.Chats.ChatId chatId = null;
            Domain.Entities.User.User user= null;
            if (withRelations)
            {
                chatId = new Domain.Entities.Chats.ChatId(entity.ChatUu.Uuid);
                user = await entity.MessageOwnerNavigation.MapToDomainEntity<Domain.Entities.User.User, User>(false);
            }
            var mediaContent = MediaContent.Parse(entity.BinaryContentPath,entity.BinaryContentFileExt);
            try
            {
                var pic = await mediaContent.LoadMediaContent(CancellationToken.None);
                mediaContent.SetBinary(pic);
            }
            catch (Exception ex)
            {

            }

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
