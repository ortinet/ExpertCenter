using ExpertCenter.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Repository.EntityFramework
{
    public class EFRepository : DbContext, IRepository
    {
        private DbSet<EFPriceList> PriceLists { get; set; } = null!;
        private DbSet<EFProduct> Products { get; set; } = null!;
        private DbSet<EFUserColumn> UserColumns { get; set; } = null!;
        private DbSet<EFUserColumnValue> UserColumnValues { get; set; } = null!;
        private DbSet<EFColumnType> ColumnTypes { get; set; } = null!;

        private readonly string _connectionString = "Server=localhost;Database=ExpertCenterDB;Trusted_Connection=True;";

        public EFRepository(string connectionString)
        {
            _connectionString = connectionString;

            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public bool CreatePriceList(PriceList priceList)
        {
            var entry = PriceLists.Add(priceList.ConvertToEF());
            SaveChanges();

            return entry.State == EntityState.Added;
        }

        public bool CreateProduct(Product product)
        {
            var entry = Products.Add(product.ConvertToEF());
            SaveChanges();

            return entry.State == EntityState.Added;
        }

        public bool DeleteProduct(int id)
        {
            int rowsAffected = Products.Where(product => product.Id == id).ExecuteDelete();
            SaveChanges();

            return rowsAffected == 1;
        }

        public ColumnType? GetColumnType(string code)
        {
            return ColumnTypes.FirstOrDefault(c => c.Code == code)?.Convert();
        }

        public IEnumerable<ColumnType> GetColumnTypes()
        {
            List<ColumnType> result = [];
            foreach (var efColumnType in ColumnTypes)
                result.Add(efColumnType.Convert());

            return result;
        }

        public PriceList? GetPriceList(int id)
        {
            return PriceLists.FirstOrDefault(x => x.Id == id)?.Convert();
        }

        public IEnumerable<PriceList> GetPriceLists()
        {
            List<PriceList> result = [];
            foreach (var efPriceList in PriceLists)
                result.Add(efPriceList.Convert());

            return result;
        }

        public IEnumerable<UserColumn> GetUnickUserColumns()
        {
            IEnumerable<EFUserColumn> efUserColumns = UserColumns.DistinctBy(column => new { column.Header, column.ColumnType.Code });

            List<UserColumn> result = new List<UserColumn>();
            foreach (var efUserColumn in efUserColumns)
                result.Add(efUserColumn.Convert());

            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
