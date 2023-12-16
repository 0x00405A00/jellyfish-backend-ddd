using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Entities.Message;

namespace Infrastructure.Repository.Concrete
{
    internal class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        public MessageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public override async Task<Message> GetAsync(Expression<Func<Message, bool>> expression)
        {
            var value = await DbSet
                .Include(i => i.Chat)
                .AsNoTracking()
                .Where(expression)
                .FirstOrDefaultAsync();


            return value;
        }

    }
}
