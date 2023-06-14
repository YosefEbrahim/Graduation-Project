using Admin.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;

namespace Admin.Areas.Admin.Controllers
{
    public class SubjectsController : GenericController
    {
        private readonly ApplicationDbContext _context;

        public SubjectsController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: SubjectsController
        public ActionResult Index()
        {
            var model = _context.Subjects.ToList();
            return View(model);
        }

        // GET: SubjectsController/Details/5
        public ActionResult Details(string id)
        {
            var model = _context.Subjects.Include(d=>d.Department).FirstOrDefault(x=>x.Id==id);
            return View(model);
        }

        // GET: SubjectsController/Create
        public ActionResult Create()
        {
          var departments=  _context.Departments.ToList();

            var model = new SubjectViewModel { Departments = new SelectList(departments, "Id", "DeptName") };
            return View(model);
        }

        // POST: SubjectsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PostSubjectViewModel collection)
        {
            var subject = new Subject { Code = collection.Code, Name = collection.Name, DeptId=collection.DeptId };
            _context.Subjects.Add(subject);
            _context.SaveChanges();
                return RedirectToAction("Index");

        }

        // GET: SubjectsController/Edit/5
        public ActionResult Edit(string id)
        {
            var model = _context.Subjects.FirstOrDefault(x => x.Id == id);
            var dept = _context.Departments.ToList();
            var selectedDept = dept.FirstOrDefault(x=>x.Id==model.Department.Id);
            var subjectmodel = new SubjectViewModel {Name=model.Name,Code=model.Code, Departments = new SelectList(dept, "Id", "DeptName",selectedDept.Id) };
            return View(subjectmodel);
        }

        // POST: SubjectsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string id, PostSubjectViewModel collection)
        {
            var subject = _context.Subjects.Find(id);
            subject.Name = collection.Name;
            subject.Code = collection.Code;
            subject.DeptId = collection.DeptId;
            _context.Subjects.Update(subject);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SubjectsController/Delete/5
        public ActionResult Delete(string id)
        {
            var subject = _context.Subjects.Find(id);
            return View(subject);
        }

        // POST: SubjectsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, SubjectViewModel collection)
        {
            var subject = _context.Subjects.Find(id);
            _context.Subjects.Remove(subject);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
