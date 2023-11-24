using Domain.Primitives;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Infrastructure.FileSys;
using Shared.ApiDataTransferObject;

namespace Infrastructure.Mapper.Concrete
{
    internal class ChatMapper : AbstractMapper<Domain.Entities.Chats.Chat, Chat>
    {
        public override async Task<Chat> MapToDatabaseEntity(Domain.Entities.Chats.Chat entity, bool mapRelationObjects)
        {
            if (entity == null)
                return null;

            Chat chat = new Chat();
            chat.Uuid = entity.Uuid.ToGuid();
            chat.Name = entity.ChatName;
            chat.Description = entity.ChatDescription;
            chat.CreatedTime =entity.CreatedTime;
            chat.LastModifiedTime=entity.LastModifiedTime;
            chat.DeletedTime =entity.DeletedTime;
            chat.OwnerUserUuid =entity.CreatedByUser.Uuid.ToGuid();
            if(mapRelationObjects)
            {
                entity.Members.ToList()
                              .ForEach(async(member) =>
                              {
                                  var memberDatabaseEntity = await member.User.MapToDatabaseEntity<Domain.Entities.User.User, User>(false);

                                  ChatRelationToUser chatRelationToUser = new ChatRelationToUser();
                                  chatRelationToUser.ChatUuid = chat.Uuid;
                                  chatRelationToUser.UserUuid = memberDatabaseEntity.Uuid;
                                  chatRelationToUser.CreatedTime = chat.CreatedTime;
                                  chatRelationToUser.LastModifiedTime = chat.LastModifiedTime;
                                  chatRelationToUser.DeletedTime = chat.DeletedTime;
                                  chatRelationToUser.IsChatAdmin = Convert.ToSByte(entity.Admins.Contains(member));
                                  chat.ChatRelationToUsers.Add(chatRelationToUser);

                              });
                entity.Messages?.ToList().ForEach(async(message) =>
                {
                    var messageDatabaseEntity = await message.MapToDatabaseEntity<Domain.Entities.Message.Message, Message>(false);

                    chat.Messages.Add(messageDatabaseEntity);
                });
            }
            return chat;    
        }

        public override async Task<Domain.Entities.Chats.Chat> MapToDomainEntity(Chat entity, bool withRelations)
        {
            if (entity == null)
                return null;
            var chatId = entity.Uuid.ToIdentification<Domain.Entities.Chats.ChatId>();
            var ownerUserEntity = await entity.OwnerUserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);
            var chatMembers = entity.ChatRelationToUsers.Select(async (userRelation) => {
                var user = await userRelation.UserUu.MapToDomainEntity<Domain.Entities.User.User, User>(false);

                bool isChatAdmin = Convert.ToBoolean(userRelation.IsChatAdmin);
                return ChatMember.Create(user, isChatAdmin, userRelation.CreatedTime ?? DateTime.MinValue, userRelation.LastModifiedTime, userRelation.DeletedTime);
            }).ToList();
            var chatMessages = await entity.Messages.MapToDomainEntity<Domain.Entities.Message.Message, Message>(true);
            var picture = Picture.Parse(entity.ChatPicturePath,entity.ChatPictureFileExt);
            try
            {
                var pic = await ((MediaContent)picture).LoadMediaContent(CancellationToken.None);
                picture.SetBinary(pic);
            }
            catch (Exception ex)
            {

            }
            var chatMembersList = await Task.WhenAll(chatMembers);
            return Domain.Entities.Chats.Chat.Create(
                chatId,
                ownerUserEntity,
                entity.Name,
                entity.Description,
                picture,
                chatMembersList?.ToList(),
                chatMessages,
                (DateTime)entity.CreatedTime!,
                entity.LastModifiedTime,
                entity.DeletedTime);
        }
    }
}
