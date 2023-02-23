
using Shoping.Application.Common.Interfaces;
using Shoping.Domain;
using Shoping.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Shoping.Persistence;
public class AppDbContext : IdentityDbContext<User>
{
    private readonly CurrentUser _user;

    public AppDbContext(
        DbContextOptions<AppDbContext> options,
        ICurrentUserService currentUserService) : base(options)
    {
        _user = currentUserService.User;
    }

 
    
    public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();
    public DbSet<Delivery> Deliveries => Set<Delivery>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _user.Id;
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    break;

                case EntityState.Modified:
                    entry.Entity.LastModifiedBy = _user.Id;
                    entry.Entity.LastModifiedByAt = DateTime.UtcNow;
                    break;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
 
        base.OnModelCreating(builder);
    }
}
