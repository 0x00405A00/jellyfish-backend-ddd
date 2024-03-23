using Domain.Primitives;
using Domain.Specification;
using System.Linq.Expressions;

namespace Domain.Repository
{
    public interface IGenericRepository<TEntity>
        where TEntity : Entity

    {
        public void Add(TEntity entity);
        public void Remove(TEntity entity);
        public void Update(TEntity entity);
        public void Attach(TEntity entity);

        public int CountMax(Expression<Func<TEntity, bool>> expression = null);
        public Task<int> CountMaxAsync(Expression<Func<TEntity, bool>> expression = null);
        public IEnumerable<TEntity> GetBySpecification(ISpecification<TEntity> specification);
        public Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);
        public Task<ICollection<TEntity>> ListAsync(Expression<Func<TEntity, bool>> expression = null);
    }
}