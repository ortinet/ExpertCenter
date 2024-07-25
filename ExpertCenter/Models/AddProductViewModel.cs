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
        public IEnumerable<UserColumnDTO> UserColumns { get; set; } = new List<UserColumnDTO>();

        public AddProductViewModel() { }

        public AddProductViewModel(int priceListId, IEnumerable<UserColumnDTO> userColumns)
        {
            PriceListId = priceListId;
            UserColumns = userColumns;
            
            UserColumnValues = new Dictionary<int, string?>();
            foreach (var column in UserColumns)
            {
                UserColumnValues.Add(column.Id, null);
            }
        }

        public ProductDTO ConvertToProduct()
        {
            return new ProductDTO()
            {
                Code = Code,
                PriceListId = PriceListId,
                Name = ProductName,
                UserColumnValues = UserColumnValues,
            };
        }
    }
}