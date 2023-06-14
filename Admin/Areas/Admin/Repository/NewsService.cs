using Admin.Areas.Admin.ViewModels;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;

namespace Admin.Areas.Admin.Repository
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext _context;
        public NewsService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<News> CreateAsync(NewsViewModels model)
        {
            var input = new News
            {
                Body = model.Body,
                CreateTime = model.CreateTime,
                Image = model.Image,
                Title = model.Title,
                AdminId = model.AdminId
            
            };
           await _context.Newses.AddAsync(input);
           await _context.SaveChangesAsync();
            return input;
        }

        public async Task<News> DeleteAsync(string Id)
        {
            var selected =await _context.Newses.FindAsync(Id);
            if(selected != null)
            {
                _context.Remove(selected);
              await _context.SaveChangesAsync();
            }
            return selected;
        }

        public IEnumerable<News> GetALL()
        {
            return _context.Newses.Include(n=>n.Admin).ThenInclude(n=>n.User).ToList();
        }

        public async Task<News> GetbyIdAsync(string NewsId)
        {
#pragma warning disable CS8603 // Possible null reference return.
            return await _context.Newses.Include(n=>n.Admin).ThenInclude(n => n.User).FirstOrDefaultAsync(n=>n.Id==NewsId);
#pragma warning restore CS8603 // Possible null reference return.
        }

        public async Task<News> UpdateAsync(News model)
        {
           _context.Update(model);
           await _context.SaveChangesAsync();
            return model;
        }
    }
}
