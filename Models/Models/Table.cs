using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Table
    {
        public Table()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string TableFile { get; set; }
        public int Section_Num { get; set; }
        public string RankId { get; set; }
        [ForeignKey("RankId")]
        public Rank Rank{ get; set; }
        public string DeptId { get; set; }
        [ForeignKey("DeptId")]
        public Department Department { get; set; }
    }
}
