using Domain.Entities.User;
using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal class UserRepository : GenericRepository<User, Infrastructure.DatabaseEntity.User>,IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        { 
        
        }
        public async override Task<User> GetAsync(Expression<Func<DatabaseEntity.User, bool>> expression)
        {
            var value = await _dbSet.Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .Include(i => i.UserFriendFriendUserUus)
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(expression);

            return this.MapToDomainEntity(value,true);
        }

        public async Task<bool> IsEmailAlreadyInUse(string email)
        {
            return await _dbSet.AsNoTracking()
                         .Where(x => x.Email == email.ToLower())
                         .AnyAsync();
        }

        public async Task<bool> IsUserRegistered(string email)
        {
            return await _dbSet.AsNoTracking()
                         .Where(x => x.Email == email.ToLower() && x.ActivationDatetime != null)
                         .AnyAsync();
        }

        public async override Task<ICollection<User>> ListAsync(Expression<Func<DatabaseEntity.User, bool>> expression)
        {
            var value = await _dbSet.Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .Include(i => i.UserFriendFriendUserUus)
                                    .AsNoTracking()
                                    .Where(expression)
                                    .ToListAsync();

            return this.MapToDomainEntity(value, true);
        }
    }
}
