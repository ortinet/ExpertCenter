using ExpertCenter.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpertCenter.Repository.Models
{
    [Table("UserColumns")]
    internal class UserColumn : ObjectBase
    {
        public string Header { get; set; }
        public int ColumnTypeId { get; set; }
        public ColumnType? ColumnType { get; set; }
        public int PriceListId { get; set; }
        public PriceList? PriceList { get; set; }
        public List<UserColumnValue> UserColumnValues { get; set; } = [];
    }
}
