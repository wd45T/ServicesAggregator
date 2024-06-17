using Aggregator.Domain.Common;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aggregator.Infrastructure.DataAccess.Configurations.EntitiesConfig;

internal static class BaseEntityConfig
{
    public static void Configure<TEntity>(EntityTypeBuilder<TEntity> builder) where TEntity : BaseEntity
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.CreatedAt);

        builder.Property(x => x.UpdatedAt);

        builder.Property(x => x.DeletedAt);
    }
}
