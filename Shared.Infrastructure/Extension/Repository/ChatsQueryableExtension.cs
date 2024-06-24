using Domain.Entities.Chats;
using Microsoft.EntityFrameworkCore;

namespace Shared.Infrastructure.Extension.Repository
{
    public static class ChatsQueryableExtension
    {
        public static IQueryable<Chat> ChatDefaultIncludes(this IQueryable<Chat> chatsQuery)
        {
            return chatsQuery
                .Include(i => i.Messages)
                .ThenInclude(u => u.CreatedByUser)
                .ThenInclude(ut => ut.UserType)
                .Include(i => i.ChatRelationToUsers)
                .ThenInclude(cru => cru.User)
                .ThenInclude(ut => ut.UserType);
        }
        public static IQueryable<Chat> ChatIncludesWithoutMessages(this IQueryable<Chat> chatsQuery)
        {
            return chatsQuery
                .Include(i => i.ChatRelationToUsers)
                .ThenInclude(cru => cru.User)
                .ThenInclude(ut => ut.UserType);
        }
    }
}
