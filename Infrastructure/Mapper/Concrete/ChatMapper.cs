using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;

namespace Infrastructure.Mapper.Concrete
{
    internal class ChatMapper : AbstractMapper<Domain.Entities.Chats.Chat, Chat>
    {
        public override Chat MapToDatabaseEntity(Domain.Entities.Chats.Chat entity, bool mapRelationObjects)
        {
            if (entity == null)
                return null;

            Chat chat = new Chat();
            chat.Uuid = entity.Uuid.ToGuid();
            chat.Name = entity.ChatName;
            chat.Description = entity.ChatDescription;
            chat.Picture =null;
            chat.CreatedTime =entity.CreatedTime;
            chat.LastModifiedTime=entity.LastModifiedTime;
            chat.DeletedTime =entity.DeletedTime;
            chat.Deleted = Convert.ToSByte(entity.DeletedTime != null && entity.DeletedTime != DateTime.MinValue);
            chat.OwnerUserUuid =entity.CreatedByUser.Uuid.ToGuid();
            if(mapRelationObjects)
            {
                entity.Members.ToList()
                              .ForEach(member =>
                              {
                                  var memberDatabaseEntity = member.User.MapToDatabaseEntity<Domain.Entities.User.User, User>(false);

                                  ChatRelationToUser chatRelationToUser = new ChatRelationToUser();
                                  chatRelationToUser.ChatUu = chat;
                                  chatRelationToUser.UserUu = memberDatabaseEntity;
                                  chatRelationToUser.CreatedTime = chat.CreatedTime;
                                  chatRelationToUser.LastModifiedTime = chat.LastModifiedTime;
                                  chatRelationToUser.DeletedTime = chat.DeletedTime;
                                  chatRelationToUser.Deleted = Convert.ToSByte(chat.DeletedTime != null && chat.DeletedTime != DateTime.MinValue);
                                  chatRelationToUser.IsChatAdmin = Convert.ToSByte(entity.Admins.Contains(member));
                                  chat.ChatRelationToUsers.Add(chatRelationToUser);

                              });
                entity.Messages?.ToList().ForEach(message =>
                {
                    var messageDatabaseEntity = message.MapToDatabaseEntity<Domain.Entities.Message.Message, Message>(false);

                    chat.Messages.Add(messageDatabaseEntity);
                });
            }
            return chat;    
        }

        public override Domain.Entities.Chats.Chat MapToDomainEntity(Chat entity, bool withRelations)
        {
            if (entity == null)
                return null;
            var chatId = entity.Uuid.ToIdentification<Domain.Entities.Chats.ChatId>();
            var ownerUserEntity = entity.OwnerUserUu.MapToDomainEntity<Domain.Entities.User.User,User>(false);
            var chatMembers = entity.ChatRelationToUsers.Select(userRelation => {
                var user = userRelation.UserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);

                bool isChatAdmin = Convert.ToBoolean(userRelation.IsChatAdmin);
                return ChatMember.Create(user, isChatAdmin, userRelation.CreatedTime ?? DateTime.MinValue, userRelation.LastModifiedTime, userRelation.DeletedTime);
            })
                                                      .ToList();
            var chatMessages = entity.Messages.MapToDomainEntity<Domain.Entities.Message.Message, Message>(false);
            var picture = Picture.Parse(entity.Picture);

            return Domain.Entities.Chats.Chat.Create(
                chatId,
                ownerUserEntity,
                entity.Name,
                entity.Description,
                picture,
                chatMembers,
                chatMessages,
                (DateTime)entity.CreatedTime!,
                entity.LastModifiedTime,
                entity.DeletedTime);
        }
    }
}
