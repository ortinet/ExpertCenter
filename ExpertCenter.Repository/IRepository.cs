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
        IEnumerable<ColumnType> GetColumnTypes();
        ColumnType? GetColumnType(string code);
        IEnumerable<PriceList> GetPriceLists();
        PriceList? GetPriceList(int id);
        IEnumerable<UserColumn> GetUnickUserColumns();
        bool CreateProduct(Product product);
        bool CreatePriceList(PriceList priceList);
    }
}
