using Admin.Areas.Admin.ViewModels;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;

namespace Services.Repository
{
    public class NewsService : INewsService
    {
        private readonly ApplicationDbContext _context;
        public NewsService(ApplicationDbContext context)
        {
            _context = context;
        }
        public IEnumerable<NewsDetails> GetALL()
        {
            List<NewsDetails> newsDetails = new List<NewsDetails>();
            var items = _context.Newses.Include(n => n.Admin).ThenInclude(n => n.User).ToList();
            foreach (var item in items)
            {
                var newsitem = new NewsDetails
                {
                    AdminName = item.Admin.Name,
                    Body = item.Body,
                    CreateTime = item.CreateTime,
                    Id = item.Id,
                    Image = item.Image,
                    Title = item.Title
                };
                newsDetails.Add(newsitem);
            };
            return newsDetails;
        }

        public async Task<NewsDetails> GetbyId(string NewsId)
        {
            var item = await _context.Newses.Include(n => n.Admin).ThenInclude(n => n.User).FirstOrDefaultAsync(n => n.Id == NewsId);
            var newsitem = new NewsDetails
            {
                AdminName = item.Admin.Name,
                Body = item.Body,
                CreateTime = item.CreateTime,
                Id = item.Id,
                Image = item.Image,
                Title = item.Title
            };
            return newsitem;
        }
    }
}
