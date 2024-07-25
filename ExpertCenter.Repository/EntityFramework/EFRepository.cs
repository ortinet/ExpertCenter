using ExpertCenter.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ExpertCenter.Repository.EntityFramework
{
    public class EFRepository : DbContext, IRepository
    {
        private DbSet<PriceList> PriceLists { get; set; } = null!;
        private DbSet<Product> Products { get; set; } = null!;
        private DbSet<UserColumn> UserColumns { get; set; } = null!;
        private DbSet<UserColumnValue> UserColumnValues { get; set; } = null!;
        private DbSet<ColumnType> ColumnTypes { get; set; } = null!;

        private readonly string _connectionString = "Server=localhost;Database=ExpertCenterDB;Trusted_Connection=true;TrustServerCertificate=True";

        public EFRepository()
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        public EFRepository(string connectionString)
        {
            _connectionString = connectionString;

            Database.EnsureCreated();
        }

        public bool CreatePriceList(Domain.PriceListDTO priceList)
        {
            try
            {
                var entry = PriceLists.Add(priceList.ConvertToEF());
                SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateProduct(Domain.ProductDTO product)
        {
            try
            {
                var entry = Products.Add(product.ConvertToEF());
                SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            UserColumnValues.Where(columnValue => columnValue.ProductId == id).ExecuteDelete();
            int rowsAffected = Products.Where(product => product.Id == id).ExecuteDelete();
            SaveChanges();

            return rowsAffected == 1;
        }

        public Domain.ColumnTypeDTO? GetColumnType(string code)
        {
            return ColumnTypes.FirstOrDefault(c => c.Code == code)?.Convert();
        }

        public IEnumerable<Domain.ColumnTypeDTO> GetColumnTypes()
        {
            List<Domain.ColumnTypeDTO> result = [];
            foreach (var efColumnType in ColumnTypes.ToList())
                result.Add(efColumnType.Convert());

            return result;
        }

        public Domain.PriceListDTO? GetPriceList(int id)
        {
            return PriceLists
                .Include(priceList => priceList.UserColumns)
                    .ThenInclude(column => column.ColumnType)
                .Include(priceList => priceList.Products)
                    .ThenInclude(product => product.UserColumnValues)
                .FirstOrDefault(x => x.Id == id)?.Convert();
        }

        public IEnumerable<Domain.PriceListDTO> GetPriceLists()
        {
            List<Domain.PriceListDTO> result = [];
            foreach (var efPriceList in PriceLists.ToList())
                result.Add(efPriceList.Convert());

            return result;
        }

        public IEnumerable<Domain.UserColumnDTO> GetUnickUserColumns()
        {
            IEnumerable<UserColumn> efUserColumns = 
                UserColumns.Include(column => column.ColumnType).AsEnumerable()
                .DistinctBy(column => new { column.Header, column.ColumnTypeId });

            List<Domain.UserColumnDTO> result = new List<Domain.UserColumnDTO>();
            foreach (var efUserColumn in efUserColumns.ToList())
                result.Add(efUserColumn.Convert());

            return result;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserColumnValue>()
            .HasOne(columnValue => columnValue.Product)
            .WithMany(product => product.UserColumnValues)
            .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<UserColumnValue>()
            //.HasOne(columnValue => columnValue.UserColumn)
            //.WithMany(column => column.UserColumnValues)
            //.OnDelete(DeleteBehavior.Cascade);
        }
    }
}