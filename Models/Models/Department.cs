using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Department
    {
        public Department()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Students = new HashSet<Student>();
            this.Doctors = new HashSet<Doctor>();
            this.Subjects = new HashSet<Subject>();
            this.Ranks = new HashSet<Rank>();
        }
        [Key]
        public string Id { get; set; }
        public string DeptName { get; set; }
        public string Description { get; set; }
        
        public virtual ICollection<Student> Students { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Subject> Subjects { get; set; }
        public virtual ICollection<Rank> Ranks { get; set; }
        public ICollection<Messages> Messages { get; set; }
    }
}
