using Microsoft.EntityFrameworkCore;
using YoussefElsayedTask2.Models;

namespace YoussefElsayedTask2.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<Product>(e =>
        {
            e.Property(p => p.Name).HasMaxLength(200).IsRequired();
            e.Property(p => p.Price).HasPrecision(18, 2);
            e.Property(p => p.RowVersion).IsRowVersion();

            e.HasIndex(p => p.Name);
            e.HasQueryFilter(p => !p.IsDeleted);

            e.HasOne(p => p.Category)
             .WithMany(c => c.Products)
             .HasForeignKey(p => p.CategoryId);
        });

        b.Entity<Category>().Property(c => c.Name).HasMaxLength(100).IsRequired();
    }
}
