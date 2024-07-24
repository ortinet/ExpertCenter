using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Domain
{
    public class PriceList : ObjectBase
    {
        public string Name { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new List<Product>();
        public List<UserColumn> Columns { get; set; } = new List<UserColumn>();
    }
}
