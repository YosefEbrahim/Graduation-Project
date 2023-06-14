using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;
using Services.Repository;
using Services.ViewModels;
using System.Net;

namespace Services.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer", Roles = "Doctor")]
    public class DoctorsController : ControllerBase
    {
        private readonly ILectureDetailsService _service;
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment env;
        public DoctorsController(ILectureDetailsService service, ApplicationDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            this._service = service;
            this.env = env;
        }

        [HttpPost("MakeLecture")]
        public async Task<IActionResult> Make_Lecture(LectureViewModel Lec)
        {
            var result = _service.Make_Lecture(Lec);
            return Ok(result);
        }
        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessage([FromForm] MessageViewModel msg)
        {
            ModelState.Remove("File");
            if (ModelState.IsValid)
            {
                string fileName = null;
           
                if (msg.File != null)
                {
                    var newName = Guid.NewGuid().ToString();
                    var extension = Path.GetExtension(msg.File.FileName);
                    fileName = string.Concat(newName, extension);
                    var root = env.WebRootPath;
                   if( ! Directory.Exists(root+ "/Uploads"))
                    {
                        Directory.CreateDirectory(root + "/Uploads");
                    }
                    var path = Path.Combine(root, "Uploads", fileName);
                    using (var fs = System.IO.File.Create(path))
                    {
                        await msg.File.CopyToAsync(fs);
                    }
                }
                var message = new Messages
                {
                    DoctorId = msg.DoctorId,
                    CreateDate = DateTime.UtcNow.AddHours(3),
                    DeptId = msg.DeptId,
                    Message = msg.Message,
                    PathFile = fileName == null ? null : "Uploads/" + fileName,
                    RankId = msg.RankId,
                    
                };
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
                return Ok("you send message succussfully");
            }
            return Content("you should check files");
        }
        //[HttpGet("GetMyLectures")]
        //public async Task<IActionResult> GetMyLectures(string SubjectId)
        //{
        //    var result =await _context.Lectures.Where(l=>l.SubjectId== SubjectId).ToListAsync();
        //    return Ok(result);
        //}

        [HttpGet("GetMySubjects")]
        public async Task<IActionResult> GetMySubjects(string DocterId)
        {
            var result = await _context.DoctorSubjects.Where(l => l.DoctorsId == DocterId).Include(x => x.Subject).Select(c => c.Subject).Select(s => new SubjectViewModel { Code = s.Code, DeptId = s.DeptId, Id = s.Id, Name = s.Name }).ToListAsync();
            return Ok(result);
        }

        [HttpGet("GetStatusOfLecture")]
        public async Task<IActionResult> GetStatusOfLecture(string LectureId)
        {
            var result = await _context.LectureStudents
                .Where(l => l.LectureId == LectureId)
                .Include(c=>c.Lecture).ThenInclude(t=>t.Subject)
                .Include(c => c.Student).ThenInclude(t=>t.User)
                .Select(s=> new LectureStudentsViewModel {
                    EndTime = s.Lecture.EndTime,
                    StartTime = s.Lecture.StartTime,
                    LecNumber= s.Lecture.LecNumber,
                    LectureId = s.Lecture.Id,
                    Major = s.Student.Major,
                    StudentId = s.Student.Id,
                    StudentName = s.Student.User.Name,
                    SubjectName = s.Lecture.Subject.Name,
                    UniversityId = s.Student.UniversityId
                })
                .ToListAsync();
            return Ok(result);

        }
        [HttpGet("GetLectures")]
        public async Task<IActionResult> GetLectures(string DocterId, string SubjectId)
        {
            var result = await _context.Lectures.Where(l => l.SubjectId == SubjectId && l.DoctorId == DocterId).ToListAsync();
            return Ok(result);

        }
        [HttpGet("GetCountStatusOfLecture")]
        public async Task<IActionResult> GetCountStatusOfLecture(string LectureId)
        {
            var result = await _context.LectureStudents.Where(l => l.LectureId == LectureId).CountAsync(x => x.IsAttend == true);
            return Ok(result);
        }
        [HttpGet("MyExams")]
        public async Task<IActionResult> MyExams(string DocterId)
        {
            var result = await _context.Exams
                .Where(l => l.DoctorId == DocterId)
                .Include(x => x.Subject)
                .Include(x=>x.Doctor)
                .ThenInclude(u=>u.User)
                .Include(x => x.Department)
                .Include(x => x.Rank)
                .Select(s => new DoctorExamsViewModel {
                    ExamId=s.Id,
                    DeptName=s.Department.DeptName,
                    DoctorName=s.Doctor.User.Name,
                    ExamName=s.ExamName,
                    RankName=s.Rank.Name,
                    StartTime=s.StartTime,
                    SubjectName=s.Subject.Name,
                    Status=s.Stutas,
                })
                .ToListAsync();
            return Ok(result);
        }
        [HttpGet("StatusOfExam")]
        public async Task<IActionResult> StatusOfExam(string ExamId)
        {
            var result = await _context.ExamStudents
                .Where(x=>x.ExamId == ExamId)
                .Include(x => x.Student)
                .ThenInclude(u => u.User)
                .Include(x => x.Exam)
                .Select(s => new ExamsStatusViewModel
                {
                    StudentName = s.Student.User.Name,
                    ExamName = s.Exam.ExamName,
                    Degree = s.Degree,
                })
                .OrderByDescending(x=>x.Degree)
                .ToListAsync();
            return Ok(result);
        }
        [HttpPost("AddExam")]
        public async Task<IActionResult> AddExam([FromForm] ExamViewModel model)
        {

            if (ModelState.IsValid)
            {
                var exam = new Exam
                {
                    StartTime = model.StartTime,
                    Degree = model.Degree,
                    Duration = model.Duration,
                    DoctorId = model.DoctorId,
                    ExamName = model.ExamName,
                    RankId = model.RankId,
                    SubjectId=model.Subject_Id,
                    DepartmentId=model.DeptId,

                };
                 await _context.Exams.AddAsync(exam);
                  await _context.SaveChangesAsync();
            return Ok(exam.Id);
            }
        return Content("you should check Inputs");
        }

        [HttpPost("AddQuestions")]
        public async Task<IActionResult> AddQuestions(List<QuestionViewModel> Questions)
        {

            if (ModelState.IsValid)
            {
                List<Question> questions = new List<Question>();

                foreach (var item in Questions)
                {
                    Question question = new Question();
                    question.answer1 = item.answer1;
                    question.answer2 = item.answer2;
                    question.answer3 = item.answer3;
                    question.answer4 = item.answer4;
                    question.CorrectAnswer = item.CorrectAnswer;
                    question.QuestionContent = item.QuestionContent;
                    question.ExamId = item.ExamId;
                    questions.Add(question);
                }
                await _context.Questions.AddRangeAsync(questions);
                await _context.SaveChangesAsync();
                return Ok("You Send questions Succussfully");
            }
            return Content("you should check Inputs");
        }
        [HttpPost("OpenExam")]
        public async Task<IActionResult> OpenExam(string ExamId)
        {
               var exam= await _context.Exams.FirstOrDefaultAsync(x=>x.Id==ExamId);
                exam.Stutas = !exam.Stutas;
                await _context.SaveChangesAsync();
                return Ok("You Send Change Status Succussfully = " + exam.Stutas);
        }
    }
}
