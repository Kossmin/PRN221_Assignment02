using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages.Customer
{
    [Authorize(Roles = "Member")]
    public class CreateModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string id { get; set; }
        [BindProperty]
        public DataLayer.Models.Customer Customer { get; set; }
        private Management<DataLayer.Models.Customer> managementCustomer = new Management<DataLayer.Models.Customer>();
        public void OnGet()
        {
            if (id != null)
            {
                Customer = managementCustomer.GetBy(cus => cus.CustomerId == id);
            }
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                Customer.CustomerId = id;
                managementCustomer.Update(Customer);
                TempData["Message"] = "Update success";
                return RedirectToPage("/Customer/Index");
            }
            else
            {
                Customer = managementCustomer.GetBy(cus => cus.CustomerId == id);
                return Page();
            }
        }
    }
}
