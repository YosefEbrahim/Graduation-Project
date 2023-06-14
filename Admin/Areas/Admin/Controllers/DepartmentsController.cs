using Admin.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;

namespace Admin.Areas.Admin.Controllers
{
    public class DepartmentsController : GenericController
    {
        private readonly ApplicationDbContext _context;

        public DepartmentsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: SubjectsController
        public ActionResult Index()
        {
            var model = _context.Departments.ToList();
            return View(model);
        }

        // GET: SubjectsController/Details/5
        public ActionResult Details(string id)
        {
            var model = _context.Departments.Include(d => d.Doctors).ThenInclude(d=>d.User).Include(d => d.Subjects).FirstOrDefault(x=>x.Id==id);
            return View(model);
        }

        // GET: SubjectsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SubjectsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DepartmentViewModel collection)
        {
            var department = new Department { DeptName = collection.Name,Description=collection.Description };
            _context.Departments.Add(department);
            _context.SaveChanges();
             return RedirectToAction("Index");

        }

        // GET: SubjectsController/Edit/5
        public ActionResult Edit(string id)
        {
            var model = _context.Departments.FirstOrDefault(x => x.Id == id);
            var departmentModel = new DepartmentViewModel { Name=model.DeptName , Description = model.Description };
            return View(departmentModel);
        }

        // POST: SubjectsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, DepartmentViewModel collection)
        {
            
            var dept = _context.Departments.Find(id);
            dept.DeptName = collection.Name;
            dept.Description = collection.Description;
            _context.Departments.Update(dept);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SubjectsController/Delete/5
        public ActionResult Delete(string id)
        {
            var subject = _context.Departments.Find(id);
            return View(subject);
        }

        // POST: SubjectsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, DepartmentViewModel collection)
        {
            var department = _context.Departments.Find(id);
            _context.Departments.Remove(department);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
