using ExpertCenter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Repository
{
    public interface IRepository
    {
        IEnumerable<ColumnTypeDTO> GetColumnTypes();
        ColumnTypeDTO? GetColumnType(string code);
        IEnumerable<PriceListDTO> GetPriceLists();
        PriceListDTO? GetPriceList(int id);
        IEnumerable<UserColumnDTO> GetUnickUserColumns();
        bool CreateProduct(ProductDTO product);
        bool CreatePriceList(PriceListDTO priceList);
        bool DeleteProduct(int id);
    }
}
