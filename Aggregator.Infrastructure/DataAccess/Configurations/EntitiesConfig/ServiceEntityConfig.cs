﻿using Aggregator.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.Infrastructure.DataAccess.Configurations.EntitiesConfig;

internal class ServiceEntityConfig : IEntityTypeConfiguration<ServiceEntity>
{
    public void Configure(EntityTypeBuilder<ServiceEntity> builder)
    {
        builder.ToTable("services", Schemas.Services);

        BaseEntityConfig.Configure(builder);

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Description);

        builder.Property(x => x.CompanyId);

        builder.Property(x => x.ServiceTypeId);

        builder.OwnsOne(p => p.ServiceData, serviceData =>
        {
            serviceData.ToJson();

            serviceData.OwnsMany(s => s.Children, node =>
            {
                node.ToJson();
                // Depth increases application startup time
                ConfigureChildren(node, 10);
            });
        });

        builder.HasOne(e => e.ServiceType)
            .WithMany()
            .HasForeignKey(e => e.ServiceTypeId)
            .IsRequired();

        builder.HasOne(e => e.Company)
            .WithMany(c => c.Services)
            .HasForeignKey(e => e.CompanyId)
            .IsRequired();

        builder.HasIndex(e => new { e.CompanyId, e.ServiceTypeId })
            .IsUnique();
    }

    public void ConfigureChildren(OwnedNavigationBuilder<ServiceDataNode, ServiceDataNode> node, int maxDepth)
    {
        if (maxDepth <= 0) return;
        
        node.OwnsMany(x => x.Children, child =>
        {
            child.ToJson();

            ConfigureChildren(child, --maxDepth);
        });
    }
}