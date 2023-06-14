using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class ExamAnswers
    {

        public string Id { get; set; }
        public string Answer { get; set; }
        public string ExamId { get; set; }
        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }
        public string QuestionId { get; set; }
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public Student Student { get; set; }

    }
}
