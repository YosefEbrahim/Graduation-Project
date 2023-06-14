using Admin.Areas.Admin.ViewModels;
using Models.Models;

namespace Services.Repository
{
    public interface INewsService
    {
        IEnumerable<NewsDetails> GetALL();
        Task<NewsDetails> GetbyId(string NewsId);
    }
}
