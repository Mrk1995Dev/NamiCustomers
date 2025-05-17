using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace NamiCustomers.Persistence.Interceptors
{
    public class SoftDeleteInterceptor : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(
            DbContextEventData eventData,
            InterceptionResult<int> result)
        {
            if (eventData.Context is null) return result;

            foreach (var entry in eventData.Context.ChangeTracker.Entries())
            {
                var entityType = entry.Context.Model.FindEntityType(entry.Entity.GetType());
                var deleted = entityType.FindProperty("RemovedAt");
                var isRemoved = entityType.FindProperty("IsRemoved");

                if (entry.State == EntityState.Deleted && deleted is not null && isRemoved is not null)
                {
                    entry.Property("IsRemoved").CurrentValue = true;
                    entry.Property("LastModifiedAt").CurrentValue = DateTime.UtcNow;
                }
            }
            return result;
        }
    }
}
