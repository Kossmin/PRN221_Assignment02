using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages.Products
{
    [Authorize(Roles ="Member")]
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public int? id { get; set; }
        public IActionResult OnGet()
        {
            Management<Product> managementProduct = new Management<Product>();
            var product = managementProduct.GetBy(prod => prod.ProductId == id);
            managementProduct.Remove(product);
            TempData["Message"] = $"{product.ProductName} is deleted";
            return RedirectToPage("/Index");
        }
    }
}
