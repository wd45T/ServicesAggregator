using Aggregator.Domain.Common;
using Aggregator.InterfaceAdapters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Aggregator.Infrastructure.DataAccess.Interceptors;

public class DateTimeInterceptor : SaveChangesInterceptor
{
    private readonly IDateTimeProvider _dateTimeProvider;

    public DateTimeInterceptor(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ChangeEntityTimestamps(eventData);

        return base.SavingChanges(eventData, result);

    }

    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        ChangeEntityTimestamps(eventData);

        return await base.SavingChangesAsync(eventData, result);
    }

    private void ChangeEntityTimestamps(DbContextEventData eventData)
    {
        var utcNow = _dateTimeProvider.UtcNow;

        foreach (var entry in eventData.Context!.ChangeTracker.Entries())
        {
            if (entry.Entity is BaseEntity entity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = utcNow;
                        break;

                    case EntityState.Modified:
                        entity.UpdatedAt = utcNow;
                        break;
                }
            }
        }
    }
}
