using Domain.Entities.Mails;
using Infrastructure.Abstractions;

namespace Infrastructure.Repository.Concrete
{
    internal class EmailTypeRepository : GenericRepository<EmailSendingType>, IEmailTypeRepository
    {
        public EmailTypeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
