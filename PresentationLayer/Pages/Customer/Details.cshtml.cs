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
    public class DetailsModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string id { get; set; }
        [BindProperty]
        public DataLayer.Models.Customer Customer { get; set; }

        Management<DataLayer.Models.Customer> management = new Management<DataLayer.Models.Customer>();
        public void OnGet()
        {
            Customer = management.GetBy(cus => cus.CustomerId == id);
        }
    }
}
