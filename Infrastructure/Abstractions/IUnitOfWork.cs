using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Abstractions
{
    public interface IUnitOfWork
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken = default);
    }
}
