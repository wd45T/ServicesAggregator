namespace Aggregator.InterfaceAdapters;

public interface IDateTimeProvider
{
    public DateTimeOffset UtcNow { get; }
}