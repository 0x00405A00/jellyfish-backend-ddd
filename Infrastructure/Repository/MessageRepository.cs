using Infrastructure.Abstractions;

namespace Infrastructure.Repository
{
    internal class MessageRepository : GenericRepository<Domain.Entities.Message.Message, Infrastructure.DatabaseEntity.Message>,IMessageRepository
    {
        public MessageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        { 
        
        }

        public DatabaseEntity.Message Get(Guid id)
        {
            var d = _context.Set<Infrastructure.DatabaseEntity.Message>().Where(x => x.Uuid == id).First();
            return d;
        }

        public void Insert(DatabaseEntity.Message message)
        {
            throw new NotImplementedException();
        }

        public void Update(DatabaseEntity.Message message)
        {
            throw new NotImplementedException();
        }
    }
}
