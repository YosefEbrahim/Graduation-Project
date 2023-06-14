using Admin.Areas.Admin.ViewModels;

namespace Services.Repository
{
    public interface IStudentService
    {
       Task<bool> GetImage(string Id,IFormFile Img);
    }
}
