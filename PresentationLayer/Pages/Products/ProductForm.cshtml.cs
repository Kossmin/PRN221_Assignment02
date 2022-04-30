using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages.Products
{
    [Authorize(Roles = "Member")]
    public class ProductFormModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int? id { get; set; }
        [BindProperty]
        public Product Product { get; set; }
        [BindProperty]
        public List<Category> Categories { get; set; }
        [BindProperty]
        public List<Supplier> Suppliers { get; set; }
        [BindProperty]
        public IFormFile Image { get; set; }

        private readonly IWebHostEnvironment webHostEnvironment;

        public ProductFormModel(IWebHostEnvironment hostEnvironment)
        {
            webHostEnvironment = hostEnvironment;
        }

        public void OnGet()
        {
            if(id != null)
            {
                Management<Product> management = new Management<Product>();
                Product = management.GetBy(prod => prod.ProductId == id);
            }
            else
            {
                Product = null;
            }
            Management<Category> managementCategory = new Management<Category>();
            Management<Supplier> managementSupplier = new Management<Supplier>();

            Categories = managementCategory.GetAll();
            Suppliers = managementSupplier.GetAll();
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Management<Product> management = new Management<Product>();
                //byte[] bytes = System.IO.File.ReadAllBytes(Path.GetFileName(Image.FileName));
                //string text = System.Text.Encoding.UTF8.GetString(bytes);

                if (id != null)
                {
                    Product.ProductId = (int)id;
                    if(Image == null && Product.ProductImage != null)
                    {

                    }
                    else
                    {
                        Product.ProductImage = Image.FileName;
                        AddImage();
                    }
                    if (management.Update(Product) != null)
                    {
                        TempData["Message"] = "Update success";
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        TempData["Error"] = "Update failed!";
                        return Page();
                    }
                }
                else
                {
                    if (management.Insert(Product) != null)
                    {
                        TempData["Message"] = "Add success";
                        return RedirectToPage("/Index");
                    }
                    else
                    {
                        TempData["Error"] = "Add failed!";
                        return Page();
                    }
                }
            }
            return Page();

        }

        public void AddImage()
        {
            string uploadsFolder = Path.Combine(webHostEnvironment.WebRootPath, "images");  
                string uniqueFileName = Image.FileName;  
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);  
                using (var fileStream = new FileStream(filePath, FileMode.Create))  
                {  
                    Image.CopyTo(fileStream);  
                }  
        }
    }
}
