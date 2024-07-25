using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Domain
{
    public class PriceListDTO : ObjectBaseDTO
    {
        [Required(ErrorMessage = "Укажите имя прайс-листа")]
        public string Name { get; set; } = string.Empty;
        public List<ProductDTO> Products { get; set; } = new List<ProductDTO>();
        public List<UserColumnDTO> Columns { get; set; } = new List<UserColumnDTO>();
    }
}
