using Domain.Entities.Roles;
using Infrastructure.Abstractions;

namespace Infrastructure.Repository.Concrete
{
    internal class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

    }
}
