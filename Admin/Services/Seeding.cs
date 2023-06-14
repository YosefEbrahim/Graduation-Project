using Microsoft.AspNetCore.Identity;
using Models;
using Models.Models;

namespace Admin.Services
{
    public class Seeding:ISeeding
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext context;

        public Seeding(RoleManager<IdentityRole> roleManager,UserManager<ApplicationUser> userManager,ApplicationDbContext context)
        {
            this._roleManager = roleManager;
            this._userManager = userManager;
            this.context = context;
        }
        public async Task IntializeAsync()
        {
            if (!await _roleManager.RoleExistsAsync("Admin"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name= "Admin" });
            }
            if (!await _roleManager.RoleExistsAsync("SuperAdmin"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "SuperAdmin" });
            }
            if (!await _roleManager.RoleExistsAsync("Doctor"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Doctor" });
            }
            if (!await _roleManager.RoleExistsAsync("Student"))
            {
                await _roleManager.CreateAsync(new IdentityRole { Name = "Student" });
            }
            var AdminUser = "yosef@gmail.com";
            if (await _userManager.FindByEmailAsync(AdminUser) == null)
            {
                var User = new ApplicationUser()
                {
                    Email = AdminUser,
                    UserName = AdminUser,
                    NormalizedEmail=AdminUser.ToUpper(),
                    NormalizedUserName=AdminUser.ToUpper(),
                    PhoneNumber = "01060409120",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    Name = "Yosef Ebrahim",
                    //AdminId= "Admin",   
                };
                await _userManager.CreateAsync(User, "Yosef@12345");
                await _userManager.AddToRoleAsync(User, "SuperAdmin");
                var admin = new Models.Models.Admin
                {
                    
                    UserId = User.Id,
                    Name=User.Name,
                    //Id="Admin"

                };
                context.Admins.Add(admin);
                context.SaveChanges();

            }
        }
    }
}
