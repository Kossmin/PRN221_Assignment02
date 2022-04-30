using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace PresentationLayer.Pages.Customer
{
    [Authorize(Roles = "Member")]
    public class IndexModel : PageModel
    {
        Management<DataLayer.Models.Customer> managementCustomer = new Management<DataLayer.Models.Customer>();
        [BindProperty]
        public List<DataLayer.Models.Customer> Customers { get; set; }
        public void OnGet()
        {
            Customers = managementCustomer.GetAll();
        }
    }
}
