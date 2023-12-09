using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal class MessageRepository : GenericRepository<Domain.Entities.Message.Message, Infrastructure.DatabaseEntity.Message>,IMessageRepository
    {
        public MessageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        { 
        
        }

        public override async Task<Domain.Entities.Message.Message> GetAsync(Expression<Func<DatabaseEntity.Message, bool>> expression)
        {
            var value = await DbSet
                .Include(i => i.ChatUu)
                .AsNoTracking()
                .Where(expression)
                .FirstOrDefaultAsync();

            return await this.MapToDomainEntity(value, true);
        }

    }
}
