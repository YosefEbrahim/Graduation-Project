using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Lecture
    {
        
        public Lecture()
    {
        this.Id = Guid.NewGuid().ToString();
            //this.Students = new HashSet<Student>();
        }
    public string Id { get; set; }
    public string LecNumber { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Qr_Duration{ get; set; }
        public byte[]? Qr_Image { get; set; }
        [ForeignKey("Subject")]
        public string SubjectId { get; set; }

        public Subject Subject{ get; set; }
        [ForeignKey("Doctor")]
        public string DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        //public virtual ICollection<Student> Students { get; set; }
        [InverseProperty(nameof(Models.LectureStudents.Lecture))]
        public List<LectureStudents> LectureStudents { get; set; }


    }
}
