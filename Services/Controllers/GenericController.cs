using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using static Services.ViewModels.GenericViewModel;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenericController : ControllerBase
    {

        private readonly ApplicationDbContext _context;

        public GenericController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("Teams")]
        public async Task<IActionResult> Teams()
        {
            var teams = await _context.Teams.ToListAsync();
            return Ok(new { Teams = teams });

        }
        [HttpGet("TeamsId")]
        public async Task<IActionResult> GetTeam(string TeamId)
        {
            var result = await _context.Teams.FirstOrDefaultAsync(w => w.Id == TeamId);
            return Ok(result);
        }
        [HttpGet("Subjects")]
        public async Task<IActionResult> Subjects(string DeptId)
        {
            var subjects = await _context.Subjects.Where(s=>s.DeptId== DeptId).ToListAsync();
            List<SubjectsViewModel> subjectsList = new List<SubjectsViewModel>();
            foreach (var item in subjects)
            {
                SubjectsViewModel subject = new SubjectsViewModel();
                subject.Id = item.Id;
                subject.SubjectName = item.Name;

                subjectsList.Add(subject);
            }
            return Ok(new { Subjects = subjectsList });

        }
        [HttpGet("Departments")]
        public async Task<IActionResult> Departments()
        {
            var departments = await _context.Departments.ToListAsync();
            List<DepartmentsViewModel> departmentsList = new List<DepartmentsViewModel>();
            foreach (var item in departments)
            {
                DepartmentsViewModel department = new DepartmentsViewModel();
                department.Id = item.Id;
                department.DepartmentName = item.DeptName;
                department.Description = item.Description;

                departmentsList.Add(department);
            }

            return Ok(new { Departments = departmentsList });

        }
        [HttpGet("DepartmentsId")]
        public async Task<IActionResult> Get(string DeptId)
        {
            var result = await _context.Departments.FirstOrDefaultAsync(w => w.Id == DeptId);
            DepartmentsViewModel department = new DepartmentsViewModel();
            department.Id = result.Id;
            department.DepartmentName = result.DeptName;
            department.Description = result.Description;
            return Ok(department);
        }
        [HttpGet("Ranks")]
        public async Task<IActionResult> Ranks(string DeptId,string DocterId)
        {
            var ranks = await _context.Ranks.Where(s => s.DeptId == DeptId).ToListAsync();
            List<RanksViewModel> ranksList = new List<RanksViewModel>();
            foreach (var item in ranks)
            {
                RanksViewModel rank = new RanksViewModel();
                rank.Id = item.Id;
                rank.RankName = item.Name;

                ranksList.Add(rank);
            }
            return Ok(new { Ranks = ranksList });

        }
    }
}
