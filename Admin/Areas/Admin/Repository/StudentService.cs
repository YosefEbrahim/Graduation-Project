using Admin.Areas.Admin.ViewModels;
using Admin.Areas.Responds;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Models;
using System.Net.Mail;
using System.Numerics;
using System.Security.Claims;

namespace Admin.Areas.Admin.Repository
{
    public class StudentService : IStudentService<Student>
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        public StudentService(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            this._userManager = userManager;
        }
    public async Task<GenericRespond<Student>> CreateAsync(StudentViewModel Model)
        {
            var user = new ApplicationUser
            {
                PhoneNumber = Model.PhoneNumber,
                Email = Model.Email,
                UserName = new MailAddress(Model.Email).User,
                Name = Model.Name,
                AdminId = Model.AdminId,
                
            };
            var result = await _userManager.CreateAsync(user, Model.Password);
            var student = new Student
            {
                UserId = user.Id,
                UniversityId = Model.UniversityId,
                Major = Model.Major,
                Faculty = Model.Faculty,
                Class = Model.Class,
                DepartmentId = Model.DeptId,
                RankId=Model.RankId,
                Section_Num=Model.Section_Num
            };
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Student");
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
            }
            else
            {
                List<string> errors = new List<string>();
                foreach (var item in result.Errors)
                {
                    errors.Add(item.Description);
                }
                return new GenericRespond<Student> { Errors = errors, GenericRes = student };
            }

            return new GenericRespond<Student> { Errors=null,GenericRes=student};
        }

        public async Task<Student> DeleteAsync(string Id)
        {
            var student = await _context.Students.Include(p => p.User).FirstOrDefaultAsync(m => m.Id == Id);
            await _userManager.DeleteAsync(student.User);
            //_context.Students.Remove(student);
            //await _context.SaveChangesAsync();
            return student;
        }

        public IEnumerable<Student> GetALL()
        {
            return _context.Students.Include(m => m.Department).Include(n => n.User).ThenInclude(m => m.Admin).ToList();
        }

        public async Task<Student> GetbyIdAsync(string Id)
        {
            return await _context.Students.Include(m => m.Department).Include(n => n.User).ThenInclude(m => m.Admin).FirstOrDefaultAsync(m => m.Id == Id);
        }

        public async Task<Student> UpdateAsync(string Id,StudentViewModel Model)
        {
            var item = await _context.Students.Include(m => m.Department).Include(m => m.User).ThenInclude(m => m.Admin).FirstOrDefaultAsync(n => n.Id == Id);
            item.Major = Model.Major;
            item.User.PhoneNumber = Model.PhoneNumber;
            item.User.Email = Model.Email;
            item.User.Name = Model.Name;
            item.Faculty = Model.Faculty;
            item.Class = Model.Class;
            item.User.UserName = new MailAddress(Model.Email).User;
            item.DepartmentId = Model.DeptId;
            item.Section_Num = Model.Section_Num;
            _context.Students.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }
    }
}
