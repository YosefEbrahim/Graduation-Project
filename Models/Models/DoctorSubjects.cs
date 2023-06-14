using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class DoctorSubjects
    {
        public int Id { get; set; }

        public string DoctorsId { get; set; }
        [ForeignKey("DoctorsId")]
        [InverseProperty(nameof(Models.Doctor.DoctorSubjects))]
        public Doctor Doctor { get; set; }
        public string SubjectsId { get; set; }
        [ForeignKey("SubjectsId")]
        [InverseProperty(nameof(Models.Subject.DoctorSubjects))]
        public Subject Subject { get; set; }
    }
}
