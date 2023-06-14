using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;

namespace Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _SignInManager = signInManager;
            _userManager = userManager;
        }
        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            //returnUrl ??= Url.Content("~/");
            var existedUser = await _userManager.FindByEmailAsync(model.Email);
            if (existedUser == null)
            {
                TempData["Error"] = "Invalid Email or Password";
                return View(model);
            }
            ModelState.Remove("returnUrl");
            if (ModelState.IsValid)
            {
                if (await _userManager.IsInRoleAsync(existedUser, "Admin") || await _userManager.IsInRoleAsync(existedUser, "SuperAdmin"))
                {
                    var result = await _SignInManager.PasswordSignInAsync(existedUser, model.Password, model.Remmember, true);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Home");

                    }
                    else if (result.IsNotAllowed)
                    {
                        TempData["Error"] = "Not Allowed";
                        return View();
                    }
                }

                else
                {
                    TempData["Error"] = "Not Allowed";
                    return View();
                }
            }

            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await _SignInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}
