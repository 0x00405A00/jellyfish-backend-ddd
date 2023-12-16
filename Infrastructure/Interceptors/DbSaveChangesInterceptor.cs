using Domain.Primitives;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors
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
            var changes = dbContext.ChangeTracker.Entries();
            foreach( var change in changes )
            {

            }
            //For Auditin Value set
            #region Auditing
            var createdEntries = dbContext.ChangeTracker.Entries < IAuditibleCreateEntity>();
            foreach (var entry in createdEntries)
            {
                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Added)
                {
                    entry.Property(x => x.CreatedTime).CurrentValue = DateTime.Now;
                }
            }
            var modifiedEntries = dbContext.ChangeTracker.Entries<IAuditibleModifiedEntity>();
            foreach (var entry in modifiedEntries)
            {
                if (entry.State == Microsoft.EntityFrameworkCore.EntityState.Modified)
                {
                    entry.Property(x => x.LastModifiedTime).CurrentValue = DateTime.Now;
                }
            }
            #endregion
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
    }
}
