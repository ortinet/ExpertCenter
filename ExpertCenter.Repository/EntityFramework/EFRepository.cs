using ExpertCenter.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace ExpertCenter.Repository.EntityFramework
{
    public class EFRepository : IRepository
    {
        private readonly ExpertCenterDbContext _dbContext;

        public EFRepository(IDbContextFactory<ExpertCenterDbContext> dbContextFactory)
        {
            _dbContext = dbContextFactory.CreateDbContext();
        }

        public bool CreatePriceList(Domain.PriceListDTO priceList)
        {
            try
            {
                var entry = _dbContext.PriceLists.Add(priceList.ConvertToEF());
                _dbContext.SaveChanges();

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
                var entry = _dbContext.Products.Add(product.ConvertToEF());
                _dbContext.SaveChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteProduct(int id)
        {
            _dbContext.UserColumnValues.Where(columnValue => columnValue.ProductId == id).ExecuteDelete();
            int rowsAffected = _dbContext.Products.Where(product => product.Id == id).ExecuteDelete();
            _dbContext.SaveChanges();

            return rowsAffected == 1;
        }

        public Domain.ColumnTypeDTO? GetColumnType(string code)
        {
            return _dbContext.ColumnTypes.FirstOrDefault(c => c.Code == code)?.Convert();
        }

        public IEnumerable<Domain.ColumnTypeDTO> GetColumnTypes()
        {
            List<Domain.ColumnTypeDTO> result = [];
            foreach (var efColumnType in _dbContext.ColumnTypes.ToList())
                result.Add(efColumnType.Convert());

            return result;
        }

        public Domain.PriceListDTO? GetPriceList(int id)
        {
            return _dbContext.PriceLists
                .Include(priceList => priceList.UserColumns)
                    .ThenInclude(column => column.ColumnType)
                .Include(priceList => priceList.Products)
                    .ThenInclude(product => product.UserColumnValues)
                .FirstOrDefault(x => x.Id == id)?.Convert();
        }

        public IEnumerable<Domain.PriceListDTO> GetPriceLists()
        {
            List<Domain.PriceListDTO> result = [];
            foreach (var efPriceList in _dbContext.PriceLists.ToList())
                result.Add(efPriceList.Convert());

            return result;
        }

        public IEnumerable<Domain.UserColumnDTO> GetUnickUserColumns()
        {
            IEnumerable<UserColumn> efUserColumns =
                _dbContext.UserColumns.Include(column => column.ColumnType).AsEnumerable()
                .DistinctBy(column => new { column.Header, column.ColumnTypeId });

            List<Domain.UserColumnDTO> result = new List<Domain.UserColumnDTO>();
            foreach (var efUserColumn in efUserColumns.ToList())
                result.Add(efUserColumn.Convert());

            return result;
        }
    }
}