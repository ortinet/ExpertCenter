using ExpertCenter.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExpertCenter.Repository.Models
{
    [Table("UserColumnValues")]
    internal class UserColumnValue : ObjectBase
    {
        public string? Value { get; set; }
        public int UserColumnId { get; set; }
        public UserColumn? UserColumn { get; set; }
        public int? ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
