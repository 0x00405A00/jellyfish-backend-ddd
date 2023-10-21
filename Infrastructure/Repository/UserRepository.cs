using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal class UserRepository : GenericRepository<Domain.Entities.User.User, Infrastructure.DatabaseEntity.User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        { 
        
        }
        public async override Task<Domain.Entities.User.User> GetAsync(Expression<Func<DatabaseEntity.User, bool>> expression)
        {
            var value = await _dbSet.Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .ThenInclude(ur => ur.RoleUu)
                                    .Include(i => i.UserFriendUserUus)
                                    .ThenInclude(uf => uf.FriendUserUu)
                                    .ThenInclude(ut => ut.UserTypeUu)
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

        public async Task<bool> IsPhoneAlreadyInUse(string phone)
        {
            return await _dbSet.AsNoTracking()
                         .Where(x => x.Phone == phone.ToLower())
                         .AnyAsync();
        }

        public async Task<bool> IsUserRegistered(string email)
        {
            return await _dbSet.AsNoTracking()
                         .Where(x => x.Email == email.ToLower() && x.ActivationDatetime != null)
                         .AnyAsync();
        }

        public async override Task<ICollection<Domain.Entities.User.User>> ListAsync(Expression<Func<DatabaseEntity.User, bool>> expression)
        {
            var value = await _dbSet.Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .ThenInclude(ur => ur.RoleUu)
                                    .Include(i => i.UserFriendUserUus)
                                    .ThenInclude(uf => uf.FriendUserUu)
                                    .ThenInclude(ut => ut.UserTypeUu)
                                    .AsNoTracking()
                                    .Where(expression)
                                    .ToListAsync();

            return this.MapToDomainEntity(value, true);
        }
    }
}
