using ExpertCenter.Repository.Models;
using System.Text;

namespace ExpertCenter.Repository.EntityFramework
{
    internal static class EFModelConverter
    {
        public static Domain.ColumnTypeDTO Convert(this ColumnType efColumnType)
        {
            return new Domain.ColumnTypeDTO()
            {
                Id = efColumnType.Id,
                Code = efColumnType.Code,
                Title = efColumnType.Title,
            };
        }

        public static Domain.UserColumnDTO Convert(this UserColumn efUserColumn)
        {
            return new Domain.UserColumnDTO()
            {
                Id = efUserColumn.Id,
                Header = efUserColumn.Header,
                Type = efUserColumn.ColumnType?.Convert(),
            };
        }

        public static Domain.ProductDTO Convert(this Product efProduct)
        {
            Dictionary<int, string?> userColumnValues = new Dictionary<int, string?>();
            foreach (var columnValue in efProduct.UserColumnValues)
            {
                userColumnValues.Add(columnValue.UserColumnId, columnValue.Value);
            }

            return new Domain.ProductDTO()
            {
                Id = efProduct.Id,
                Code = efProduct.Code,
                Name = efProduct.Name,
                PriceListId = efProduct.PriceListId,
                UserColumnValues = userColumnValues,
            };
        }

        public static Domain.PriceListDTO Convert(this PriceList efPriceList)
        {
            List<Domain.UserColumnDTO> userColumns = new List<Domain.UserColumnDTO>();
            foreach (var userColumn in efPriceList.UserColumns)
            {
                userColumns.Add(Convert(userColumn));
            }

            List<Domain.ProductDTO> products = new List<Domain.ProductDTO>();
            foreach (var product in efPriceList.Products)
            {
                products.Add(Convert(product));
            }

            return new Domain.PriceListDTO()
            {
                Id = efPriceList.Id,
                Name = efPriceList.Name ?? string.Empty,
                Columns = userColumns,
                Products = products,
            };
        }

        public static PriceList ConvertToEF(this Domain.PriceListDTO priceList)
        {
            List<Product> products = new List<Product>();
            foreach (var product in priceList.Products)
            {
                products.Add(ConvertToEF(product));
            }

            List<UserColumn> userColumns = new List<UserColumn>();
            foreach (var userColumn in priceList.Columns)
            {
                userColumns.Add(userColumn.ConvertToEF());
            }

            return new PriceList()
            {
                Id = priceList.Id,
                Name = priceList.Name,
                Products = products,
                UserColumns = userColumns,
            };
        }

        public static Product ConvertToEF(this Domain.ProductDTO product)
        {
            List<UserColumnValue> columnValues = new List<UserColumnValue>();
            foreach (var columnValue in product.UserColumnValues)
            {
                columnValues.Add(new UserColumnValue()
                {
                    UserColumnId = columnValue.Key,
                    ProductId = product.Id,
                    Value = columnValue.Value,
                });
            }

            return new Product()
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                PriceListId = product.PriceListId,
                UserColumnValues = columnValues,
            };
        }

        public static UserColumn ConvertToEF(this Domain.UserColumnDTO userColumn)
        {
            return new UserColumn()
            {
                Id = userColumn.Id,
                Header = userColumn.Header,
                ColumnTypeId = userColumn.Type.Id,
                PriceListId = userColumn.PriceListId,
            };
        }

        public static ColumnType ConvertToEF(this Domain.ColumnTypeDTO columnType)
        {
            return new ColumnType()
            {
                Id = columnType.Id,
                Code = columnType.Code,
                Title = columnType.Title,
            };
        }
    }
}
