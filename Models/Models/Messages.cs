using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Messages
    {
        public int Id { get; set; }
        public string? RecieverId { get; set; }
        public bool IsSeen { get; set; }
        public string DoctorId { get; set; }
        [ForeignKey("DoctorId")]
        public Doctor Doctor { get; set; }
        public string RankId { get; set; }
        [ForeignKey("RankId")]
        public Rank Rank { get; set; }
        public string DeptId { get; set; }
        [ForeignKey("DeptId")]
        public Department Department { get; set; }
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
        public string? PathFile { get; set; }
    }
}
