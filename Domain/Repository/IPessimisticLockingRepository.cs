using Domain.Primitives;
using System.Linq.Expressions;

namespace Domain.Repository
{
    public interface IPessimisticLockingRepository<TEntity> where TEntity : Entity
    {
        public Task<ICollection<TEntity>> ListWithPessimisticLockAsync(Expression<Func<TEntity, bool>> expression = null);
    }
}