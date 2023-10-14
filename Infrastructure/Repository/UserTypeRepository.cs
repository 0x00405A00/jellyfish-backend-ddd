using Domain.Entities.User;
using Infrastructure.Abstractions;

namespace Infrastructure.Repository
{
    internal class UserTypeRepository : GenericRepository<UserType, Infrastructure.DatabaseEntity.UserType>,IUserTypeRepository
    {
        public UserTypeRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        { 
        
        }
        
    }
}
