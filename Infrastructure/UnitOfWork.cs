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
            int result = 0;
            try
            {

                result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {

            }
            return result;
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
