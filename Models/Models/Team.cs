using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Team
    {
        public Team()
        {
            this.Id = Guid.NewGuid().ToString();
        }
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public byte[] Logo { get; set; }
        public string Description { get; set; }
    }
}
