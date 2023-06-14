using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Admin.Data
{
    public class AdminContext : DbContext
    {
        public AdminContext (DbContextOptions<AdminContext> options)
            : base(options)
        {
        }

        public DbSet<Models.Models.Table> Table { get; set; } = default!;
    }
}
