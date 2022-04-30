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
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string id { get; set; }
        public IActionResult OnGet()
        {
            Management<DataLayer.Models.Customer> management = new Management<DataLayer.Models.Customer>();
            management.Remove(management.GetBy(cus => cus.CustomerId == id));
            TempData["Message"] = "Delete success";
            return RedirectToPage("/Customer/Index");
        }
    }
}
