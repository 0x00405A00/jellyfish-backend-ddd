using Domain.Entities.Auths;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Concrete
{
    internal class AuthRepository : GenericRepository<Auth>, IAuthRepository
    {
        public AuthRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }

        public override async Task<Auth> GetAsync(Expression<Func<Auth, bool>> expression)
        {
            var result = await DbSet.AsNoTracking()
                                     .Include(x => x.User)
                                     .ThenInclude(x => x.UserHasRelationToRoles)
                                     .ThenInclude(x => x.Role)
                                     .AsSingleQuery()
                                     .FirstOrDefaultAsync(expression);
            return result;
        }
    }
}
