namespace Aggregator.Domain.Services;

public class ServiceDataNode
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public string? ImageUrl  { get; set; }
    public List<ServiceDataNode> Children { get; set; } = [];
}
