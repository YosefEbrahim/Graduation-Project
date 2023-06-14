using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class DoctorRanks
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        [InverseProperty(nameof(Models.Doctor.DoctorRanks))]
        public Doctor Doctor { get; set; }
        public string RankId { get; set; }
        [ForeignKey("RankId")]
        [InverseProperty(nameof(Models.Rank.DoctorRanks))]
        public Rank Rank { get; set; }
    }
}
