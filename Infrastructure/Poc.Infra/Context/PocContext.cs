using Poc.App.Services;
using Poc.Domain.Core;
using Poc.Infra.Configurations;

namespace Poc.Infra.Context;

public class PocContext(DbContextOptions<PocContext> options, IUserService userService) : DbContext(options)
{
    private readonly IUserService _userService = userService;
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseConfiguration<>).Assembly);
    }

    public override int SaveChanges()
    {
        SaveChangeAdditionInfo();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        SaveChangeAdditionInfo();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void SaveChangeAdditionInfo()
    {
        var entries = ChangeTracker.Entries()
           .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity is BaseEntity entity)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entity.CreatedAt = DateTime.UtcNow;
                        entity.CreatedBy = _userService.UserName ?? "System";
                        break;
                    case EntityState.Modified:
                        entity.UpdatedAt = DateTime.UtcNow;
                        entity.UpdatedBy = _userService.UserName ?? "System";
                        break;
                }
            }
        }

    }
}