using Aggregator.Domain.Companies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.Infrastructure.DataAccess.Configurations.EntitiesConfig;

internal class CompanyEntityConfig : IEntityTypeConfiguration<CompanyEntity>
{
    public void Configure(EntityTypeBuilder<CompanyEntity> builder)
    {
        builder.ToTable("companies", Schemas.Companies);

        BaseEntityConfig.Configure(builder);

        builder.Property(x => x.Name)
            .IsRequired();

        builder.Property(x => x.Description);

        builder.Property(x => x.LogoUrl);

        builder.HasMany(e => e.Services)
           .WithOne(s => s.Company)
           .HasForeignKey(s => s.CompanyId)
           .IsRequired();
    }
}
