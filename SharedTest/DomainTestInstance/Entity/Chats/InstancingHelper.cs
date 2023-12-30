using Domain.Entities.Chats;
using Domain.Entities.Messages;
using Domain.Extension;
using Domain.Primitives.Ids;

namespace SharedTest.DomainTestInstance.Entity.Chats
{
    public static class InstancingHelper
    {
        public static Domain.Entities.Chats.Chat GetChatInstance(Domain.Entities.Users.User adminUser,Guid? chatId = null)
        {
            var _chatId = new ChatId(chatId??Guid.NewGuid());
            var createdByUser = adminUser ?? SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var chatName = "Sample Chat";
            var member1 = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();
            var member2 = SharedTest.DomainTestInstance.Entity.Users.InstancingHelper.GetUserInstance();

            var members = new List<ChatRelationToUser>()
            {
                ChatRelationToUser.Create(ChatRelationToUser.NewId(),member1.Id,_chatId,false,DateTime.Now.ToTypedDateTime(),null,null,null,null,null),
                ChatRelationToUser.Create(ChatRelationToUser.NewId(),member2.Id,_chatId,false,DateTime.Now.ToTypedDateTime(),null,null,null,null,null),

            };

            var messages = new List<Message>();

            if (adminUser != null)
            {
                var chatAdm = ChatRelationToUser.Create(ChatRelationToUser.NewId(), adminUser.Id, _chatId, true, DateTime.Now.ToTypedDateTime(), null, null, null, null, null);
               members.Add(chatAdm);
            }

            var chat = Chat.Create(
                _chatId,
                "testChat",
                "testDesc",
                null,
                members,
                DateTime.Now.ToTypedDateTime(),
                null,
                null,
                null,
                null,
                null);
            return chat;
        }
    }
}
