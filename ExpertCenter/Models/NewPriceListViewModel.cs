using ExpertCenter.Domain;

namespace ExpertCenter.Models
{
    public class NewPriceListViewModel
    {
        public PriceList PriceList { get; }
        public IEnumerable<ColumnType> ColumnTypes { get; }

        public NewPriceListViewModel(PriceList priceList, IEnumerable<ColumnType> availableColumnTypes)
        {
            PriceList = priceList;
            ColumnTypes = availableColumnTypes;
        }
    }
}
