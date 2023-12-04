using Infrastructure.Abstractions;
using Infrastructure.Repository.Primitives;
using Microsoft.EntityFrameworkCore;
using Shared.DataFilter.Infrastructure;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal class ChatRepository : GenericRepository<Domain.Entities.Chats.Chat, Infrastructure.DatabaseEntity.Chat>,IChatRepository
    {
        public ChatRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        { 
        
        }
        public async override Task<Domain.Entities.Chats.Chat> GetAsync(Expression<Func<DatabaseEntity.Chat, bool>> expression)
        {
            var value = await DbSet
                .Include(i => i.Messages)
                .ThenInclude(u => u.MessageOwnerNavigation)
                .ThenInclude(ut => ut.UserTypeUu)
                .Include(i => i.OwnerUserUu)
                .ThenInclude(ut=>ut.UserTypeUu)
                .Include(i => i.ChatRelationToUsers)
                .ThenInclude(cru=>cru.UserUu)
                .ThenInclude(ut => ut.UserTypeUu)
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);

            return await this.MapToDomainEntity(value, true);
        }
        public async override Task<ICollection<Domain.Entities.Chats.Chat>> ListAsync(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {
            var value = await DbSet.ExpressionQuery(columnSearchAggregateDTO)
                .Include(i => i.Messages)
                .ThenInclude(u => u.MessageOwnerNavigation)
                .ThenInclude(ut => ut.UserTypeUu)
                .Include(i => i.OwnerUserUu)
                .ThenInclude(ut => ut.UserTypeUu)
                .Include(i => i.ChatRelationToUsers)
                .ThenInclude(cru => cru.UserUu)
                .ThenInclude(ut => ut.UserTypeUu)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync();

            value = value.OrderQuery(columnSearchAggregateDTO);
            return await this.MapToDomainEntity(value, true);
        }

        public override async Task<RepositoryResponse<ICollection<Domain.Entities.Chats.Chat>>> ListAsyncWithMeta(Expression<Func<DatabaseEntity.Chat, bool>> expression = null)
        {
            int count = await CountMaxAsync(expression);
            var value = await DbSet.ExpressionQuery(expression)
                .Include(i => i.Messages)
                .ThenInclude(u => u.MessageOwnerNavigation)
                .ThenInclude(ut => ut.UserTypeUu)
                .Include(i => i.OwnerUserUu)
                .ThenInclude(ut => ut.UserTypeUu)
                .Include(i => i.ChatRelationToUsers)
                .ThenInclude(cru => cru.UserUu)
                .ThenInclude(ut => ut.UserTypeUu)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync();

            ICollection<Domain.Entities.Chats.Chat> data = await this.MapToDomainEntity(value, true);
            var meta = new Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<Domain.Entities.Chats.Chat>>(data, ref meta);
        }
        public override async Task<RepositoryResponse<ICollection<Domain.Entities.Chats.Chat>>> ListAsyncWithMeta(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {
            var expression = columnSearchAggregateDTO.GetExpression<DatabaseEntity.Chat>(nameof(DatabaseEntity.Chat));
            int count = await CountMaxAsync(expression);
            var value = await DbSet.ExpressionQuery(expression)
                .Include(i => i.Messages)
                .ThenInclude(u => u.MessageOwnerNavigation)
                .ThenInclude(ut => ut.UserTypeUu)
                .Include(i => i.OwnerUserUu)
                .ThenInclude(ut => ut.UserTypeUu)
                .Include(i => i.ChatRelationToUsers)
                .ThenInclude(cru => cru.UserUu)
                .ThenInclude(ut => ut.UserTypeUu)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync();

            value = value.OrderQuery(columnSearchAggregateDTO);
            ICollection<Domain.Entities.Chats.Chat> data = await this.MapToDomainEntity(value, true);
            var meta = new Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<Domain.Entities.Chats.Chat>>(data, ref meta, columnSearchAggregateDTO);
        }
    }
}
