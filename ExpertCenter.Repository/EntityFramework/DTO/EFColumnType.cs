using ExpertCenter.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpertCenter.Repository.EntityFramework
{
    [Table("ColumnTypes")]
    internal class EFColumnType : ObjectBase
    {
        public string Title { get; set; }
        public string Code { get; set; }
        public EFUserColumnValue UserColumnValue { get; set; }
    }
}
