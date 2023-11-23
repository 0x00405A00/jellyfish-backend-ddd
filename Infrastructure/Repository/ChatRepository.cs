using Domain.Entities.Chats;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal class ChatRepository : GenericRepository<Chat, Infrastructure.DatabaseEntity.Chat>,IChatRepository
    {
        public ChatRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        { 
        
        }
        public async override Task<Chat> GetAsync(Expression<Func<DatabaseEntity.Chat, bool>> expression)
        {
            var value = await _dbSet
                .Include(i => i.Messages)
                .ThenInclude(u => u.MessageOwnerNavigation)
                .ThenInclude(ut => ut.UserTypeUu)
                .Include(i => i.OwnerUserUu)
                .ThenInclude(ut=>ut.UserTypeUu)
                .Include(i => i.ChatRelationToUsers)
                .ThenInclude(cru=>cru.UserUu)
                .ThenInclude(ut => ut.UserTypeUu)
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);

            return await this.MapToDomainEntity(value, true);
        }
    }
}
