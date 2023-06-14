using Admin.Areas.Admin.Repository;
using Admin.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.Models;
using System.Security.Claims;

namespace Admin.Areas.Admin.Controllers
{
    public class StudentsController : GenericController
    {
        private readonly IStudentService<Student> _service;
        private readonly IGlobal _global;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;
        public StudentsController(IStudentService<Student> service, IGlobal global, UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            this._service = service;
            this._userManager = userManager;
            this._global = global;
            this._context = context;
        }
        public IActionResult Index()
        {
            var model = _service.GetALL();
            return View(model);
        }
        public IActionResult Create()
        {
            var departments = _context.Departments.ToList();
            var ranks = _context.Ranks.ToList();
            var model = new StudentViewModel { Departments = new SelectList(departments, "Id", "DeptName"),Ranks= new SelectList(ranks, "Id", "Name") };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(StudentViewModel model)
        {

            model.AdminId = await _global.GetAdminIdAsync(this.User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _service.CreateAsync(model);
            if(result.Errors != null )
            {
                var departments = _context.Departments.ToList();
                var ranks = _context.Ranks.ToList();
                model.Departments = new SelectList(departments, "Id", "DeptName");
                model.Ranks = new SelectList(ranks, "Id", "Name");
                ViewBag.errors = result.Errors;
                return View(model);
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
                var model = new StudentViewModel
                {
                    Email = item.User.Email,
                    Name = item.User.Name,
                    PhoneNumber = item.User.PhoneNumber,
                    Class = item.Class,
                    Faculty=item.Faculty,
                    Major=item.Major,
                    UniversityId=item.UniversityId,
                    Departments= new SelectList(departments, "Id", "DeptName",departments.FirstOrDefault(d=>d.Id==item.DepartmentId).Id)
                };
                return View(model);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string Id, StudentViewModel model)
        {
            if (!String.IsNullOrEmpty(Id))
            {
                await _service.UpdateAsync(Id, model);
                return RedirectToAction("Index");
            }
            return View();

        }

    }
}
