using Domain.Entities.Chats;
using Domain.ValueObjects;

namespace SharedTest.DomainTestInstance.Entity.Chats
{
    public static class InstancingHelper
    {
        public static Domain.Entities.Chats.Chat GetChatInstance(Domain.Entities.User.User adminUser)
        {
            var chatId = new ChatId(Guid.NewGuid());
            var createdByUser = adminUser ?? User.InstancingHelper.GetUserInstance();
            var chatName = "Sample Chat";
            var members = new List<ChatMember>()
            {
                ChatMember.Create(User.InstancingHelper.GetUserInstance(),false,DateTime.Now,null,null),
                ChatMember.Create(User.InstancingHelper.GetUserInstance(),false,DateTime.Now,null,null),

            };

            var messages = new List<Domain.Entities.Message.Message>();

            if (adminUser != null)
            {
                var chatAdm = ChatMember.Create(adminUser, true, DateTime.Now, null, null);
                members.Add(chatAdm);
            }

            var chat = Chat.Create(
                chatId,
                createdByUser,
                chatName,
                null,
                null,
                members,
                messages,
                DateTime.Now,
                null,
                null);
            return chat;
        }
    }
}
