using ExpertCenter.Domain;
using System.ComponentModel.DataAnnotations;

namespace ExpertCenter.Models
{
    public class AddProductViewModel
    {
        public int PriceListId { get; set; }

        [Required(ErrorMessage = "Укажите название товара")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Укажите код товара")]
        public string Code { get; set; } = string.Empty;

        public Dictionary<int, string?> UserColumnValues { get; set; } = [];
        public IEnumerable<UserColumn> UserColumns { get; set; } = new List<UserColumn>();

        public AddProductViewModel() { }

        public AddProductViewModel(int priceListId, IEnumerable<UserColumn> userColumns)
        {
            PriceListId = priceListId;
            UserColumns = userColumns;
            
            UserColumnValues = new Dictionary<int, string?>();
            foreach (var column in UserColumns)
            {
                UserColumnValues.Add(column.Id, null);
            }
        }

        public Product ConvertToProduct()
        {
            return new Product()
            {
                Code = Code,
                PriceListId = PriceListId,
                Name = ProductName,
                UserColumnValues = UserColumnValues,
            };
        }
    }
}