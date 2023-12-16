using Domain.Entities.MailoutBox;
using Infrastructure.Abstractions;

namespace Infrastructure.Repository.Concrete
{
    internal class EmailTypeRepository : GenericRepository<EmailType>, IEmailTypeRepository
    {
        public EmailTypeRepository(ApplicationDbContextMailService applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
