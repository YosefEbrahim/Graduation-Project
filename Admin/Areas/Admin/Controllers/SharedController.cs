using Admin.Areas.Admin.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Data;

namespace Admin.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class SharedController : Controller
    {
        private readonly ApplicationDbContext _context;
        public SharedController(ApplicationDbContext context)
        {
            this._context = context;
        }
        [HttpPost]
        public IActionResult GetRanks(string deptid)
        {
            if (deptid != null)
            {
                var ranks = _context.Ranks.Where(x=>x.DeptId== deptid).ToList();
                List<RanksViewModel> rankslist = new List<RanksViewModel>();
                foreach (var item in ranks)
                {
                    RanksViewModel model = new RanksViewModel();
                    model.Id = item.Id;
                    model.Name = item.Name;
                    rankslist.Add(model);
                }
                return Json(new {key=1, ranks = rankslist });
            }
            return NotFound();
        }
    }
}
