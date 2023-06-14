using Admin.Areas.Admin.ViewModels;
using Admin.Areas.Responds;
using Models.Models;

namespace Admin.Areas.Admin.Repository
{
    public interface IDoctorService<T>
    {
        IEnumerable<Doctor> GetALL();
        Task<Doctor> GetbyIdAsync(string DoctorId);
        Task<GenericRespond<T>> CreateAsync(DoctorViewModel model,ApplicationUser user);
        Task<Doctor> UpdateAsync(string Id, DoctorEditViewModel model);
        Task<Doctor> DeleteAsync(string Id);
    }
}
