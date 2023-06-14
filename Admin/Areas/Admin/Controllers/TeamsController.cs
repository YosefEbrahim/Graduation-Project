using Admin.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;

namespace Admin.Areas.Admin.Controllers
{
    public class TeamsController : GenericController
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;


        public TeamsController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this.env = env;
        }
        // GET: SubjectsController
        public ActionResult Index()
        {
            var model = _context.Teams.ToList();
            return View(model);
        }

        // GET: SubjectsController/Details/5
        public ActionResult Details(string id)
        {
            var model = _context.Teams.FirstOrDefault(x=>x.Id==id);
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
        public async Task<IActionResult> Create(TeamsViewModel collection)
        {

            //    var newName = Guid.NewGuid().ToString();
            //    var extension = Path.GetExtension(collection.Logo.FileName);
            //    var fileName = string.Concat(newName, extension);
            //    var root = env.WebRootPath;
            //    var path = Path.Combine(root, "Teams", fileName);
            //    using (var fs = System.IO.File.Create(path))
            //    {
            //        await collection.Logo.CopyToAsync(fs);
            //    }
            var team = new Team { Name = collection.Name,Description=collection.Description}; //,Logo = string.Concat("Teams/", fileName)
            using (var dataStream = new MemoryStream())
            {
                await collection.Logo.CopyToAsync(dataStream);

                team.Logo = dataStream.ToArray();
            }
            _context.Teams.Add(team);
            _context.SaveChanges();
             return RedirectToAction("Index");

        }

        // GET: SubjectsController/Edit/5
        public ActionResult Edit(string id)
        {
            var model = _context.Teams.FirstOrDefault(x => x.Id == id);
            var team = new TeamsViewModel { Name=model.Name , Description = model.Description};
            return View(team);
        }

        // POST: SubjectsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, TeamsViewModel collection)
        {
            var newName = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(collection.Logo.FileName);
            var fileName = string.Concat(newName, extension);
            var root = env.WebRootPath;
            var path = Path.Combine(root, "Teams", fileName);
            using (var fs = System.IO.File.Create(path))
            {
                await collection.Logo.CopyToAsync(fs);
            }

            var team = _context.Teams.Find(id);
            team.Name = collection.Name;
            team.Description = collection.Description;
            using (var dataStream = new MemoryStream())
            {
                await collection.Logo.CopyToAsync(dataStream);

                team.Logo = dataStream.ToArray();
            }
            _context.Teams.Update(team);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: SubjectsController/Delete/5
        public ActionResult Delete(string id)
        {
            var team = _context.Teams.Find(id);
            return View(team);
        }

        // POST: SubjectsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string id, TeamsViewModel collection)
        {
            var team = _context.Teams.Find(id);
            _context.Teams.Remove(team);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
