using Domain.Entities.Users;
using Infrastructure.Abstractions;

namespace Infrastructure.Repository.Concrete
{
    internal class UserTypeRepository : GenericRepository<UserType>, IUserTypeRepository
    {
        public UserTypeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

    }
}
