using Models;
using Models.Models;

namespace Services.Repository
{
    public class StudentService : IStudentService
    {
        private readonly ApplicationDbContext _context;
        public StudentService(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> GetImage(string Id, IFormFile Img)
        {
            var std =await _context.Students.FindAsync(Id);
            if(std != null)
            {
                using (var dataStream = new MemoryStream())
                {
                    await Img.CopyToAsync(dataStream);
                    std.Image = dataStream.ToArray();
                    _context.Students.Update(std);
                   await _context.SaveChangesAsync();
                    return true;
                }
     
            }
            return false;
        }
    }
}
