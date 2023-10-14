using Domain.Primitives;
using Infrastructure.Abstractions;
using System.Linq.Expressions;

namespace Infrastructure.Repository
{
    public interface IGenericRepository<TDbEntity>
        where TDbEntity : DatabaseEntityModel

    {
        public void Add(TDbEntity entity);
        public void Remove(TDbEntity entity);
        public void Update(TDbEntity entity);
        public TDbEntity Get(Expression<Func<TDbEntity, bool>> expression);
        public ICollection<TDbEntity> List(Expression<Func<TDbEntity, bool>> expression);

        public Task<TDbEntity> GetAsync(Expression<Func<TDbEntity, bool>> expression);
        public Task<ICollection<TDbEntity>> ListAsync(Expression<Func<TDbEntity, bool>> expression);
    }
    public interface IGenericRepository<TEntity,TDbEntity>
        where TEntity : Entity
        where TDbEntity : DatabaseEntityModel

    {
        public void Add(TEntity entity);
        public void Remove(TEntity entity);
        public void Update(TEntity entity);
        public TEntity Get(Expression<Func<TDbEntity, bool>> expression);
        public ICollection<TEntity> List(Expression<Func<TDbEntity, bool>> expression);

        public Task<TEntity> GetAsync(Expression<Func<TDbEntity, bool>> expression);
        public Task<ICollection<TEntity>> ListAsync(Expression<Func<TDbEntity, bool>> expression);
    }
}