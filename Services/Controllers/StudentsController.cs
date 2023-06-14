using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;
using Services.Repository;
using Services.ViewModels;

namespace Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer",Roles = "Student")]
    public class StudentsController : ControllerBase
    {

        private readonly ApplicationDbContext _context;
        private readonly IStudentService _service;

        public StudentsController(IStudentService service, ApplicationDbContext context)
        {
            this._service = service;
            _context = context;
        }


        [HttpPost("GetImage")]
        public async Task<IActionResult> GetImage(string Id , IFormFile Img)
        {
           var result= await _service.GetImage(Id, Img);
            if(result==true)
            return Ok("the image is saved successfully ...");
            else
                return BadRequest("the image is not saved ...");

        }
        [HttpPost("RegisterAttendance")]
        public IActionResult RegisterAttendance(RegisterAttendanceViewModel model)
        {
            if(model.LecId != "" && model.StdId != "")
            {
                string[] ids = model.LecId.Split(" ");
                try
                {
                var qr = _context.Qrs_Code.FirstOrDefault(q => q.Id == ids[1]);
                    if (qr != null && qr.Status == true)
                    {
                        var lec_std = new LectureStudents { LectureId = ids[0], StudentId = model.StdId, IsAttend = true, Time = model.ScanTime };
                        _context.LectureStudents.Add(lec_std);
                        _context.SaveChanges();
                        return Ok("You Are Attend Lecture successfully");
                    }
                    else if(qr != null && qr.Status == false)
                    {
                        return Ok("Qr code is expired");
                    }             
                }
                catch
                {
                }
                var lec = _context.Lectures.FirstOrDefault(n => n.Id == model.LecId);
                if (model.ScanTime > lec.StartTime && model.ScanTime < lec.StartTime.AddMinutes(lec.Qr_Duration))
                {
                    var lec_std = new LectureStudents { LectureId = model.LecId, StudentId = model.StdId, IsAttend = true, Time = model.ScanTime };
                    _context.LectureStudents.Add(lec_std);
                    _context.SaveChanges();
                    return Ok("You Are Attend Lecture successfully");
                }
                return Content("Not Attend");
            }
            return NotFound();
        }

        [HttpGet("GetMessages")]
        public async Task<IActionResult> GetMessages(string stdId)
        {
            if(stdId != null)
            {
               var student= _context.Students.FirstOrDefault(x => x.Id == stdId);
               var msg = await _context.Messages
                    .Include(c=>c.Doctor)
                    .ThenInclude(c=>c.User)
                    .Where(l => l.RankId == student.RankId && l.DeptId==student.DepartmentId)
                    .Select(x=> new StudentViewModel { 
                        CreateDate=x.CreateDate,
                        File=x.PathFile,
                        Message=x.Message,
                       DoctorName=x.Doctor.User.Name}
                    ).OrderByDescending(x=>x.CreateDate).ToListAsync();

            return Ok(msg);
            }
            return Content("you should send valid Id");
        }

        [HttpGet("MyExams")]
        public async Task<IActionResult> MyExams(string StudentId)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == StudentId);
            var result = await _context.Exams
                .Where(x=>x.DepartmentId == student.DepartmentId && x.RankId==student.RankId)
                .Include(x => x.Subject)
                .Include(x => x.Doctor)
                .ThenInclude(u => u.User)
                .Include(x => x.Department)
                .Include(x => x.Rank)
                .Select(s => new StudentExamsViewModel
                {
                    ExamId=s.Id,
                    DoctorName = s.Doctor.User.Name,
                    ExamName = s.ExamName,
                    StartTime = s.StartTime,
                    SubjectName = s.Subject.Name,
                    Status=s.Stutas,
                    Degree=_context.ExamStudents.FirstOrDefault(x=>x.StudentId==StudentId && x.ExamId==s.Id).Degree,
                    MainDegree=s.Degree,
                
                })
                .ToListAsync();
            return Ok(result);
        }
        [HttpGet("MyAvialableExams")]
        public async Task<IActionResult> MyAvialableExams(string StudentId)
        {
            var student = _context.Students.FirstOrDefault(x => x.Id == StudentId);
            var result = await _context.Exams
                .Where(x => x.DepartmentId == student.DepartmentId && x.RankId == student.RankId && x.Stutas==true)
                .Include(x => x.Subject)
                .Include(x => x.Doctor)
                .ThenInclude(u => u.User)
                .Include(x => x.Department)
                .Include(x => x.Rank)
                .Select(s => new StudentExamsViewModel
                {
                    ExamId=s.Id,
                    DoctorName = s.Doctor.User.Name,
                    ExamName = s.ExamName,
                    StartTime = s.StartTime,
                    SubjectName = s.Subject.Name,
                    Status = s.Stutas,
                    MainDegree = s.Degree,
                    

                })
                .ToListAsync();
            return Ok(result);
        }

        [HttpGet("GetQeustions")]
        public async Task<IActionResult> GetQeustions(string ExamId)
        {
            var exam = _context.Exams.FirstOrDefault(x => x.Id == ExamId);
            if (DateTime.UtcNow.AddHours(3) > exam.StartTime.AddMinutes(exam.Duration))
            {
                exam.Stutas = false;
                await _context.SaveChangesAsync();
                return Content("The Exam is End");
            }
            var result = await _context.Questions
                .Include(x=>x.Exam)
                .Where(x => x.ExamId == ExamId && x.Exam.Stutas==true)
                .Select(s => new StudentQuestionViewModel
                {
                    QuestionId=s.Id,
                   answer1=s.answer1,
                   answer2=s.answer2,
                   answer3=s.answer3,
                   answer4=s.answer4,
                   Duration=s.Exam.Duration,
                   ExamName=s.Exam.ExamName,
                   QuestionContent=s.QuestionContent,
                })
                .ToListAsync();
            return Ok(result);
        }

        [HttpPost("Answer")]
        public async Task<IActionResult> Answer(string ExamId, string StudentId ,List<AnswerViewModel> model)
        {
            var exam = await _context.Exams.FirstOrDefaultAsync(x => x.Id == ExamId);
            if(DateTime.UtcNow.AddHours(3) > exam.StartTime.AddMinutes(exam.Duration))
            {
                return Content("The Exam is End");
            }
            var result = 0;
            foreach (var answer in model)
            {
                var mainanswer = await _context.Questions.FirstOrDefaultAsync(x=>x.Id==answer.QuestionId);
                if (answer.Answer == mainanswer.CorrectAnswer)
                    result += 1;
                else
                    continue;
            }
            var countquestion =await _context.Questions.Where(x => x.ExamId == ExamId).CountAsync();
            ExamStudents examStudents = new ExamStudents { StudentId = StudentId, ExamId = ExamId, Degree = result.ToString() }; 
            await _context.ExamStudents.AddAsync(examStudents);
            await _context.SaveChangesAsync();
            return Content("your answer " + result + " question from total " + countquestion);
        }
    }
}
