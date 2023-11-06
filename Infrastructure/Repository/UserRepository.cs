using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Infrastructure.Repository.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Shared.DataFilter.Infrastructure;
using System.Configuration;
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
                                    .AsSingleQuery()
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
            var value = await _dbSet.ExpressionQuery(expression)
                                    .Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .ThenInclude(ur => ur.RoleUu)
                                    .Include(i => i.UserFriendUserUus)
                                    .ThenInclude(uf => uf.FriendUserUu)
                                    .ThenInclude(ut => ut.UserTypeUu)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .ToListAsync();
            return this.MapToDomainEntity(value, true);
        }
        public async override Task<ICollection<Domain.Entities.User.User>> ListAsync(ColumnSearchAggregateDTO columnSearchAggregateDTO)
        {
            var value = await _dbSet.ExpressionQuery(columnSearchAggregateDTO)
                                    .Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .ThenInclude(ur => ur.RoleUu)
                                    .Include(i => i.UserFriendUserUus)
                                    .ThenInclude(uf => uf.FriendUserUu)
                                    .ThenInclude(ut => ut.UserTypeUu)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .ToListAsync();

            value = value.OrderQuery(columnSearchAggregateDTO);
            return this.MapToDomainEntity(value, true);
        }
        public override async Task<RepositoryResponse<ICollection<Domain.Entities.User.User>>> ListAsyncWithMeta(Expression<Func<DatabaseEntity.User, bool>> expression = null)
        {
            int count = await CountMaxAsync();
            var value = await _dbSet.ExpressionQuery(expression)
                                    .Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .ThenInclude(ur => ur.RoleUu)
                                    .Include(i => i.UserFriendUserUus)
                                    .ThenInclude(uf => uf.FriendUserUu)
                                    .ThenInclude(ut => ut.UserTypeUu)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .ToListAsync();

            var data = this.MapToDomainEntity(value, true);
            var meta = new Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<Domain.Entities.User.User>>(data, ref meta);
        }
        public override async Task<RepositoryResponse<ICollection<Domain.Entities.User.User>>> ListAsyncWithMeta(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {
            int count = await CountMaxAsync();
            var value = await _dbSet.ExpressionQuery(columnSearchAggregateDTO)
                                    .Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .ThenInclude(ur => ur.RoleUu)
                                    .Include(i => i.UserFriendUserUus)
                                    .ThenInclude(uf => uf.FriendUserUu)
                                    .ThenInclude(ut => ut.UserTypeUu)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .ToListAsync();

            value = value.OrderQuery(columnSearchAggregateDTO);
            var data = this.MapToDomainEntity(value, true);
            var meta = new Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<Domain.Entities.User.User>>(data, ref meta, columnSearchAggregateDTO);
        }
        public override ICollection<Domain.Entities.User.User> List(ColumnSearchAggregateDTO columnSearchAggregateDTO)
        {
            var value = _dbSet.ExpressionQuery(columnSearchAggregateDTO)
                                    .Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .ThenInclude(ur => ur.RoleUu)
                                    .Include(i => i.UserFriendUserUus)
                                    .ThenInclude(uf => uf.FriendUserUu)
                                    .ThenInclude(ut => ut.UserTypeUu)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .ToList();

            return this.MapToDomainEntity(value, true);
        }
    }
}
