using Infrastructure.DatabaseEntity;
using Infrastructure.Repository;

namespace Infrastructure.Abstractions
{
    public interface IUserTypeRepository : IGenericRepository<Domain.Entities.User.UserType, UserType>
    {

    }
}
