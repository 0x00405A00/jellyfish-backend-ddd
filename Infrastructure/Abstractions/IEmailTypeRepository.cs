using Infrastructure.Repository;
using Domain.Entities.Mails;

namespace Infrastructure.Abstractions
{
    public interface IEmailTypeRepository : IGenericRepository<EmailSendingType>
    {
    }
}