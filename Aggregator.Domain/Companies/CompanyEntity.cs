using Aggregator.Domain.Common;
using Aggregator.Domain.Services;

namespace Aggregator.Domain.Companies;

public class CompanyEntity : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public string? LogoUrl { get; set; }
    public IReadOnlyCollection<ServiceEntity> Services => _services.AsReadOnly();

    private List<ServiceEntity> _services = [];

    private CompanyEntity() { }

    public static CompanyEntity Create(string name, string? description = null, string? logoUrl = null)
    {
        return new CompanyEntity
        {
            Name = name,
            Description = description,
            LogoUrl = logoUrl,
        };
    }

    public void AddService(ServiceEntity serviceEntity)
    {
        _services.Add(serviceEntity);
    }

    public void RemoveService(ServiceEntity serviceEntity)
    {
        _services.Remove(serviceEntity);
    }
}