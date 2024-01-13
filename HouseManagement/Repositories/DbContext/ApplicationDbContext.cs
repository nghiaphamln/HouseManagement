using Helper;
using Microsoft.EntityFrameworkCore;
using Models.Entities;

namespace Repositories.DbContext;

public class ApplicationDbContext : Microsoft.EntityFrameworkCore.DbContext
{
    public DbSet<UserEntity> UserEntities { get; set; } = null!;
    public DbSet<GroupEntity> GroupEntities { get; set; } = null!;
    public DbSet<GroupDetailEntity> GroupDetailEntities { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured) return;

        var connectionString = AppSettings.Get("ConnectionStrings:DbConnection");

        optionsBuilder.UseNpgsql(connectionString, builder => { builder.MigrationsAssembly("Repositories"); });
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<UserEntity>();
        modelBuilder.Entity<GroupEntity>();
        modelBuilder.Entity<GroupDetailEntity>();
    }
}