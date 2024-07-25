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
            throw new NotImplementedException();
        }

        public bool CreateProduct(Product product)
        {
            throw new NotImplementedException();
        }

        public bool DeleteProduct(int id)
        {
            throw new NotImplementedException();
        }

        public ColumnType? GetColumnType(string code)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ColumnType> GetColumnTypes()
        {
            throw new NotImplementedException();
        }

        public PriceList? GetPriceList(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<PriceList> GetPriceLists()
        {
            List<PriceList> result = [];

            //foreach (var efPriceList in PriceLists.ToList())
            //{
            //    PriceList priceList = new PriceList() { }
            //}

            return result;
        }

        public IEnumerable<UserColumn> GetUnickUserColumns()
        {
            throw new NotImplementedException();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
    }
}
