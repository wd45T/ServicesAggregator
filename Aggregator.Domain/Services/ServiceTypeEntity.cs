using Aggregator.Domain.Common;

namespace Aggregator.Domain.Services;

public class ServiceTypeEntity : BaseEntity
{
    public string Name { get; set; }

    public ServiceTypeEntity() { }

    public static ServiceTypeEntity Create(string name)
    {
        return new ServiceTypeEntity
        {
            Name = name
        };
    }
}