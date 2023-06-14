using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class ExamStudents
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        [InverseProperty(nameof(Models.Student.ExamStudents))]
        public Student Student { get; set; }
        public string ExamId { get; set; }
        [ForeignKey("ExamId")]
        [InverseProperty(nameof(Models.Exam.ExamStudents))]
        public Exam Exam { get; set; }
        public string Degree { get; set; }

    }
}
