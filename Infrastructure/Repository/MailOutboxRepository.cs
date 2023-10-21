using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal class MailOutboxRepository : GenericRepository<Infrastructure.DatabaseEntity.MailOutbox>, IMailoutboxRepositorySingleton
    {
        public MailOutboxRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        { 
        
        }

        public async Task<EmailType> GetEmailType(string type)
        {
            var result =await _context.EmailTypes.AsNoTracking().Where(x => x.Type == type)
                .FirstAsync();
            return result;
        }

        public override async Task<ICollection<MailOutbox>> ListAsync(Expression<Func<MailOutbox, bool>> expression = null)
        {
            var result = expression==null? await _dbSet.AsNoTracking().Include(x => x.MailOutboxAttachments)
                .Include(x => x.MailOutboxRecipients)
                .ThenInclude(x => x.EmailTypeUu)
                .AsNoTracking()
                .ToListAsync():
                await _dbSet.AsNoTracking().Include(x => x.MailOutboxAttachments)
                .Include(x => x.MailOutboxRecipients)
                .ThenInclude(x=>x.EmailTypeUu)
                .Where(expression)
                .ToListAsync();
            return result;
        }
    }
    internal class MailOutboxRepositoryScoped : MailOutboxRepository, IMailoutboxRepositoryScoped
    {
        public MailOutboxRepositoryScoped(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}
