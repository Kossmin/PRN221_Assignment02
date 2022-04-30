using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PresentationLayer.Model;

namespace PresentationLayer.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public RegisterAccount AccountInfo { get; set; }
        [BindProperty]
        [Required]
        public string ConfirmedPassword { get; set; }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            Management<DataLayer.Models.Customer> managementAccount = new Management<DataLayer.Models.Customer>();
            if(ConfirmedPassword != AccountInfo.Password)
            {
                ModelState.AddModelError("ConfirmedPassword", "The password and the confirmed password is not correct");
                return Page();
            }
            if(managementAccount.GetBy(acc => acc.CustomerId == AccountInfo.Username) != null){
                ModelState.AddModelError("AccountInfo.UserName", "This username is used");
                return Page();
            }
            if (ModelState.IsValid)
            {
                managementAccount.Insert(new DataLayer.Models.Customer { 
                    CustomerId = AccountInfo.Username,
                    Password = AccountInfo.Password,
                    ContactName = AccountInfo.Fullname,
                });
                TempData["Message"] = "Register success";
                return RedirectToPage("/Login");
            }
            else
            {
                TempData["Error"] = "Try again";
                return Page();
            }
        }
    }
}
