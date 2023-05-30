using Microsoft.EntityFrameworkCore;
using Ordering.Domain.Common;
using Ordering.Domain.Entities;

namespace Ordering.Infrastructure.Persistence;

public class OrderContext:DbContext
{
    public OrderContext(DbContextOptions<OrderContext> options):base(options)
    {
       

    }

    public DbSet<Order> Orders { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Order>()
            .Property(x => x.LastModifiedBy).IsRequired(false);

        modelBuilder.Entity<Order>()
            .Property(x => x.ModifiedDate).IsRequired(false);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<EntityBase>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreateDate=DateTime.Now;
                    entry.Entity.CreatedBy = "behnamhadipanah";
                    
                    break;
                case EntityState.Modified:
                    entry.Entity.ModifiedDate=DateTime.Now;
                    entry.Entity.LastModifiedBy = "behnamhadipanah";
                    break;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}