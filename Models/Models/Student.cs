using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class Student
    {
        public Student()
        {
            this.Id = Guid.NewGuid().ToString();
            //this.Lectures = new HashSet<Lecture>();
            //this.Exams = new HashSet<Exam>();

        }
        public string Id { get; set; }
        public string UniversityId { get; set; }
        public string Major { get; set; }
        public int Section_Num { get; set; }
        public string Faculty { get; set; }
        public byte[]? Image { get; set; }
        public string Class { get; set; }
        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("Department")]
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
       // public virtual ICollection<Exam> Exams { get; set; }
        //public virtual ICollection<Lecture> Lectures { get; set; }
        public string RankId { get; set; }
        [ForeignKey("RankId")]
        public Rank Rank { get; set; }
        [InverseProperty(nameof(Models.ExamStudents.Student))]
        public List<ExamStudents> ExamStudents { get; set; }
        [InverseProperty(nameof(Models.LectureStudents.Student))]
        public List<LectureStudents> LectureStudents { get; set; }

    }
}
