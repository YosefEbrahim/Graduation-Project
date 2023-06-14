using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class LectureStudents
    {
        public int Id { get; set; }

        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        [InverseProperty(nameof(Models.Student.LectureStudents))]
        public Student Student { get; set; }
        public string LectureId { get; set; }
        [ForeignKey("LectureId")]
        [InverseProperty(nameof(Models.Lecture.LectureStudents))]
        public Lecture Lecture { get; set; }
        public bool IsAttend { get; set; }
        public DateTime Time { get; set; }
    }
}
