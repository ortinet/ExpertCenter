using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Domain
{
    public class Product : ObjectBase
    {
        public string Name { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public int PriceListId { get; set; } = -1;
        public Dictionary<UserColumn, string?> Properties { get; set; } = new Dictionary<UserColumn, string?>();
    }
}
