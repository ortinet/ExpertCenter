using ExpertCenter.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpertCenter.Repository.EntityFramework
{
    [Table("Products")]
    internal class EFProduct : ObjectBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int PriceListId { get; set; }
        public EFPriceList? PriceList { get; set; }
        public List<EFUserColumnValue> UserColumnsValues { get; set; } = [];
    }
}
