using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class Doctor
    {
        public Doctor()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateTime = DateTime.Now;
            //this.Subjects = new HashSet<Subject>();
            //this.Ranks = new HashSet<Rank>();
        }
        public string Id { get; set; }
        public string Specialice { get; set; }
        public DateTime CreateTime { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [ForeignKey("Department")]
        public string DepartmentId { get; set; }
        public Department Department { get; set; }
        //public virtual ICollection<Subject> Subjects { get; set; }
        //public virtual ICollection<Rank> Ranks{ get; set; }
        [InverseProperty(nameof(Models.DoctorRanks.Doctor))]
        public List<DoctorRanks> DoctorRanks{ get; set; }
        [InverseProperty(nameof(Models.DoctorSubjects.Doctor))]
        public  List<DoctorSubjects> DoctorSubjects{ get; set; }
        public ICollection<Messages> Messages{ get; set; }

    }
}
