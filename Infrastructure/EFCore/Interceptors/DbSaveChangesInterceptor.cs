using Domain.Primitives;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.EFCore.Interceptors
{
    public class DbSaveChangesInterceptor : SaveChangesInterceptor
    {
        private readonly IMediator mediator;

        public DbSaveChangesInterceptor(IMediator mediator)
        {
            this.mediator = mediator;
        }
        public override ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            return base.SavedChangesAsync(eventData, result, cancellationToken);
        }
        public override int SavedChanges(SaveChangesCompletedEventData eventData, int result)
        {
            return base.SavedChanges(eventData, result);
        }
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {

            var dbContext = eventData.Context;
            if (dbContext == null)
            {

                return base.SavingChangesAsync(eventData, result, cancellationToken);
            }
            var changes = dbContext.ChangeTracker.Entries();
            foreach (var change in changes)
            {
                if(change.Entity is Entity<Identification> entity)
                {
                    if (!entity.DomainEvents.Any())
                    {
                        continue;
                    }
                    entity.DomainEvents.ToList().ForEach(e =>
                    {
                        mediator.Publish(e);
                    });
                }
            }
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
