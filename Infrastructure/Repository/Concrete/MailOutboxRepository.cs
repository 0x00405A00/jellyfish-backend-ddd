using Domain.Entities.Mails;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Concrete
{
    internal class MailOutboxRepository : GenericRepository<MailOutbox>, IMailoutboxRepository
    {
        public MailOutboxRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public override async Task<ICollection<MailOutbox>> ListAsync(Expression<Func<MailOutbox, bool>> expression = null)
        {
            var result = await DbSet.Include(x => x.Attachments)
                .Include(x => x.Recipients)
                .AsSingleQuery()
                .Where(DefaultExpression)
                .ToListAsync();
            return result;
        }

        public async Task<ICollection<MailOutbox>> ListWithPessimisticLockAsync(Expression<Func<MailOutbox, bool>> expression = null)
        {
            const string query =
                """
                SELECT *
                FROM public.mail_outbox 
                ORDER BY created_time asc 
                FOR UPDATE 
                SKIP LOCKED
                """;

            var result = await DbSet
                .FromSqlRaw<MailOutbox>(query)
                .Include(x => x.Attachments)
                .Include(x => x.Recipients)
                .AsNoTracking() 
                .AsSingleQuery()
                .Where(expression??DefaultExpression)
                .ToListAsync();
            return result;
        }
    }
    internal class MailOutboxRepositoryMailService : MailOutboxRepository, IMailoutboxRepositoryMailService
    {
        public MailOutboxRepositoryMailService(ApplicationDbContextMailService applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
