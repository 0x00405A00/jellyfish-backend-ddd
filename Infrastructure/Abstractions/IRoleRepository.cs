using Infrastructure.DatabaseEntity;
using Infrastructure.Repository;

namespace Infrastructure.Abstractions
{
    public interface IRoleRepository : IGenericRepository<Domain.Entities.Role.Role, Role>
    {

    }
}
