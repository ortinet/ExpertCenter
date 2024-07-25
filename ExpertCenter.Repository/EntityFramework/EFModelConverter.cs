using ExpertCenter.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Repository.EntityFramework
{
    internal static class EFModelConverter
    {
        public static ColumnType Convert(this EFColumnType efColumnType)
        {
            return new ColumnType()
            {
                Id = efColumnType.Id,
                Code = efColumnType.Code,
                Title = efColumnType.Title,
            };
        }

        public static UserColumn Convert(this EFUserColumn efUserColumn)
        {
            return new UserColumn()
            {
                Id = efUserColumn.Id,
                Header = efUserColumn.Header,
                Type = efUserColumn.ColumnType?.Convert(),
            };
        }

        public static Product Convert(this EFProduct efProduct)
        {
            Dictionary<int, string?> userColumnValues = new Dictionary<int, string?>();
            foreach (var columnValue in efProduct.UserColumnsValues)
            {
                userColumnValues.Add(columnValue.UserColumnId, columnValue.Value);
            }

            return new Product()
            {
                Id = efProduct.Id,
                Code = efProduct.Code,
                Name = efProduct.Name,
                PriceListId = efProduct.PriceListId,
                UserColumnValues = userColumnValues,
            };
        }

        public static PriceList Convert(this EFPriceList efPriceList)
        {
            List<UserColumn> userColumns = new List<UserColumn>();
            foreach (var userColumn in efPriceList.UserColumns)
            {
                userColumns.Add(Convert(userColumn));
            }

            List<Product> products = new List<Product>();
            foreach (var product in efPriceList.Products)
            {
                products.Add(Convert(product));
            }

            return new PriceList()
            {
                Id = efPriceList.Id,
                Name = efPriceList.Name ?? string.Empty,
                Columns = userColumns,
                Products = products,
            };
        }

        public static EFPriceList ConvertToEF(this PriceList priceList)
        {
            List<EFProduct> products = new List<EFProduct>();
            foreach (var product in priceList.Products)
            {
                products.Add(ConvertToEF(product));
            }

            List<EFUserColumn> userColumns = new List<EFUserColumn>();
            foreach (var userColumn in priceList.Columns)
            {
                userColumns.Add(userColumn.ConvertToEF());
            }

            return new EFPriceList()
            {
                Id = priceList.Id,
                Name = priceList.Name,
                Products = products,
                UserColumns = userColumns,
            };
        }

        public static EFProduct ConvertToEF(this Product product)
        {
            List<EFUserColumnValue> columnValues = new List<EFUserColumnValue>();
            foreach (var columnValue in product.UserColumnValues)
            {
                columnValues.Add(new EFUserColumnValue()
                {
                    UserColumnId = columnValue.Key,
                    ProductId = product.Id,
                    Value = columnValue.Value,
                });
            }

            return new EFProduct()
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                PriceListId = product.PriceListId,
                UserColumnsValues = columnValues,
            };
        }

        public static EFUserColumn ConvertToEF(this UserColumn userColumn)
        {
            return new EFUserColumn()
            {
                Id = userColumn.Id,
                Header = userColumn.Header,
                ColumnTypeId = userColumn.Type.Id,
                PriceListId = userColumn.PriceListId,
                ColumnType = ConvertToEF(userColumn.Type),
            };
        }

        public static EFColumnType ConvertToEF(this ColumnType columnType)
        {
            return new EFColumnType()
            {
                Id = columnType.Id,
                Code = columnType.Code,
                Title = columnType.Title,
            };
        }
    }
}
