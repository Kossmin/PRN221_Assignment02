using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages.Products
{
    public class DetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }

        [BindProperty]
        public Product Product { get; set; }
        public void OnGet()
        {
            Management<Product> managementProduct = new Management<Product>();
            Management<Category> managementCategory = new Management<Category>();
            Management<Supplier> managementSupplier = new Management<Supplier>();

            Product = managementProduct.GetBy(prod => prod.ProductId == id);
            Product.Supplier = managementSupplier.GetBy(sup => sup.SupplierId == Product.SupplierId);
            Product.Category = managementCategory.GetBy(cate => cate.CategoryId == Product.CategoryId);
        }
    }
}
