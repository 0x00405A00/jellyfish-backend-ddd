using Domain.Entities.Role;
using Infrastructure.Abstractions;

namespace Infrastructure.Repository
{
    internal class RoleRepository : GenericRepository<Role, Infrastructure.DatabaseEntity.Role>,IRoleRepository
    {
        public RoleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        { 
        
        }
        
    }
}
