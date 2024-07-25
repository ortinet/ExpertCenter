using System.ComponentModel.DataAnnotations.Schema;

namespace ExpertCenter.Repository.Models
{
    [Table("Products")]
    public class Product : ObjectBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int PriceListId { get; set; }
        public PriceList? PriceList { get; set; }
        public List<UserColumnValue> UserColumnValues { get; set; } = []; 
    }
}
