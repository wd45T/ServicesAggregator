using Aggregator.InterfaceAdapters;

namespace Aggregator.Infrastructure.Utils;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTime.UtcNow;
}
