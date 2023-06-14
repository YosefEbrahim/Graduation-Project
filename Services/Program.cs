using File_Sharing_App.Helper.Mail;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Models;
using Models.Models;
using Services.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtOptions:Issuer"],
        ValidAudience = builder.Configuration["JwtOptions:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtOptions:Key"]))
    };
});

builder.Services.AddSwaggerGen(
  options =>
  {
      // Adding swagger document
      //options.SwaggerDoc("v1.0", new OpenApiInfo() { Title = "Main API v1.0", Version = "v1.0" });

      // Include the comments that we wrote in the documentation
      /*      options.IncludeXmlComments("ReadSwap.Api.xml");
            options.IncludeXmlComments("ReadSwap.Core.xml");
      */
      // To use unique names with the requests and responses
      options.CustomSchemaIds(x => x.FullName.Replace("+", "_"));
      //options.SchemaGeneratorOptions.SchemaIdSelector = x => x.FullName;
      // Defining the security schema
      var securitySchema = new OpenApiSecurityScheme()
      {
          Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
          Name = "Authorization",
          In = ParameterLocation.Header,
          Type = SecuritySchemeType.Http,
          Scheme = "bearer",
          Reference = new OpenApiReference
          {
              Type = ReferenceType.SecurityScheme,
              Id = "Bearer"
          }
      };
      // Adding the bearer token authentaction option to the ui
      options.AddSecurityDefinition("Bearer", securitySchema);

      // use the token provided with the endpoints call
      options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { securitySchema, new[] { "Bearer" } }
      });

  });


builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
   options.SignIn.RequireConfirmedEmail = false;
   options.SignIn.RequireConfirmedAccount = false;
   options.SignIn.RequireConfirmedPhoneNumber = false;
}).AddDefaultTokenProviders().AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddTransient<INewsService, NewsService>();
builder.Services.AddTransient<IStudentService, StudentService>();
builder.Services.AddTransient<IJwtService, JwtService>();
builder.Services.AddTransient<IMailHelper, MailHelper>();
builder.Services.AddTransient<ILectureDetailsService, LectureDetailsService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}
app.UseSwagger();
app.UseSwaggerUI();
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.UseCors(options=>
{
    options.AllowAnyOrigin();
    options.AllowAnyMethod();
    options.AllowAnyHeader();

});
app.MapControllers();
app.MapControllerRoute(
  name: "areas",
  pattern: "{area:exists=External}/{controller=Account}/{action=Login}/{id?}"
  );
app.Run();
