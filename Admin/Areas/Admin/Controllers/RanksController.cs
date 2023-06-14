using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models.Models;
using Models;
using Admin.Areas.Admin.ViewModels;

namespace Admin.Areas.Admin.Controllers
{
    public class RanksController : GenericController
    {
        private readonly ApplicationDbContext _context;

        public RanksController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Ranks
        public async Task<IActionResult> Index()
        {
              return View(await _context.Ranks.ToListAsync());
        }

        // GET: Admin/Ranks/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Ranks == null)
            {
                return NotFound();
            }

            var rank = await _context.Ranks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rank == null)
            {
                return NotFound();
            }

            return View(rank);
        }

        // GET: Admin/Ranks/Create
        public IActionResult Create()
        {
            var departments = _context.Departments.ToList();
            var model = new PostRanksViewModel { Departments = new SelectList(departments, "Id", "DeptName")};
            return View(model);
        }

        // POST: Admin/Ranks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PostRanksViewModel rank)
        {
            ModelState.Remove("Departments");
            if (ModelState.IsValid)
            {
                var rankToAdd = new Rank { Name = rank.Name,DeptId=rank.DeptId };
                _context.Ranks.Add(rankToAdd);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var departments = _context.Departments.ToList();
            rank.Departments = new SelectList(departments, "Id", "DeptName");
            return View(rank);
        }

        // GET: Admin/Ranks/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Ranks == null)
            {
                return NotFound();
            }

            var rank = await _context.Ranks.FindAsync(id);
            if (rank == null)
            {
                return NotFound();
            }
            var model=new RanksViewModel { Name=rank.Name,Id=rank.Id};
            return View(model);
        }

        // POST: Admin/Ranks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, RanksViewModel rank)
        {
            if (id != rank.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var rankToUpdate =_context.Ranks.FirstOrDefault(n=>n.Id==rank.Id);
                    rankToUpdate.Name = rank.Name;
                    _context.Ranks.Update(rankToUpdate);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RankExists(rank.Id))
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
            return View(rank);
        }

        // GET: Admin/Ranks/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Ranks == null)
            {
                return NotFound();
            }

            var rank = await _context.Ranks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rank == null)
            {
                return NotFound();
            }

            return View(rank);
        }

        // POST: Admin/Ranks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Ranks == null)
            {
                return Problem("Entity set 'AdminContext.Rank'  is null.");
            }
            var rank = await _context.Ranks.FindAsync(id);
            if (rank != null)
            {
                _context.Ranks.Remove(rank);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RankExists(string id)
        {
          return _context.Ranks.Any(e => e.Id == id);
        }
    }
}
