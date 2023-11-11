using MobileApp.Data.SqlLite.Schema;
using MobileApp.Handler.Data.InternalDataInterceptor.Abstraction;
using Shared.DataTransferObject.Messenger;

namespace MobileApp.Handler.Data.InternalDataInterceptor.Invoker
{
    public class SqlLiteDatabaseHandlerInvoker : IInternalDataInterceptorApplicationInvoker
    {
        private readonly JellyfishSqlliteDatabaseHandler _jellyfishSqlliteDatabaseHandler;
        public SqlLiteDatabaseHandlerInvoker(JellyfishSqlliteDatabaseHandler jellyfishSqlliteDatabaseHandler) 
        {
            _jellyfishSqlliteDatabaseHandler = jellyfishSqlliteDatabaseHandler; 
        }
        public Task CreateFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveAcceptFriendRequest(params MessengerUserDTO[] data)
        {
            throw new NotImplementedException();
        }

        public Task ReceiveFriendRequest(params UserFriendshipRequestDTO[] data)
        {
            throw new NotImplementedException();
        }

        public async Task ReceiveMessage(params MessageDTO[] data)
        {
            foreach (var dataItem in data)
            {
                try
                {
                    //var maxChatId = await _jellyfishSqlliteDatabaseHandler.Max<ChatEntity,int>(x=>x.ChatId);
                    //var maxUserId = await _jellyfishSqlliteDatabaseHandler.Max<UserEntity, int>(x => x.UserId);
                    //var maxMessageId = await _jellyfishSqlliteDatabaseHandler.Max<MessageEntity, int>(x => x.MessageId);
                    //Check if user exists already, if not insert user in SqlLite
                    var searchUserCondition = new UserEntity { UserUuidValue = dataItem.MessageOwner };
                    var searchUserFunc = new Func<UserEntity, bool>((searchUserCondition) => {
                        if (searchUserCondition.UserUuidValue == dataItem.MessageOwner)
                            return true;
                        return false;
                    });
                    List<UserEntity> userEntities = (await _jellyfishSqlliteDatabaseHandler.Select<UserEntity>(searchUserFunc));
                    if (userEntities != null && userEntities.Count() > 1)
                    {
                        throw new InvalidDataException();
                    }
                    UserEntity userEntity = new UserEntity
                    {
                        NickName = dataItem.MessageOwner.ToString(),
                        //UserId = maxUserId + 1,
                        UserUuidValue = dataItem.MessageOwner,
                    };
                    if (userEntities == null||!userEntities.Any())
                    {
                        var chatInsResponse = await _jellyfishSqlliteDatabaseHandler.Insert<UserEntity>(userEntity);
                    }
                    userEntity = (await _jellyfishSqlliteDatabaseHandler.Select<UserEntity>(searchUserFunc))?.ToList()?.First();




                    //Check if chat exists already, if not insert chat in SqlLite
                    var searchChatCondition = new ChatEntity { ChatUuidValue = dataItem.ChatUuid };
                    var searchChatFunc = new Func<ChatEntity, bool>((searchChatCondition) => {
                        if (searchChatCondition.ChatUuidValue == dataItem.ChatUuid)
                            return true;
                        return false;
                    });
                    List<ChatEntity> chatEntities = (await _jellyfishSqlliteDatabaseHandler.Select<ChatEntity>(searchChatFunc));
                    if (chatEntities != null && chatEntities.Count() > 1)
                    {
                        throw new InvalidDataException();
                    }
                    ChatEntity chatEntity = new ChatEntity
                    {
                        //ChatId = maxChatId + 1,
                        ChatUuidValue = dataItem.ChatUuid,
                        ChatName = dataItem.ChatUuid.ToString(),
                    };
                    if (chatEntities == null|| !chatEntities.Any())
                    {
                        var chatInsResponse = await _jellyfishSqlliteDatabaseHandler.Insert<ChatEntity>(chatEntity);
                    }
                    chatEntity = (await _jellyfishSqlliteDatabaseHandler.Select<ChatEntity>(searchChatFunc))?.ToList()?.First();

                    var insertChatRelationToUserResponse = await _jellyfishSqlliteDatabaseHandler.Insert<UserLinkChatEntity>(new UserLinkChatEntity { ChatId = chatEntity.ChatId, UserId = userEntity.UserId });


                    //check if message exists already in sqllite, if not than add
                    var searchMessageCondition = new MessageEntity {MessageUuidValue = dataItem.Uuid };
                    var searchMessageFunc = new Func<MessageEntity, bool>((searchMessageCondition) => {
                        if (searchMessageCondition.MessageUuidValue == dataItem.Uuid)
                            return true;
                        return false;
                    });
                    List<MessageEntity> messageEntities = (await _jellyfishSqlliteDatabaseHandler.Select<MessageEntity>(searchMessageFunc));
                    if(messageEntities==null|| !messageEntities.Any())
                    {
                        var messagEntity = new MessageEntity(chatEntity.ChatId,userEntity.UserId,dataItem);
                        //messagEntity.MessageId = maxUserId + 1;
                        var msgInsResponse = await _jellyfishSqlliteDatabaseHandler.Insert<MessageEntity>(messagEntity);
                    }
                    //var ttt2 = await _jellyfishSqlliteDatabaseHandler.Select<ChatEntity>();
                   // var ttt3 = await _jellyfishSqlliteDatabaseHandler.Select<UserEntity>();
                    //var ttt4 = (await _jellyfishSqlliteDatabaseHandler.Select<MessageEntity>()).ToList();
                    //Guid werden nicht richtig von sqllite gespeichert --> möglichkeit auf valueconverter für property prüfen --> FIX: DB Col als String und Hidden Prop in Entity class
                    //var res = await _jellyfishSqlliteDatabaseHandler.DatabaseHandle.ExecuteScalarAsync<byte[]>("select hex(ChatUuid) from chat where ChatId=" + chatEntity.ChatId+"");
                }
                catch (Exception ex)
                {

                }

            }
        }

        public Task SendMessage(params MessageDTO[] data)
        {
            throw new NotImplementedException();
        }
    }
}
