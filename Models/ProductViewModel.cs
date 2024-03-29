﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEB2022_ZZFashion.Models
{
    public class ProductViewModel
    {
        public int ProductID { get; set; }
        public string ProductTitle { get; set; }
        public string? ProductImage { get; set; }
        public string ProductDesc { get; set; }
        public string ProductCat { get; set; }
        public string Obsolete { get; set; }
        public List<Product> ProductList { get; set; }
    }
}
