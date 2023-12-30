using Domain.Entities.Mails;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
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
            var result = expression == null ? await DbSet.Include(x => x.Attachments)
                .Include(x => x.Recipients)
                .ThenInclude(x => x.SendingType)
                .AsSingleQuery()
                .ToListAsync() :
                await DbSet.Include(x => x.Attachments)
                .Include(x => x.Recipients)
                .ThenInclude(x => x.SendingType)
                .AsSingleQuery()
                .Where(expression)
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
