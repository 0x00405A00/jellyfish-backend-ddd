using Domain.Entities.Mails;
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
                .Include(i => i.Message)
                .ThenInclude(u => u.CreatedByUser)
                .AsNoTracking()
                .Where(expression)
                .FirstOrDefaultAsync();
        }

        public override async Task<ICollection<MessageOutbox>> ListAsync(Expression<Func<MessageOutbox, bool>> expression)
        {
            return await DbSet
                .Include(i => i.Message)
                .ThenInclude(m => m.Chat)
                .Include(i => i.Message)
                .ThenInclude(u => u.CreatedByUser)
                .AsNoTracking()
                .Where(expression??DefaultExpression)
                .ToListAsync();
        }
        public async Task<ICollection<MessageOutbox>> ListWithPessimisticLockAsync(Expression<Func<MessageOutbox, bool>> expression = null)
        {
            const string query =
                """
                SELECT *
                FROM public.message_outbox
                ORDER BY created_time asc 
                FOR UPDATE 
                SKIP LOCKED
                """;

            var result = await DbSet
                .FromSqlRaw(query)
                .Include(i => i.Message)
                .ThenInclude(m => m.Chat)
                .Include(i => i.Message)
                .ThenInclude(u=>u.CreatedByUser)
                .AsSingleQuery()
                .AsNoTracking()
                .Where(expression ?? DefaultExpression)
                .ToListAsync();
            return result;
        }

    }
}
