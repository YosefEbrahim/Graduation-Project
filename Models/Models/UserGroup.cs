﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class UserGroup
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        public string GroupsId { get; set; }

        [ForeignKey("GroupsId")]
        public virtual Rank Rank{ get; set; }
    }
}
