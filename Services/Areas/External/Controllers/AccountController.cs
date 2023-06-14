using Admin.Areas.Admin;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.Areas.External.ViewModels;

namespace Services.Areas.External.Controllers
{
    [Area("External")]
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
        public async Task<IActionResult> ResetPassword(VerifyResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ExistUesr = await _userManager.FindByEmailAsync(model.Email);
                if (ExistUesr != null)
                {
                    model.Token = model.Token.Replace(" ", "+");
                    var isValid = await _userManager.VerifyUserTokenAsync(ExistUesr, TokenOptions.DefaultProvider, "ResetPassword", model.Token);
                    if (isValid)
                    {
                        return View();
                    }
                    else
                    {
                        TempData["Error"] = "Token is invalid";
                        return View("Error");
                    }
                }
                TempData["Error"] = "user is not found";
                return View("Error");

            }
            TempData["Error"] = "data is invalid";
            return View("Error");
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var ExistUesr = await _userManager.FindByEmailAsync(model.Email);
                if (ExistUesr != null)
                {
                    model.Token = model.Token.Replace(" ", "+");
                    var result = await _userManager.ResetPasswordAsync(ExistUesr, model.Token, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("ResetRespond");
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        public IActionResult ResetRespond()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Lecture");
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
                if (await _userManager.IsInRoleAsync(existedUser, "Doctor"))
                {
                    var result = await _SignInManager.PasswordSignInAsync(existedUser, model.Password, model.Remmember, true);
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                        {
                            return LocalRedirect(returnUrl);
                        }
                        return RedirectToAction("Index", "Lecture");

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
    }
}
