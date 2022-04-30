using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages.Order
{
    [Authorize(Roles = "Member")]
    public class DeleteModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string id { get; set; }
        public IActionResult OnGet()
        {
            Management<DataLayer.Models.Order> management = new Management<DataLayer.Models.Order>();
            management.Remove(management.GetBy(order => order.OrderId == id));
            TempData["Message"] = "Delete success";
            return RedirectToPage("/Order/Index");
        }
    }
}
