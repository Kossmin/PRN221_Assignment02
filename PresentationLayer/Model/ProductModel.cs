using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Model
{
    public class ProductModel
    {
        public Product Product { get; set; }
        public Category Category { get; set; }
        public Supplier Supplier { get; set; }
    }
}
