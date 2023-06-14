using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Services.ViewModels;

namespace Services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatbotController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ChatbotController(ApplicationDbContext context)
        {
            _context = context;

        }

        [HttpGet("GetSection")]
        public async Task<IActionResult> GetSection(string StudentId)
        {
            var result =  _context.Students.FirstOrDefault(w => w.Id == StudentId || w.UniversityId == StudentId);
            if (result != null)
                return Ok(result.Section_Num);
            else
                return Ok(new {key=0,msg="There Exist Error"});
        }

        [HttpGet("GetTable")]
        public async Task<IActionResult> GetTable(int SectionNum)
        {
            var result = _context.Tables.FirstOrDefault(w => w.Section_Num == SectionNum);
            if (result != null)
                return Ok(result.TableFile);
            else
                return Ok(new { key = 0, msg = "There Exist Error" });
        }

        [HttpGet("GetStatusOfAttandance")]
        public async Task<IActionResult> GetStatusOfAttandance(string StudentId)
        {
            var result = await _context.LectureStudents
                .Where(l => l.StudentId == StudentId)
                .Include(c => c.Lecture).ThenInclude(t => t.Subject)
                .Include(c => c.Student).ThenInclude(t => t.User)
                .Select(s => new LectureStudentsChatbotViewModel
                {
                    EndTime = s.Lecture.EndTime,
                    StartTime = s.Lecture.StartTime,
                    LecNumber = s.Lecture.LecNumber,
                    SubjectName = s.Lecture.Subject.Name,
                })
                .ToListAsync();
            if (result == null || result.Count == 0)
                return Ok(new { key = 0, msg = "There Exist Error" });
            else
                return Ok(result);

        }

    }
}
