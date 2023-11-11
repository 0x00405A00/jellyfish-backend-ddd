using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal class AuthRepository : GenericRepository<Infrastructure.DatabaseEntity.Auth>, IAuthRepository
    {
        public AuthRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        {   
        
        }

        public override async Task<Auth> GetAsync(Expression<Func<Auth, bool>> expression)
        {
            var result = await _dbSet.AsNoTracking()
                                     .Include(x => x.UserUu)
                                     .ThenInclude(x => x.UserRelationToRoles)
                                     .ThenInclude(x => x.RoleUu)
                                     .AsSingleQuery()
                                     .FirstOrDefaultAsync(expression);
            return result;
        }
    }
}
