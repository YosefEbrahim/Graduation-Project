using Admin.Areas.Admin.ViewModels;
using Admin.Areas.Responds;
using Models.Models;

namespace Admin.Areas.Admin.Repository
{
    public interface IStudentService<T>
    {
      
        IEnumerable<Student> GetALL();
        Task<Student> GetbyIdAsync(string Id);
        Task<GenericRespond<T>> CreateAsync(StudentViewModel Model);
        Task<Student> UpdateAsync(string Id, StudentViewModel Model);
        Task<Student> DeleteAsync(string Id);
    }
}
