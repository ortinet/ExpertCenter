using ExpertCenter.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpertCenter.Repository.EntityFramework
{
    [Table("UserColumns")]
    internal class EFUserColumn : ObjectBase
    {
        public string Header { get; set; }
        public int ColumnTypeId { get; set; }
        public EFColumnType? ColumnType { get; set; }
        public int PriceListId { get; set; }
        public EFPriceList? PriceList { get; set; }
    }
}
