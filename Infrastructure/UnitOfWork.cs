using Domain.Entities.Chats;
using Domain.Primitives;
using Infrastructure.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Runtime.ConstrainedExecution;

namespace Infrastructure
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMediator mediator;

        public UnitOfWork(ApplicationDbContext applicationDbContext, IMediator mediator)
        {
            _applicationDbContext = applicationDbContext;
            this.mediator = mediator;
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            int result = 0;
            var changes = _applicationDbContext.ChangeTracker.Entries();
            try
            {

                result = await _applicationDbContext.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
            }

            foreach (var change in changes)
            {
                if (change.Entity is Entity entity)
                {
                    var domainEvents = entity.GetDomainEvents();
                    if (!domainEvents.Any())
                    {
                        continue;
                    }
                    domainEvents.ForEach(e =>
                    {
                        mediator.Publish(e);
                    });
                }
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
        public UnitOfWorkMailService(ApplicationDbContextMailService applicationDbContext, IMediator mediator) : base(applicationDbContext,mediator)
        {
        }
    }
}
