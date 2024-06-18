using Aggregator.Domain.Companies;
using Aggregator.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace Aggregator.InterfaceAdapters;

public interface IUnitOfWork : IDisposable
{
    DbSet<ServiceTypeEntity> ServiceTypes { get; }
    DbSet<ServiceEntity> Services { get; }
    DbSet<CompanyEntity> Companies { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}