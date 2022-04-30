using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using PresentationLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PresentationLayer.Pages
{
    [Authorize(Roles = "Member")]
    [Route("/IndexManage")]
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        [BindProperty]
        public List<Product> Products { get; set; }

        [BindProperty]
        public string searchString { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            Management<Product> managementProduct = new Management<Product>();
            Management<Category> managementCateGory = new Management<Category>();
            Management<Supplier> managementSupplier = new Management<Supplier>();
            Products = managementProduct.GetAll().ToList();
            foreach (var item in Products)
            {
                if(item.SupplierId == null)
                {
                    item.Supplier = new Supplier();
                }
                else
                {
                    item.Supplier = managementSupplier.GetBy(sup => sup.SupplierId == item.SupplierId);
                }
                if (item.CategoryId == null)
                {
                    item.Category = new Category();
                }
                else
                {
                    item.Category = managementCateGory.GetBy(cate => cate.CategoryId == item.CategoryId);
                }
            }
        }

        public void OnPost()
        {
            if (string.IsNullOrWhiteSpace(searchString))
            {
                OnGet();
            }
            else
            {

                Management<Product> managementProduct = new Management<Product>();
                Management<Category> managementCateGory = new Management<Category>();
                Management<Supplier> managementSupplier = new Management<Supplier>();
                Products = managementProduct.GetMany(prod => prod.ProductName.Contains(searchString)).ToList();
                foreach (var item in Products)
                {
                    if (item.SupplierId == null)
                    {
                        item.Supplier = new Supplier();
                    }
                    else
                    {
                        item.Supplier = managementSupplier.GetBy(sup => sup.SupplierId == item.SupplierId);
                    }
                    if (item.CategoryId == null)
                    {
                        item.Category = new Category();
                    }
                    else
                    {
                        item.Category = managementCateGory.GetBy(cate => cate.CategoryId == item.CategoryId);
                    }
                }
            }
        }
    }
}
