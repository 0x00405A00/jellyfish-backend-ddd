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
            var value = await _dbSet.Include(i => i.Messages)
                .Include(i => i.OwnerUserUu)
                .Include(i => i.ChatRelationToUsers)
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);

            return this.MapToDomainEntity(value, true);
        }
    }
}
