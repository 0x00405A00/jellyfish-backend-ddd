using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _applicationDbContext.SaveChangesAsync(cancellationToken);
        }
        public async Task<IDbContextTransaction> BeginTransaction(CancellationToken cancellationToken)
        {
            return await _applicationDbContext.Database.BeginTransactionAsync();
        }
    }
    internal class UnitOfWorkMailService : UnitOfWork, IUnitOfWorkMailService
    {
        public UnitOfWorkMailService(ApplicationDbContextMailService applicationDbContext) : base(applicationDbContext)
        {
        }
    }
}
