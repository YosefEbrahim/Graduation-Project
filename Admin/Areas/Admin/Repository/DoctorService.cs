using Admin.Areas.Admin.ViewModels;
using Admin.Areas.Responds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;
using System.Net.Mail;

namespace Admin.Areas.Admin.Repository
{
    public class DoctorService : IDoctorService<Doctor>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DoctorService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }

        public async Task<GenericRespond<Doctor>> CreateAsync(DoctorViewModel model, ApplicationUser user)
        {
            var result = await _userManager.CreateAsync(user, model.Password);
            var doctor = new Doctor
            {
                UserId = user.Id,
                Specialice = model.Specialice, 
                DepartmentId= model.DeptId,
            };
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Doctor");
                await _context.Doctors.AddAsync(doctor);
                await _context.SaveChangesAsync();
            }
            else
            {
                List<string> errors = new List<string>();
                foreach (var item in result.Errors)
                {
                    errors.Add(item.Description);
                }
                return new GenericRespond<Doctor> { Errors = errors, GenericRes = doctor };
            }

                return new GenericRespond<Doctor> { Errors = null, GenericRes = doctor };

            }

        public async Task<Doctor> DeleteAsync(string Id)
        {
           var doctor= await _context.Doctors.Include(p=>p.User).FirstOrDefaultAsync(m=>m.Id== Id);
            await _userManager.DeleteAsync(doctor.User);
            //_context.Doctors.Remove(doctor);
            //await _context.SaveChangesAsync();
            return doctor;
        }

        public IEnumerable<Doctor> GetALL()
        {
            return _context.Doctors.Include(n => n.User).ThenInclude(m => m.Admin).ToList();
        }

        public async Task<Doctor> GetbyIdAsync(string DoctorId)
        {
            return await _context.Doctors.Include(n => n.User).ThenInclude(m=>m.Admin).FirstOrDefaultAsync(m => m.Id == DoctorId);
        }

        public async Task<Doctor> UpdateAsync(string Id, DoctorEditViewModel model)
        {
            var item = await _context.Doctors.Include(m=>m.User).ThenInclude(m => m.Admin).FirstOrDefaultAsync(n=>n.Id==Id);
            item.Specialice = model.Specialice;
            item.User.PhoneNumber = model.PhoneNumber;
            item.User.Email = model.Email;
            item.User.Name = model.Name;
            item.CreateTime = DateTime.UtcNow.AddHours(3);
            item.User.UserName= new MailAddress(model.Email).User;
            item.DepartmentId = model.DeptId;
            _context.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
