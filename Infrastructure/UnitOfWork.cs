using Infrastructure.Abstractions;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

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
}
