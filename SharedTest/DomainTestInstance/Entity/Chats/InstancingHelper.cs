using Domain.Entities.Chats;

namespace SharedTest.DomainTestInstance.Entity.Chats
{
    public static class InstancingHelper
    {
        public static Domain.Entities.Chats.Chat GetChatInstance(Domain.Entities.User.User adminUser,Guid? chatId = null)
        {
            var _chatId = new ChatId(chatId??Guid.NewGuid());
            var createdByUser = adminUser ?? User.InstancingHelper.GetUserInstance();
            var chatName = "Sample Chat";
            var members = new List<ChatMember>()
            {
                ChatMember.Create(Guid.NewGuid(),User.InstancingHelper.GetUserInstance(),false,DateTime.Now,null,null),
                ChatMember.Create(Guid.NewGuid(),User.InstancingHelper.GetUserInstance(),false,DateTime.Now,null,null),

            };

            var messages = new List<Domain.Entities.Message.Message>();

            if (adminUser != null)
            {
                var chatAdm = ChatMember.Create(Guid.NewGuid(), adminUser, true, DateTime.Now, null, null);
                members.Add(chatAdm);
            }

            var chat = Chat.Create(
                _chatId,
                createdByUser,
                null,
                null,
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
