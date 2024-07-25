using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Domain
{
    public class UserColumnValue : ObjectBase
    {
        public string? Value { get; set; }
        public UserColumn UserColumn { get; set; }
    }
}
