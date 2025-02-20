﻿using ExpertCenter.Domain;
using ExpertCenter.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCenter.Repository.Models
{ 
    public class PriceList : ObjectBase
    {
        public string? Name { get; set; }
        public List<Product> Products { get; set; } = [];
        public List<UserColumn> UserColumns { get; set; } = [];
    }
}
