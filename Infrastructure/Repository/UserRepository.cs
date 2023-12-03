﻿using Infrastructure.Abstractions;
using Infrastructure.DatabaseEntity;
using Infrastructure.Repository.Primitives;
using Microsoft.EntityFrameworkCore;
using Shared.DataFilter.Infrastructure;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal class UserRepository : GenericRepository<Domain.Entities.User.User, Infrastructure.DatabaseEntity.User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        {
        }

        public async Task<List<Domain.ValueObjects.FriendshipRequest>> GetAllFriendshipRequests(Expression<Func<DatabaseEntity.User, bool>> expression)
        {
            //Will be optimized in future, because GetAsync generates too much not useful overhead (e.g. roles etc.) that are not used in this operation or operation related processes
            var value = await this.GetAsync(expression);
            return value.FriendshipRequests.ToList();
        }

        /*public static Func<ApplicationDbContext, Expression<Func<DatabaseEntity.User, bool>>, DatabaseEntity.User> GetUserCompiledQuery =
EF.CompileQuery((ApplicationDbContext context, Expression<Func<DatabaseEntity.User, bool>> expression) =>
   context.Users
          .Include(i => i.UserTypeUu)
          .Include(i => i.UserRelationToRoles)
              .ThenInclude(ur => ur.RoleUu)
          .Include(i => i.UserFriendUserUus)
              .ThenInclude(uf => uf.FriendUserUu)
                  .ThenInclude(ut => ut.UserTypeUu)
          .AsNoTracking()
          .Where(expression)
          .FirstOrDefault());*/

        public async override Task<Domain.Entities.User.User> GetAsync(Expression<Func<DatabaseEntity.User, bool>> expression)
        {
            var value = await DbSet.Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .ThenInclude(ur => ur.RoleUu)
                                    .Include(i => i.UserFriendUserUus)
                                        .ThenInclude(uf => uf.FriendUserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .Include(i => i.UserFriendshipRequestTargetUserUus)//all request that user received from other users
                                        .ThenInclude(ru => ru.UserUu)
                                        .ThenInclude(ut=>ut.UserTypeUu)
                                    .Include(i => i.UserFriendshipRequestUserUus)//all request that user requested to other users
                                        .ThenInclude(tu => tu.TargetUserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .FirstOrDefaultAsync(expression);
            return await this.MapToDomainEntity(value, true);
        }

        public async Task<bool> IsEmailAlreadyInUse(string email)
        {
            return await DbSet.AsNoTracking()
                         .Where(x => x.Email == email.ToLower())
                         .AnyAsync();
        }

        public async Task<bool> IsPhoneAlreadyInUse(string phone)
        {
            return await DbSet.AsNoTracking()
                         .Where(x => x.Phone == phone.ToLower())
                         .AnyAsync();
        }

        public async Task<bool> IsUserRegistered(string email)
        {
            return await DbSet.AsNoTracking()
                         .Where(x => x.Email == email.ToLower() && x.ActivationDatetime != null)
                         .AnyAsync();
        }

        public async override Task<ICollection<Domain.Entities.User.User>> ListAsync(Expression<Func<DatabaseEntity.User, bool>> expression)
        {
            var value = await DbSet.ExpressionQuery(expression)
                                    .Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .ThenInclude(ur => ur.RoleUu)
                                    .Include(i => i.UserFriendUserUus)
                                        .ThenInclude(uf => uf.FriendUserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .Include(i => i.UserFriendshipRequestTargetUserUus)//all request that user received from other users
                                        .ThenInclude(ru => ru.UserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .Include(i => i.UserFriendshipRequestUserUus)//all request that user requested to other users
                                        .ThenInclude(tu => tu.TargetUserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .ToListAsync();
            return await this.MapToDomainEntity(value, true);
        }
        public async override Task<ICollection<Domain.Entities.User.User>> ListAsync(ColumnSearchAggregateDTO columnSearchAggregateDTO)
        {
            var value = await DbSet.ExpressionQuery(columnSearchAggregateDTO)
                                    .Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .ThenInclude(ur => ur.RoleUu)
                                    .Include(i => i.UserFriendUserUus)
                                        .ThenInclude(uf => uf.FriendUserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .Include(i => i.UserFriendshipRequestTargetUserUus)//all request that user received from other users
                                        .ThenInclude(ru => ru.UserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .Include(i => i.UserFriendshipRequestUserUus)//all request that user requested to other users
                                        .ThenInclude(tu => tu.TargetUserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .ToListAsync();

            value = value.OrderQuery(columnSearchAggregateDTO);
            return await this.MapToDomainEntity(value, true);
        }
        public override async Task<RepositoryResponse<ICollection<Domain.Entities.User.User>>> ListAsyncWithMeta(Expression<Func<DatabaseEntity.User, bool>> expression = null)
        {
            int count = await CountMaxAsync(expression);
            var value = await DbSet.ExpressionQuery(expression)
                                    .Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .ThenInclude(ur => ur.RoleUu)
                                    .Include(i => i.UserFriendUserUus)
                                        .ThenInclude(uf => uf.FriendUserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .Include(i => i.UserFriendshipRequestTargetUserUus)//all request that user received from other users
                                        .ThenInclude(ru => ru.UserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .Include(i => i.UserFriendshipRequestUserUus)//all request that user requested to other users
                                        .ThenInclude(tu => tu.TargetUserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .ToListAsync();

            var data = await this.MapToDomainEntity(value, true);
            var meta = new Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<Domain.Entities.User.User>>(data, ref meta);
        }
        public override async Task<RepositoryResponse<ICollection<Domain.Entities.User.User>>> ListAsyncWithMeta(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {
            var expression = columnSearchAggregateDTO.GetExpression<User>(nameof(User));
            int count = await CountMaxAsync(expression);
            var value = await DbSet.ExpressionQuery(columnSearchAggregateDTO)
                                    .Include(i => i.UserTypeUu)
                                    .Include(i => i.UserRelationToRoles)
                                    .ThenInclude(ur => ur.RoleUu)
                                    .Include(i => i.UserFriendUserUus)
                                        .ThenInclude(uf => uf.FriendUserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .Include(i => i.UserFriendshipRequestTargetUserUus)//all request that user received from other users
                                        .ThenInclude(ru => ru.UserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .Include(i => i.UserFriendshipRequestUserUus)//all request that user requested to other users
                                        .ThenInclude(tu => tu.TargetUserUu)
                                        .ThenInclude(ut => ut.UserTypeUu)
                                    .AsNoTracking()
                                    .AsSingleQuery()
                                    .ToListAsync();

            value = value.OrderQuery(columnSearchAggregateDTO);
            var data = await this.MapToDomainEntity(value, true);
            var meta = new Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<Domain.Entities.User.User>>(data, ref meta, columnSearchAggregateDTO);
        }
    }
}
