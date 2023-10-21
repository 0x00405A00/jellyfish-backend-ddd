using Domain.Primitives;
using Infrastructure.Abstractions;
using Infrastructure.Mapper;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    internal abstract class GenericRepository<TDbEntity> : IGenericRepository<TDbEntity>
        where TDbEntity : DatabaseEntityModel
    {
        protected readonly DbSet<TDbEntity> _dbSet;
        protected readonly ApplicationDbContext _context;
        protected readonly ApplicationDbContext applicationDbContext;

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
            var value = expression==null?
                _dbSet.AsNoTracking().ToList() : _dbSet.AsNoTracking().Where(expression).ToList();

            return value;
        }
        #endregion

        #region Async

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
        public void Add(TEntity entity)
        {
            var dbModel = MapToDatabaseEntity(entity, true);
            base.Add(dbModel);
        }

        public void Update(TEntity entity)
        {
            var dbModel = MapToDatabaseEntity(entity,false);
            base.Update(dbModel);
        }

        public void Remove(TEntity entity)
        {
            var dbModel = MapToDatabaseEntity(entity,false);
            base.Remove(dbModel);
        }

        TEntity IGenericRepository<TEntity, TDbEntity>.Get(Expression<Func<TDbEntity, bool>> expression)
        {
            var value = base.Get(expression);

            return this.MapToDomainEntity(value, true);
        }

        ICollection<TEntity> IGenericRepository<TEntity, TDbEntity>.List(Expression<Func<TDbEntity, bool>> expression = null)
        {
            var value = base.List(expression);

            return this.MapToDomainEntity(value, true);
        }
        #endregion

        #region Mapping
        protected virtual TEntity MapToDomainEntity(TDbEntity entity, bool withRelations)
        {
            return entity.MapToDomainEntity<TEntity, TDbEntity>(withRelations);
        }
        protected virtual TDbEntity MapToDatabaseEntity(TEntity entity, bool mapRelationObjects)
        {
            return entity.MapToDatabaseEntity<TEntity, TDbEntity>(mapRelationObjects);
        }
        protected virtual ICollection<TEntity> MapToDomainEntity(ICollection<TDbEntity> entity, bool withRelations)
        {
            return entity.MapToDomainEntity<TEntity, TDbEntity>(withRelations);
        }
        protected virtual ICollection<TDbEntity> MapToDatabaseEntity(ICollection<TEntity> entity, bool mapRelationObjects)
        {
            return entity.MapToDatabaseEntity<TEntity, TDbEntity>(mapRelationObjects);
        }
        #endregion
        #region Async
        public async virtual Task<TEntity> GetAsync(Expression<Func<TDbEntity, bool>> expression)
        {
            var value = await base.GetAsync(expression);

            return this.MapToDomainEntity(value, true);
        }

        public async virtual Task<ICollection<TEntity>> ListAsync(Expression<Func<TDbEntity, bool>> expression)
        {

            var value = await base.ListAsync(expression);

            return this.MapToDomainEntity(value, true);
        }
        #endregion
    }
}
