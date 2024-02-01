using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Shared.Infrastructure.EFCore.Interceptors
{
    public class DbSaveChangesInterceptor : SaveChangesInterceptor
    {
        public DbSaveChangesInterceptor()
        {

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

            var res = base.SavingChangesAsync(eventData, result, cancellationToken);
            return res;
        }
    }
}
