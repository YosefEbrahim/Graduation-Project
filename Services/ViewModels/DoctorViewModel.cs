using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace Services.ViewModels
{
    public class LectureViewModel
    {
        [Required]
        public string LecNumber { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public int Qr_Duration { get; set; }
        [Required]
        public string Subject_Id { get; set; }
        [Required]
        public string Doctor_Id { get; set; }

    }
    public class LectureWebViewModel
    {
        [Required]
        [Display(Name = "Lecture Number")]
        [Range(1,12,ErrorMessage ="you must enter num between 1 to 12")]
        public string LecNumber { get; set; }
        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; } = DateTime.Now;
        [Required]
        [Display(Name = "Qr Duration Randamizetion")]
        [Range(1, 5, ErrorMessage = "you must enter num between 1 to 5")]
        public int Qr_Duration { get; set; } = 1;
        [Required]
        [Display(Name = "Subject Name")]
        public string Subject_Id { get; set; }
        [Required]
        public string Doctor_Id { get; set; }
        public SelectList Subjects { get; set; }

    }
    public class SubjectViewModel
    {
        public string Code { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string DeptId { get; set; }
    }
    public class MessageViewModel
    {
        [Required]
        public string Message { get; set; }
        public IFormFile? File { get; set; }
        [Required]
        public string DeptId { get; set; }
        [Required]
        public string RankId { get; set; }
        [Required]
        public string DoctorId { get; set; }
    }
    public class DoctorExamsViewModel
    {

        public string ExamId { get; set; }
        public string DeptName { get; set; }
        public string RankName { get; set; }
        public string SubjectName { get; set; }
        public DateTime StartTime { get; set; }
        public string ExamName { get; set; }
        public string DoctorName { get; set; }
        public bool Status { get; set; }

    }
    public class ExamsStatusViewModel
    {

        public string Degree { get; set; }
        public string StudentName { get; set; }
        public string ExamName { get; set; }
    }
    public class ExamViewModel
    {

        [Required]
        public string DeptId { get; set; }
        [Required]
        public string RankId { get; set; }
        [Required]
        public string DoctorId { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public string Subject_Id { get; set; }
        [Required]
        public int Duration { get; set; }
        public string Degree { get; set; }
        public string ExamName { get; set; }
    }
    
    public class QuestionViewModel
    {
        [Required]
        public string QuestionContent { get; set; }
        [Required]
        public string answer1 { get; set; }
        [Required]
        public string answer2 { get; set; }
        [Required]
        public string answer3 { get; set; }
        [Required]
        public string answer4 { get; set; }
        [Required]
        public string CorrectAnswer { get; set; }
        public string ExamId { get; set; }
    }
}
