using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal class MailOutboxRepository : GenericRepository<Infrastructure.DatabaseEntity.MailOutbox>, IMailoutboxRepository
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

        public void InsertMailAttachment(List<MailOutboxAttachment> mailOutboxAttachments)
        {
            _context.MailOutboxAttachments.AddRange(mailOutboxAttachments);
        }

        public void InsertMailRecipients(List<MailOutboxRecipient> mailOutboxRecipients)
        {
            _context.MailOutboxRecipients.AddRange(mailOutboxRecipients);
        }

        public override async Task<ICollection<MailOutbox>> ListAsync(Expression<Func<MailOutbox, bool>> expression = null)
        {
            var result = expression==null? await _dbSet.Include(x => x.MailOutboxAttachments)
                .Include(x => x.MailOutboxRecipients)
                .ThenInclude(x => x.EmailTypeUu)
                .AsSingleQuery()
                .ToListAsync():
                await _dbSet.Include(x => x.MailOutboxAttachments)
                .Include(x => x.MailOutboxRecipients)
                .ThenInclude(x=>x.EmailTypeUu)
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
