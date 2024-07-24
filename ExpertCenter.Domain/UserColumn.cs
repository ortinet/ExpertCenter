using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Domain
{
    public class UserColumn : ObjectBase
    {
        public string Header { get; set; } = string.Empty;
        public ColumnType Type { get; set; }
    }
}
