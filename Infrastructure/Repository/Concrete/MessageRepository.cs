using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Entities.Messages;

namespace Infrastructure.Repository.Concrete
{
    internal class MessageRepository : GenericRepository<Message>, IMessageRepository
    {
        private readonly IMessageOutboxRepository messageOutboxRepository;

        public MessageRepository(
            ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
            this.messageOutboxRepository = messageOutboxRepository;
        }

        public override void Add(Message entity)
        {
            base.Add(entity);
        }

        public override async Task<Message> GetAsync(Expression<Func<Message, bool>> expression)
        {
            var value = await DbSet
                .Include(i => i.Chat)
                .Include(i=>i.MessagesInOutbox)
                .AsNoTracking()
                .Where(expression)
                .FirstOrDefaultAsync();


            return value;
        }

    }
}
