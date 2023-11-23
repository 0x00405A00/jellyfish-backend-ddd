using Domain.Primitives;
using Infrastructure.Abstractions;
using Infrastructure.Mapper;
using Infrastructure.Repository.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Shared.DataFilter.Infrastructure;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal abstract class GenericRepository<TDbEntity> : IGenericRepository<TDbEntity>
        where TDbEntity : DatabaseEntityModel
    {
        protected readonly DbSet<TDbEntity> _dbSet;
        protected readonly ApplicationDbContext _context;

        protected GenericRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
            _dbSet = _context.Set<TDbEntity>();
        }

        #region Sync
        public void Add(TDbEntity entity)
        {
            _dbSet.Add(entity);
        }
        public void Attach(TDbEntity entity)
        {
            _dbSet.Attach(entity);
        }

        public void Update(TDbEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(TDbEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public virtual TDbEntity Get(Expression<Func<TDbEntity, bool>> expression)
        {
            var value = _dbSet.AsNoTracking().FirstOrDefault(expression);
            return value;
        }

        public virtual ICollection<TDbEntity> List(Expression<Func<TDbEntity, bool>> expression = null)
        {
            var value = _dbSet.ExpressionQuery(expression);
            return value.ToList();
        }
        public virtual ICollection<TDbEntity> List(ColumnSearchAggregateDTO columnSearchAggregateDTO)
        {
            var value = _dbSet.ExpressionQuery(columnSearchAggregateDTO);
            return value.ToList();
        }
        public int CountMax(Expression<Func<TDbEntity, bool>> expression = null)
        {
            return expression==null? _dbSet.Count():_dbSet.Count(expression);
        }

        #endregion

        #region Async

        public async Task AddAsync(TDbEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void UpdateAsync(TDbEntity entity)
        {
            _dbSet.Update(entity);
        }

        public void RemoveAsync(TDbEntity entity)
        {
            _dbSet.Remove(entity) ;
        }
        public virtual async Task<TDbEntity> GetAsync(Expression<Func<TDbEntity, bool>> expression)
        {
            var value = await _dbSet.AsNoTracking().FirstOrDefaultAsync(expression);
            return value!;
        }

        public virtual async Task<ICollection<TDbEntity>> ListAsync(Expression<Func<TDbEntity, bool>> expression=null)
        {
            var value = expression==null?
                await _dbSet.AsNoTracking().ToListAsync() : await _dbSet.AsNoTracking().Where(expression).ToListAsync();

            return value;
        }

        public virtual async Task<ICollection<TDbEntity>> ListAsync(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {

            var value = _dbSet.ExpressionQuery(columnSearchAggregateDTO);

            return await value.ToListAsync();
        }

        public Task<int> CountMaxAsync(Expression<Func<TDbEntity, bool>> expression = null)
        {
            return expression==null?_dbSet.CountAsync(): _dbSet.CountAsync(expression);
        }
        #endregion
    }
    internal abstract class GenericRepository<TEntity, TDbEntity> : GenericRepository<TDbEntity>, IGenericRepository<TEntity, TDbEntity> 
        where TEntity : Entity
        where TDbEntity : DatabaseEntityModel
    {
        
        protected GenericRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext) 
        {
        }
        #region Sync
        public async void Add(TEntity entity)
        {
            var dbModel = await MapToDatabaseEntity(entity, true);
            base.Add(dbModel);
        }

        public async void Update(TEntity entity)
        {
            var dbModel = await MapToDatabaseEntity(entity, false);
            base.Update(dbModel);
        }

        public async void Remove(TEntity entity)
        {
            var dbModel = await MapToDatabaseEntity(entity,false);
            base.Remove(dbModel);
        }

        public int CountMax(Expression<Func<TDbEntity, bool>> expression = null)
        {
            return expression == null ? _dbSet.Count() : _dbSet.Count(expression);
        }
        #endregion

        #region Mapping
        protected virtual async Task<TEntity> MapToDomainEntity(TDbEntity entity, bool withRelations)
        {
            return await entity.MapToDomainEntity<TEntity, TDbEntity>(withRelations);
        }
        protected virtual async Task<TDbEntity> MapToDatabaseEntity(TEntity entity, bool mapRelationObjects)
        {
            return await entity.MapToDatabaseEntity<TEntity, TDbEntity>(mapRelationObjects);
        }
        protected virtual async Task<ICollection<TEntity>> MapToDomainEntity(ICollection<TDbEntity> entity, bool withRelations)
        {
            return await entity.MapToDomainEntity<TEntity, TDbEntity>(withRelations);
        }
        protected virtual async Task<ICollection<TDbEntity>> MapToDatabaseEntity(ICollection<TEntity> entity, bool mapRelationObjects)
        {
            return await entity.MapToDatabaseEntity<TEntity, TDbEntity>(mapRelationObjects);
        }
        #endregion
        #region Async

        public async virtual Task<TEntity> GetAsync(Expression<Func<TDbEntity, bool>> expression)
        {
            var value = await base.GetAsync(expression);

            return await this.MapToDomainEntity(value, true);
        }

        public async virtual Task<ICollection<TEntity>> ListAsync(Expression<Func<TDbEntity, bool>> expression)
        {

            var value = await base.ListAsync(expression);

            return await this.MapToDomainEntity(value, true);
        }
        public async virtual Task<ICollection<TEntity>> ListAsync(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {
            var value = await base.ListAsync(columnSearchAggregateDTO);

            return await this.MapToDomainEntity(value, true);
        }

        public virtual async Task<RepositoryResponse<ICollection<TEntity>>> ListAsyncWithMeta(Expression<Func<TDbEntity, bool>> expression = null)
        {
            int count = await CountMaxAsync();
            var value = await base.ListAsync(expression);
            var data = await this.MapToDomainEntity(value, true);
            var meta = new Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<TEntity>>(data,ref meta);
        }

        public virtual async Task<RepositoryResponse<ICollection<TEntity>>> ListAsyncWithMeta(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {
            int count = await CountMaxAsync();
            var value = await base.ListAsync(columnSearchAggregateDTO);
            var data = await this.MapToDomainEntity(value, true);
            var meta = new Meta { TotalItems = count };

            return new RepositoryResponse<ICollection<TEntity>>(data, ref meta);
        }

        public async Task AddAsync(TEntity entity)
        {
            var dbModel = await MapToDatabaseEntity(entity, false);
            await base.AddAsync(dbModel);
        }

        public async void RemoveAsync(TEntity entity)
        {
            var dbModel = await MapToDatabaseEntity(entity, false);
            base.RemoveAsync(dbModel);
        }

        public async void UpdateAsync(TEntity entity)
        {
            var dbModel = await MapToDatabaseEntity(entity, false);
            base.UpdateAsync(dbModel);
        }

        public TDbEntity GetDbEntity(Expression<Func<TDbEntity, bool>> expression)
        {
            return base.Get(expression);
        }

        public ICollection<TDbEntity> ListDbEntity(Expression<Func<TDbEntity, bool>> expression = null)
        {
            return base.List(expression);
        }

        public ICollection<TDbEntity> ListDbEntity(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {
            return base.List(columnSearchAggregateDTO);
        }

        public int CountMaxDbEntity(Expression<Func<TDbEntity, bool>> expression = null)
        {
            return base.CountMax(expression);
        }

        public async Task<TDbEntity> GetAsyncDbEntity(Expression<Func<TDbEntity, bool>> expression)
        {
            return await base.GetAsync(expression);  
        }

        public async Task<ICollection<TDbEntity>> ListAsyncDbEntity(Expression<Func<TDbEntity, bool>> expression = null)
        {
            return await base.ListAsync(expression);
        }

        public async Task<ICollection<TDbEntity>> ListAsyncDbEntity(ColumnSearchAggregateDTO? columnSearchAggregateDTO)
        {
            return await base.ListAsync(columnSearchAggregateDTO);
        }

        public async Task<int> CountMaxAsyncDbEntity(Expression<Func<TDbEntity, bool>> expression = null)
        {
            return await base.CountMaxAsync(expression);
        }




        #endregion
    }
}
