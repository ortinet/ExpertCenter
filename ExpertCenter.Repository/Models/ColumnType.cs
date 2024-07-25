using ExpertCenter.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpertCenter.Repository.Models
{
    [Table("ColumnTypes")]
    public class ColumnType : ObjectBase
    {
        public string Title { get; set; }
        public string Code { get; set; }
    }
}
