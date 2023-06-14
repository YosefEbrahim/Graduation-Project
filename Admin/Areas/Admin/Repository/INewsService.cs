using Admin.Areas.Admin.ViewModels;
using Models.Models;

namespace Admin.Areas.Admin.Repository
{
    public interface INewsService
    {
        IEnumerable<News> GetALL();
        Task<News> GetbyIdAsync(string NewsId);
        Task<News> CreateAsync(NewsViewModels model);
        Task<News> UpdateAsync(News model);
        Task<News> DeleteAsync(string Id);

    }
}
