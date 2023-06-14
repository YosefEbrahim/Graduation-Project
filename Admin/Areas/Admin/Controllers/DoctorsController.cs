using Admin.Areas.Admin.Repository;
using Admin.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Models;
using System.Net.Mail;
using System.Security.Claims;

namespace Admin.Areas.Admin.Controllers
{
    public class DoctorsController : GenericController
    {
        private readonly IDoctorService<Doctor> _service;
        private readonly IGlobal _global;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public DoctorsController(IDoctorService<Doctor> service, IGlobal global, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this._service = service;
            this._userManager = userManager;
            this._global = global;
            this._context = context;
        }
        public  IActionResult Index()
        {
            var model= _service.GetALL();
            return View(model);
        }
        public IActionResult Create()
        {
            var departments = _context.Departments.ToList();
            var subjects = _context.Subjects.ToList();

            var model = new DoctorViewModel { Departments = new SelectList(departments, "Id", "DeptName"),Subjects =new SelectList ( subjects,"Id", "Name" ) };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(DoctorViewModel model)
        {

            var user = new ApplicationUser
            {
                PhoneNumber = model.PhoneNumber,
                Email = model.Email,
                UserName = new MailAddress(model.Email).User,
                Name = model.Name,
                AdminId= await _global.GetAdminIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier))
        };
           var result = await _service.CreateAsync(model,user);
            if (result.Errors != null)
            {
                ViewBag.errors = result.Errors;
                return View(model);
            }
            foreach (var item in model.SubjectsId)
            {
                DoctorSubjects doctor_Subject = new DoctorSubjects() { DoctorsId = result.GenericRes.Id, SubjectsId = item };

                _context.DoctorSubjects.Add(doctor_Subject);
                _context.SaveChanges();

            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteAsync(string Id)
        {
            if (!String.IsNullOrEmpty(Id))
            {
                var item = await _service.GetbyIdAsync(Id);
                return View(item);
            }
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteItem(string Id)
        {
            if (!String.IsNullOrEmpty(Id))
            {
                await _service.DeleteAsync(Id);
                return RedirectToAction("Index");
            }

            return View();
        }
        public async Task<IActionResult> Details(string Id)
        {
            if (!String.IsNullOrEmpty(Id))
            {
                var item = await _service.GetbyIdAsync(Id);
                return View(item);
            }
            return View();
        }
        public async Task<IActionResult> Edit(string Id)
        {
            if (!String.IsNullOrEmpty(Id))
            {
                var item = await _service.GetbyIdAsync(Id);
                var departments = _context.Departments.ToList();
                var model = new DoctorEditViewModel
                {
                    Email = item.User.Email,
                    Name = item.User.Name,
                    PhoneNumber = item.User.PhoneNumber,
                    Specialice=item.Specialice,
                    Departments = new SelectList(departments, "Id", "DeptName", departments.FirstOrDefault(d => d.Id == item.DepartmentId).Id)
                };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string Id, DoctorEditViewModel model)
        {
            if (!String.IsNullOrEmpty(Id))
            { 
                await _service.UpdateAsync(Id,model);
                return RedirectToAction("Index");
            }
            return View();

        }









    }
}
