using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Domain
{
    public class ColumnTypeDTO : ObjectBaseDTO
    {
        public string Title { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
    }
}
