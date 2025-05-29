using Fireworks.Application.common;
using Fireworks.Domain.Entities;
using Fireworks.Domain.Identity.Entities;
using Fireworks.Infrastructure.Persistence.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Fireworks.Infrastructure.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options),
        IApplicationDbContext
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<UserLoginLog> UserLoginLogs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyIdentityTableNamingConvention();

        builder.Entity<RefreshToken>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasOne(x => x.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
            entity.Property(x => x.Token).IsRequired();
            entity.Property(x => x.CreatedByIp).IsRequired();
        });

        builder.Entity<UserLoginLog>(entity =>
        {
            entity.HasKey(x => x.Id);
            entity.HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}