using Aggregator.Domain.Common;
using Aggregator.Domain.Companies;

namespace Aggregator.Domain.Services;

public class ServiceEntity : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public ServiceDataNode ServiceData { get; set; }
    public ServiceTypeEntity ServiceType { get; set; }
    public CompanyEntity Company { get; set; }
    public long CompanyId { get; set; }
    public long ServiceTypeId { get; set; }

    private ServiceEntity() { }

    public static ServiceEntity Create(string name, string? description, CompanyEntity company, ServiceTypeEntity serviceType, ServiceDataNode serviceData)
    {
        return new ServiceEntity
        {
            Name = name,
            Description = description,
            Company = company,
            ServiceType = serviceType,
            ServiceData = serviceData
        };
    }
}