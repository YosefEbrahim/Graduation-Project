using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;

namespace Admin.Areas.Admin.Repository
{
    public class Global : IGlobal
    {
        private readonly ApplicationDbContext _context;
        public Global(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<string> GetAdminIdAsync(string AdminId)
        {
            var admin =await _context.Admins.FirstOrDefaultAsync(n => n.UserId == AdminId);
            return admin.Id;
        }
    }
}
