using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages.Order
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        [BindProperty(SupportsGet =true)]
        public string id { get; set; }
        [BindProperty]
        public List<DataLayer.Models.OrderDetail> OrderDetails { get; set; }
        Management<DataLayer.Models.OrderDetail> managementOrderDetail = new Management<DataLayer.Models.OrderDetail>();
        public void OnGet()
        {
            OrderDetails = managementOrderDetail.GetMany(x => x.OrderId == id).ToList();
        }
    }
}
