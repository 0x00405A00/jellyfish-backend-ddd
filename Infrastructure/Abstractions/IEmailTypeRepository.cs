using Infrastructure.Repository;
using Domain.Entities.MailoutBox;

namespace Infrastructure.Abstractions
{
    public interface IEmailTypeRepository : IGenericRepository<EmailType>
    {
    }
}