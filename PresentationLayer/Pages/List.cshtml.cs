using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages
{
    public class ListModel : PageModel
    {
        [BindProperty]
        public string searchString { get; set; }
        
        [BindProperty]
        public decimal amount { get; set; }

        [BindProperty]
        public List<Product> Products { get; set; }
        public void OnGet()
        {
            Management<Product> managementProduct = new Management<Product>();
            Products = managementProduct.GetAll();
        }

        public void OnPost()
        {
            Management<Product> managementProduct = new Management<Product>();
            if (searchString != null && amount != 0)
            {
                Products = managementProduct.GetMany(prod => prod.ProductName.Contains(searchString) && prod.UnitPrice <= amount).ToList();

            }
            else if(searchString != null)
            {
                Products = managementProduct.GetMany(prod => prod.ProductName.Contains(searchString)).ToList();
            }else if(amount != 0)
            {
                Products = managementProduct.GetMany(prod => prod.UnitPrice <= amount).ToList();
            }
            else
            {
                Products = managementProduct.GetAll();
            }
        }
    }
}
