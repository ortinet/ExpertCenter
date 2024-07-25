using ExpertCenter.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpertCenter.Repository.EntityFramework
{
    public class ExpertCenterDbContext : DbContext
    {
        public DbSet<PriceList> PriceLists { get; set; } = null!;
        public DbSet<Product> Products { get; set; } = null!;
        public DbSet<UserColumn> UserColumns { get; set; } = null!;
        public DbSet<UserColumnValue> UserColumnValues { get; set; } = null!;
        public DbSet<ColumnType> ColumnTypes { get; set; } = null!;

        public ExpertCenterDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserColumnValue>()
            .HasOne(columnValue => columnValue.Product)
            .WithMany(product => product.UserColumnValues)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
