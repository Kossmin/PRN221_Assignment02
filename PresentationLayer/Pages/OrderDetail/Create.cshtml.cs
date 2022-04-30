using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages.OrderDetail
{
    [Authorize(Roles = "Member")]
    public class CreateModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string orderid { get; set; }
        [BindProperty]
        public DataLayer.Models.OrderDetail OrderDetail { get; set; }
        Management<DataLayer.Models.OrderDetail> managementOrderDetail = new Management<DataLayer.Models.OrderDetail>();
        Management<DataLayer.Models.Product> managementProduct = new Management<DataLayer.Models.Product>();
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if(managementOrderDetail.GetBy(orderDetail => orderDetail.OrderId == OrderDetail.OrderId && orderDetail.ProductId == OrderDetail.ProductId) != null){
                ModelState.AddModelError("ProductId", "This product is already in the order detail");
            }else if(managementProduct.GetBy(prod=> prod.ProductId == OrderDetail.ProductId) == null)
            {
                ModelState.AddModelError("ProductId", "This product is not available");
            }
            if (ModelState.IsValid)
            {
                OrderDetail.OrderId = orderid;
                managementOrderDetail.Insert(OrderDetail);
                TempData["Message"] = "Add success";
                return RedirectToPage("/Order/Details");
            }
            else
            {
                return Page();
            }
        }
    }
}
