using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages.OrderDetail
{
    public class IndexModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public int productid { get; set; }
        [BindProperty(SupportsGet = true)]
        public string orderid { get; set; }
        public DataLayer.Models.OrderDetail OrderDetail { get; set; }
        Management<DataLayer.Models.OrderDetail> managementOrderDetail = new Management<DataLayer.Models.OrderDetail>();
        public void OnGet()
        {
            OrderDetail = managementOrderDetail.GetBy(ordetail => ordetail.OrderId == orderid && ordetail.ProductId == productid);
        }
    }
}
