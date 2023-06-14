using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Exam
    {
        public Exam()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateTime = DateTime.UtcNow.AddHours(3);
            this.Stutas = false;
            //this.Students = new HashSet<Student>();
            this.Questions = new HashSet<Question>();


        }
        public string Id { get; set; }
        public string ExamName { get; set; }
        public string Degree { get; set; }
        public bool Stutas { get; set; }
        public int Duration { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime StartTime { get; set; }
        [ForeignKey("Rank")]
        public string RankId { get; set; }
        public Rank Rank { get; set; }
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        [ForeignKey("Department")]
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
        [ForeignKey("Subject")]
        public string SubjectId { get; set; }
        public Subject Subject { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        //public virtual ICollection<Student> Students { get; set; }
        [InverseProperty(nameof(Models.ExamStudents.Exam))]
        public List<ExamStudents> ExamStudents { get; set; }

    }
}
