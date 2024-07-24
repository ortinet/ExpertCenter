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
        /// <summary>
        /// Ключ - id колонки
        /// </summary>
        public Dictionary<int, string?> Properties { get; set; } = new Dictionary<int, string?>();
        public List<UserColumn> UserColumns { get; set; } = new List<UserColumn>();
    }
}
