using Infrastructure.Abstractions;

namespace Infrastructure.Repository
{
    internal class MessageRepository : GenericRepository<Domain.Entities.Message.Message, Infrastructure.DatabaseEntity.Message>,IMessageRepository
    {
        public MessageRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        { 
        
        }

    }
}
