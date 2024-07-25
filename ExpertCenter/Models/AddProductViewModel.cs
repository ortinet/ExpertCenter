using ExpertCenter.Domain;

namespace ExpertCenter.Models
{
    public class AddProductViewModel
    {
        public int PriceListId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public Dictionary<int, string> UserColumnValues { get; set; } = [];
        public IEnumerable<UserColumn> UserColumns { get; set; } = new List<UserColumn>();

        public AddProductViewModel(int priceListId, IEnumerable<UserColumn> userColumns)
        {
            PriceListId = priceListId;
            UserColumns = userColumns;
        }

        public Product ConvertToProduct()
        {
            List<UserColumnValue> columnValues = [];
            foreach (var columnValue in UserColumnValues)
            {
                columnValues.Add(new UserColumnValue()
                {
                    Value = columnValue.Value,
                    UserColumn = UserColumns.First(column => column.Id == columnValue.Key),
                });
            }

            return new Product()
            {
                Code = Code,
                PriceListId = PriceListId,
                Name = ProductName,
                UserColumnValues = columnValues,
            };

            throw new NotImplementedException();
        }
    }
}
