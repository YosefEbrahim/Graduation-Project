using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Admin.Data;
using Models.Models;
using Models;
using Admin.Areas.Admin.ViewModels;

namespace Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TablesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;

        public TablesController(ApplicationDbContext context, IWebHostEnvironment env)
        {
            this._context = context;
            this.env = env;
        }

        // GET: Admin/Tables
        public async Task<IActionResult> Index()
        {
            var adminContext = _context.Tables.Include(t => t.Department).Include(t => t.Rank);
            return View(await adminContext.ToListAsync());
        }

        // GET: Admin/Tables/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Tables == null)
            {
                return NotFound();
            }

            var table = await _context.Tables
                .Include(t => t.Department)
                .Include(t => t.Rank)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        // GET: Admin/Tables/Create
        public IActionResult Create()
        {
            ViewData["DeptId"] = new SelectList(_context.Set<Department>(), "Id", "DeptName");
            //ViewData["RankId"] = new SelectList(_context.Set<Rank>(), "Id", "Name");
            return View();
        }

        // POST: Admin/Tables/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TableViewModel table)
        {
           
            if (ModelState.IsValid)
            {
                string fileName = null;
                if (table.File != null)
                {
                    var newName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(table.File.FileName);
                    fileName = string.Concat(newName, extension);
                    var root = env.WebRootPath;
                    if (!Directory.Exists(root + "/Uploads"))
                    {
                        Directory.CreateDirectory(root + "/Uploads");
                    }
                    var path = Path.Combine(root, "Uploads", fileName);
                    using (var fs = System.IO.File.Create(path))
                    {
                        await table.File.CopyToAsync(fs);
                    }
                }
                Table tableToAdd = new Table { RankId = table.RankId, DeptId = table.DeptId, TableFile = "Uploads/" + fileName };
                _context.Add(tableToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_context.Set<Department>(), "Id", "DeptName", table.DeptId);
            ViewData["RankId"] = new SelectList(_context.Set<Rank>(), "Id", "Name", table.RankId);
            return View(table);
        }

        // GET: Admin/Tables/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Tables == null)
            {
                return NotFound();
            }

            var table = await _context.Tables.FindAsync(id);
            if (table == null)
            {
                return NotFound();
            }
            ViewData["DeptId"] = new SelectList(_context.Set<Department>(), "Id", "Id", table.DeptId);
            ViewData["RankId"] = new SelectList(_context.Set<Rank>(), "Id", "Id", table.RankId);
            return View(table);
        }

        // POST: Admin/Tables/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("Id,TableFile,RankId,DeptId")] Table table)
        {
            if (id != table.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(table);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TableExists(table.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeptId"] = new SelectList(_context.Set<Department>(), "Id", "Id", table.DeptId);
            ViewData["RankId"] = new SelectList(_context.Set<Rank>(), "Id", "Id", table.RankId);
            return View(table);
        }

        // GET: Admin/Tables/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Tables == null)
            {
                return NotFound();
            }

            var table = await _context.Tables
                .Include(t => t.Department)
                .Include(t => t.Rank)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (table == null)
            {
                return NotFound();
            }

            return View(table);
        }

        // POST: Admin/Tables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Tables == null)
            {
                return Problem("Entity set 'AdminContext.Table'  is null.");
            }
            var table = await _context.Tables.FindAsync(id);
            if (table != null)
            {
                _context.Tables.Remove(table);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TableExists(string id)
        {
          return _context.Tables.Any(e => e.Id == id);
        }
    }
}
