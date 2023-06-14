using Models.Models;

namespace Admin.Areas.Admin.Repository
{
    public interface IGlobal
    {
        Task<string> GetAdminIdAsync(string AdminId);
    }
}
