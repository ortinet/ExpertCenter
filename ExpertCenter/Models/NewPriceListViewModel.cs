using ExpertCenter.Domain;

namespace ExpertCenter.Models
{
    public class NewPriceListViewModel
    {
        public PriceListDTO PriceList { get; }
        public IEnumerable<ColumnTypeDTO> ColumnTypes { get; }

        public NewPriceListViewModel(PriceListDTO priceList, IEnumerable<ColumnTypeDTO> availableColumnTypes)
        {
            PriceList = priceList;
            ColumnTypes = availableColumnTypes;
        }
    }
}
