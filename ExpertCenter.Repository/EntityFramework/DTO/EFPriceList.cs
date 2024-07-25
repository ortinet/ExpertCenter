using ExpertCenter.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Repository.EntityFramework
{
    internal class EFPriceList : ObjectBase
    {
        public string? Name { get; set; }
        public List<EFProduct> Products { get; set; } = [];
    }
}
