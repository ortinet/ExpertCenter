using ExpertCenter.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpertCenter.Repository.EntityFramework
{
    [Table("UserColumnValues")]
    internal class EFUserColumnValue : ObjectBase
    {
        public string? Value { get; set; }

        public int UserColumnId { get; set; }
        public int ProductId { get; set; }
    }
}
