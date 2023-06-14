using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Models;
using Services.ViewModels;
using Services.Models;
using System.Security.Claims;
using Services.Repository;
using System.Text;
using File_Sharing_App.Helper.Mail;
using Models;

namespace Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _SignInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IJwtService _jwtService;
        private readonly IMailHelper _mailHelper;
        private readonly INewsService _service;
        private readonly ApplicationDbContext _context;
        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IJwtService jwtService, IMailHelper mailHelper, INewsService service, ApplicationDbContext context)
        {
            _SignInManager = signInManager;
            _userManager = userManager;
            this._jwtService = jwtService;
            this._mailHelper = mailHelper;
            this._service = service;
            this._context = context;
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ApiRespnse<CheckLoginApiModel.Response>>> Login(CheckLoginApiModel.Request model)
        {
            var response = new ApiRespnse<CheckLoginApiModel.Response>();
            var existedUser = await _userManager.FindByEmailAsync(model.Email);
            if (existedUser == null)
            {
                response.AddError(2);
                return (response);
            }
            var result = await _SignInManager.PasswordSignInAsync(existedUser, model.Password, model.Remmember, true);
            if (!result.Succeeded)
            {
                response.AddError(3);
                return (response);
            }
           var roles=await _userManager.GetRolesAsync(existedUser);
            response.Data = new CheckLoginApiModel.Response();
            response.Data.Token = _jwtService.GenerateAccessToken(new List<Claim>() {
                new Claim(ClaimTypes.Name,existedUser.Name),
                new Claim(ClaimTypes.Role,roles.FirstOrDefault()?.ToString() ?? "Not assign to role")
            }) ;
            var news = _service.GetALL();
            response.Data.News = news;
            if (await _userManager.IsInRoleAsync(existedUser, "Student"))
            { 
                response.Data.Type = 2;
                response.Data.Id = _context.Students.FirstOrDefault(d => d.UserId == existedUser.Id).Id;
            }
            if (await _userManager.IsInRoleAsync(existedUser, "Doctor"))
            {
                response.Data.Type = 1;
                response.Data.Id = _context.Doctors.FirstOrDefault(d => d.UserId == existedUser.Id).Id;
            }
            return Ok(response);
        }
        [HttpPost("LogOut")]
        public async Task<IActionResult> LogOut()
        {
            await _SignInManager.SignOutAsync();
            return Ok(new ApiResponse() { Message="You Logout successfully !!"});
        }

        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel.Request model,string Id)
        {
            var currentUser = await _userManager.FindByIdAsync(Id);
            if (ModelState.IsValid)
            {
                if (currentUser != null)
                {                  
                    var result = await _userManager.ChangePasswordAsync(currentUser, model.CurrentPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        await _SignInManager.SignOutAsync();
                        return Ok(new ChangePasswordViewModel.Response { Message= "Your Password is Changed successfully , you should log in again" });
                    }
                    else
                    {
                        return Ok(new ChangePasswordViewModel.Response { Message = "Your Password is weak, it should start with capital letter , contain greater than 8 letter , contain special letter" });
                    }
                }
                else
                {
                    return NotFound(new ChangePasswordViewModel.Response { Message = "The data of user is wrong" });
                }
            }
            return NotFound();
        }

        [HttpPost("ForgetPassword")]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel.Request model)
        {
            if (ModelState.IsValid)
            {
                var ExistUesr = await _userManager.FindByEmailAsync(model.Email);
                if (ExistUesr != null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(ExistUesr);
                    //$"{Request.Scheme}://{Request.Host}{Request.PathBase}/your/custom/path"
                    var url = $"{Request.Scheme}://{Request.Host}{Request.PathBase}/External/Account/ResetPassword?Token={token}&Email={model.Email}";
                    //var url = Url.Action("ResetPassword", "Account",new { area = "External", token, model.Email }, Request.Scheme);
                    StringBuilder body = new StringBuilder();
                    body.AppendLine("<div style='direction: ltr''>");
                    body.AppendLine("<h2> <b style='color:blue'>ACATANCE</b> : Reset Password </h2><br/>");
                    body.AppendLine("We are sending this email, because we have recieved a reset password request to your account <br/>");
                    body.AppendFormat("to reset new password <a href='{0}'>Click this link</a>", url);
                    body.AppendLine("</div>");
                    _mailHelper.SendMail(new InputMailMessage
                    {
                        Email = model.Email,
                        Subject = "Reset Password",
                        Body = body.ToString()
                    });
                    return Ok(new ChangePasswordViewModel.Response { Message = "Check mail Box the Email is sent" });
                }
            }
            return Ok(model);
        }
    }
}
