using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Subject
    {
        public Subject()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Students = new HashSet<Student>();
            //this.Doctors = new HashSet<Doctor>();
            this.Lectures = new HashSet<Lecture>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Student> Students{ get; set; }
       
        public string DeptId { get; set; }
        [ForeignKey("DeptId")]
        public Department Department{ get; set; }
        public virtual ICollection<Lecture> Lectures{ get; set; }
        [InverseProperty(nameof(Models.DoctorSubjects.Subject))]
        public List<DoctorSubjects> DoctorSubjects { get; set; }

    }
}
