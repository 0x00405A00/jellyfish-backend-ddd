using Domain.Entities.Messages;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Concrete
{
    internal class MessageOutboxRepository : GenericRepository<MessageOutbox>, IMessageOutboxRepository
    {
        public MessageOutboxRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public override async Task<MessageOutbox> GetAsync(Expression<Func<MessageOutbox, bool>> expression)
        {
            return await DbSet
                .Include(i => i.Message)
                .ThenInclude(m => m.Chat)
                .AsNoTracking()
                .Where(expression)
                .FirstOrDefaultAsync();
        }

        public override async Task<ICollection<MessageOutbox>> ListAsync(Expression<Func<MessageOutbox, bool>> expression)
        {
            return await DbSet
                .Include(i => i.Message)
                .ThenInclude(m => m.Chat)
                .AsNoTracking()
                .Where(expression??DefaultExpression)
                .ToListAsync();
        }

    }
}
