using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Question
    {
        public Question()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string QuestionContent { get; set; }
        public string answer1 { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public string answer4 { get; set; }
        public string CorrectAnswer { get; set; }
        public string ExamId { get; set; }
        [ForeignKey("ExamId")]
        public Exam Exam { get; set; }

    }
}
