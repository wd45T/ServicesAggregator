using Aggregator.Domain.Companies;
using Aggregator.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Aggregator.Infrastructure.DataAccess;

public static class DatabaseMigratorExtensions
{
    public static void MigrateDatabase<T>(this IServiceProvider serviceProvider) where T : DbContext
    {
        using var scope = serviceProvider.CreateScope();

        using var dbContext = scope.ServiceProvider.GetRequiredService<T>();

        if (dbContext.Database.ProviderName == "Microsoft.EntityFrameworkCore.Sqlite") return;

        dbContext.Database.Migrate();
    }

    public static void InitializeData(this ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ServiceTypeEntity>().HasData(
        [
            new ServiceTypeEntity{ Id = 1, Name = "Cleaning", CreatedAt = DateTime.UtcNow },
            new ServiceTypeEntity{ Id = 2, Name = "Delivery", CreatedAt = DateTime.UtcNow },
            new ServiceTypeEntity{ Id = 3, Name = "Kitchen", CreatedAt = DateTime.UtcNow }
        ]);

        modelBuilder.Entity<CompanyEntity>().HasData(
        [
            new CompanyEntity
            {
                Id = 1,
                Name = "House Cleaning",
                Description = "Let's clean up your shit",
                LogoUrl = "https://comenian.org/wp-content/uploads/2023/04/istockphoto-1340208950-612x612-1.jpeg",
                CreatedAt = DateTime.UtcNow
            },
            new CompanyEntity
            {
                Id = 2,
                Name = "Cooking pizza",
                Description = "Enjoy our pizza",
                CreatedAt = DateTime.UtcNow
            },
            new CompanyEntity
            {
                Id = 3,
                Name = "Handyman",
                Description = "Our hands are not for boredom",
                LogoUrl = "https://media.istockphoto.com/id/1463132842/vector/wrench-in-hand-screwdriver-brush-repair-and-service-sign.jpg?s=612x612&w=0&k=20&c=RBWR7k6jh09E9UDXOqviT9hAaex4qmrqX-6gYPnEGbk=",
                CreatedAt = DateTime.UtcNow
            },
        ]);

        modelBuilder.Entity<ServiceEntity>().HasData(
        [
            new ServiceEntity
            {
                Id = 1,
                ServiceTypeId = 1,
                Name = "Let's clean",
                CompanyId = 1,
                Data = "{}",
                Description = "Let's clean up your problem",
                CreatedAt = DateTime.UtcNow
            },
            new ServiceEntity
            {
                Id = 2,
                ServiceTypeId = 2,
                Name = "Delivery",
                CompanyId = 2,
                Data = "{}",
                CreatedAt = DateTime.UtcNow
            },
            new ServiceEntity
            {
                Id = 3,
                ServiceTypeId = 1,
                Name = "Super Cleaning",
                CompanyId = 3,
                Data = "{}",
                CreatedAt = DateTime.UtcNow
            },
            new ServiceEntity
            {
                Id = 4,
                ServiceTypeId = 2,
                Name = "Super Delivery",
                CompanyId = 3,
                Data = "{}",
                CreatedAt = DateTime.UtcNow
            },
            new ServiceEntity
            {
                Id = 5,
                ServiceTypeId = 3,
                Name = "Super Kitchen",
                CompanyId = 3,
                Data = "{}",
                CreatedAt = DateTime.UtcNow
            }
        ]);
    }
}