using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Rank
    {
        public Rank()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Students = new HashSet<Student>();
            //this.Doctors = new HashSet<Doctor>();
            this.Subjects = new HashSet<Subject>();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Student> Students { get; set; }
        //public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Subject> Subjects{ get; set; }
        public string DeptId { get; set; }
        [ForeignKey("DeptId")]
        public Department Department { get; set; }
        [InverseProperty(nameof(Models.DoctorRanks.Rank))]
        public List<DoctorRanks> DoctorRanks { get; set; }
        public ICollection<Messages> Messages { get; set; }

    }
}
