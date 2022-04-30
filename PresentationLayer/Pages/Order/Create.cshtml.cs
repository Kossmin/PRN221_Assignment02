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
    public class CreateModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string id { get; set; }
        [BindProperty]
        public DataLayer.Models.Order Order { get; set; }
        
        private Management<DataLayer.Models.Order> managementOrder = new Management<DataLayer.Models.Order>();
        private Management<DataLayer.Models.Customer> managementCustomer = new Management<DataLayer.Models.Customer>();
        
        public void OnGet()
        {
            if(id != null)
            {
                Order = managementOrder.GetBy(order => order.OrderId == id);
            }
        }

        public IActionResult OnPost()
        {
            var tmpCustomer = managementCustomer.GetBy(cus => cus.CustomerId == Order.CustomerId);
            if(tmpCustomer == null)
            {
                ModelState.AddModelError("CustomerId", "There is no customer with this ID");
            }
            if(id == null)
            {
                if (ModelState.IsValid)
                {
                    var newGuid = Guid.NewGuid();
                    var base64 = Convert.ToBase64String(newGuid.ToByteArray());
                    Order.OrderId = base64;
                    managementOrder.Insert(Order);
                    TempData["Message"] = "Add success";
                    return RedirectToPage("/Order/Index");
                }
            }
            else
            {
                if (ModelState.IsValid)
                {
                    Order.OrderId = id;
                    managementOrder.Update(Order);
                    TempData["Message"] = "Update success";
                    return RedirectToPage("/Order/Index");
                }
            }
            return Page();
        }
    }
}
