using Admin.Areas.Admin.Repository;
using Admin.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;
using Microsoft.Extensions.DependencyInjection;
using Admin.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AdminContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdminContext") ?? throw new InvalidOperationException("Connection string 'AdminContext' not found.")));

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<IDoctorService<Doctor>, DoctorService>();
builder.Services.AddTransient<IStudentService<Student>, StudentService>();
builder.Services.AddTransient<ISeeding, Seeding>();
builder.Services.AddTransient< IGlobal, Global> ();
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.User.RequireUniqueEmail = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredUniqueChars = 0;
    options.Password.RequireDigit = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<ApplicationDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
using (var scope = app.Services.CreateScope())
{
    //scope.ServiceProvider.GetService<ApplicationDbContext>()
    // .Database.Migrate();
    var intial = scope.ServiceProvider.GetRequiredService<ISeeding>();
    await intial.IntializeAsync();

}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();

/*app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acc}/{action=Login}/{id?}"
 );
*/
app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists=Admin}/{controller=Account}/{action=Login}/{id?}"
  );

app.Run();