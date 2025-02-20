﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Domain
{
    public class UserColumnDTO : ObjectBaseDTO
    {
        [Required]
        public string Header { get; set; } = string.Empty;
        [Required]
        public ColumnTypeDTO? Type { get; set; }
        public int PriceListId { get; set; }
    }
}
