using Aggregator.Domain.Companies;
using Aggregator.Domain.Services;
using Aggregator.InterfaceAdapters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Reflection;

namespace Aggregator.Infrastructure.DataAccess;

public class UnitOfWork : DbContext, IUnitOfWork
{
    public UnitOfWork(DbContextOptions<UnitOfWork> options) : base(options) { }

    public DbSet<ServiceTypeEntity> ServiceTypes { get; set; }
    public DbSet<ServiceEntity> Services { get; set; }
    public DbSet<CompanyEntity> Companies { get; set; }

    private IDbContextTransaction? _transaction;

    public async Task BeginTransactionAsync()
    {
        if (_transaction is not null) return;

        _transaction = await Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction is null) return;

        try
        {
            await SaveChangesAsync();

            await _transaction.CommitAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();

            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction is null) return;

        await _transaction.RollbackAsync();

        await _transaction.DisposeAsync();

        _transaction = null;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.InitializeData();

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSnakeCaseNamingConvention();

        base.OnConfiguring(optionsBuilder);
    }
}