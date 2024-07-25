using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Domain
{
    public class Product : ObjectBase
    {
        [Required(ErrorMessage = "Укажите название товара")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Укажите код товара")]
        public string Code { get; set; }

        [Range(0, int.MaxValue)]
        public int PriceListId { get; set; } = -1;
        public Dictionary<int, string?> UserColumnValues { get; set; } = [];
    }
}
