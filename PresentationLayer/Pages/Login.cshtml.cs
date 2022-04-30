using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using DataLayer;
using DataLayer.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PresentationLayer.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        [Required]
        public string Username { get; set; }
        [BindProperty]
        [Required]
        public string Password { get; set; }

        private Management<Account> managementStaff = new Management<Account>();
        private Management<DataLayer.Models.Customer> managementCustomer = new Management<DataLayer.Models.Customer>();

        public void OnGet(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var accountStaff = managementStaff.GetBy(x => x.UserName == Username && x.Password == Password);
                var accountMember = managementCustomer.GetBy(x => x.CustomerId == Username && x.Password == Password);
                if(accountMember!= null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("username", Username));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, Username));

                    claims.Add(new Claim(ClaimTypes.Role, "Customer"));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimPrincipal);
                    return RedirectToPage("/List");
                }else if(accountStaff != null)
                {
                    var claims = new List<Claim>();
                    claims.Add(new Claim("username", Username));
                    claims.Add(new Claim(ClaimTypes.NameIdentifier, Username));

                    claims.Add(new Claim(ClaimTypes.Role, "Member"));

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
                    await HttpContext.SignInAsync(claimPrincipal);
                    return RedirectToPage("/List");
                }
                else
                {
                    TempData["Error"] = "Wrong password/username";
                    return Page();
                }
            }
            TempData["Error"] = "Invalid input";
            return Page();
        }
    }
}
