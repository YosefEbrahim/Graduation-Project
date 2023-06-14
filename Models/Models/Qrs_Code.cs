using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Qrs_Code
    {
        public Qrs_Code()
        {
            this.Id = Guid.NewGuid().ToString();
            //this.Students = new HashSet<Student>();
        }
        public string Id { get; set; }
        public int Qr_Duration { get; set; }
        public byte[]? Qr_Image { get; set; }
        public bool Status { get; set; }
        public string LectureId { get; set; }
        [ForeignKey("LectureId")]
        public Lecture Lecture { get; set; }
    }
}
