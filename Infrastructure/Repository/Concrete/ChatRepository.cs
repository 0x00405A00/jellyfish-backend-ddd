﻿using Domain.Entities.Chats;
using Infrastructure.Abstractions;
using Infrastructure.Repository.Primitives;
using Microsoft.EntityFrameworkCore;
using Shared.DataFilter.Infrastructure;
using System.Linq.Expressions;

namespace Infrastructure.Repository.Concrete
{
    internal class ChatRepository : GenericRepository<Chat>, IChatRepository
    {
        public ChatRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
        public override void Update(Chat entity)
        {
            base.Update(entity);
        }
        public async override Task<Chat> GetAsync(Expression<Func<Chat, bool>> expression)
        {
            var value = await DbSet
                .Include(i => i.Messages)
                .ThenInclude(u => u.CreatedByUser)
                .ThenInclude(ut => ut.UserType)
                .Include(i => i.ChatRelationToUsers)
                .ThenInclude(cru => cru.User)
                .ThenInclude(ut => ut.UserType)
                .AsNoTracking()
                .AsSingleQuery()
                .FirstOrDefaultAsync(expression);

            return value;
        }
        public async override Task<ICollection<Chat>> ListAsync(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {
            var value = await DbSet.ExpressionQuery(columnSearchAggregateDTO)
                .Include(i => i.Messages)
                .ThenInclude(u => u.CreatedByUser)
                .ThenInclude(ut => ut.UserType)
                .Include(i => i.ChatRelationToUsers)
                .ThenInclude(cru => cru.User)
                .ThenInclude(ut => ut.UserType)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync();

            value = value.OrderQuery(columnSearchAggregateDTO);
            return value;
        }

        public override async Task<RepositoryResponse<ICollection<Chat>>> ListAsyncWithMeta(Expression<Func<Chat, bool>> expression = null)
        {
            int count = await CountMaxAsync(expression);
            var value = await DbSet.ExpressionQuery(expression)
                .Include(i => i.Messages)
                .ThenInclude(u => u.CreatedByUser)
                .ThenInclude(ut => ut.UserType)
                .Include(i => i.ChatRelationToUsers)
                .ThenInclude(cru => cru.User)
                .ThenInclude(ut => ut.UserType)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync();

            ICollection<Chat> data = value;
            var meta = new Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<Chat>>(data, ref meta);
        }
        public override async Task<RepositoryResponse<ICollection<Chat>>> ListAsyncWithMeta(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {
            var expression = columnSearchAggregateDTO.GetExpression<Chat>(nameof(Chat));
            int count = await CountMaxAsync(expression);
            var value = await DbSet.ExpressionQuery(expression)
                .Include(i => i.Messages)
                .ThenInclude(u => u.CreatedByUser)
                .ThenInclude(ut => ut.UserType)
                .Include(i => i.Members)
                .ThenInclude(cru => cru.User)
                .ThenInclude(ut => ut.UserType)
                .AsNoTracking()
                .AsSingleQuery()
                .ToListAsync();

            value = value.OrderQuery(columnSearchAggregateDTO);
            ICollection<Chat> data = value;
            var meta = new Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<Chat>>(data, ref meta, columnSearchAggregateDTO);
        }
    }
}
