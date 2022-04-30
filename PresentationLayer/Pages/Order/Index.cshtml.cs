using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages.Order
{
    [Authorize]
    public class IndexModel : PageModel
    {
        [BindProperty]
        public List<DataLayer.Models.Order> Orders { get; set; }
        [BindProperty(SupportsGet =true)]
        public string id { get; set; }
        Management<DataLayer.Models.Order> managementOrder = new Management<DataLayer.Models.Order>();
        Management<DataLayer.Models.Customer> managementCustomer = new Management<DataLayer.Models.Customer>();
        public void OnGet()
        {
            if (User.IsInRole("Customer"))
            {
                var userID = User.Claims.FirstOrDefault(user => user.Type == ClaimTypes.NameIdentifier).Value;
                var cus = managementCustomer.GetBy(cus => cus.CustomerId == userID);
                Orders = managementOrder.GetMany(x=>x.CustomerId == userID).ToList();
                foreach (var item in Orders)
                {
                    item.Customer = cus;
                }
            }
            else
            {
                Orders = managementOrder.GetAll();
                foreach (var item in Orders)
                {
                    item.Customer = managementCustomer.GetBy(cus => cus.CustomerId == item.CustomerId);
                }
            }
        }
    }
}
