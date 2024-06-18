using Aggregator.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.Infrastructure.DataAccess.Configurations.EntitiesConfig;

internal class ServiceTypeEntityConfig : IEntityTypeConfiguration<ServiceTypeEntity>
{
    public void Configure(EntityTypeBuilder<ServiceTypeEntity> builder)
    {
        builder.ToTable("service_types", Schemas.Services);

        BaseEntityConfig.Configure(builder);

        builder.Property(x => x.Name).IsRequired();

        builder.HasIndex(e => e.Name)
            .IsUnique();
    }
}