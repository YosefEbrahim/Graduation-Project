using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models.Models;

namespace Models
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<News> Newses { get; set; }
        public DbSet<Models.Admin> Admins{ get; set; }
        public DbSet<Doctor> Doctors{ get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments{ get; set; }
        public DbSet<Exam> Exams{ get; set; }
        public DbSet<Lecture> Lectures{ get; set; }
        public DbSet<Subject> Subjects{ get; set; }
        public DbSet<Rank> Ranks{ get; set; }
        public DbSet<Question> Questions{ get; set; }
        public DbSet<LectureStudents> LectureStudents { get; set; }
        public DbSet<ExamStudents> ExamStudents { get; set; }
        public DbSet<DoctorRanks> DoctorRanks { get; set; }
        public DbSet<DoctorSubjects> DoctorSubjects { get; set; }
        public DbSet<Table> Tables{ get; set; }
        public DbSet<Messages> Messages{ get; set; }
        public DbSet<ExamAnswers> ExamAnswers{ get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Qrs_Code> Qrs_Code { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ApplicationUser>()
                .HasOne(p => p.Admin)
                .WithMany()
                .HasForeignKey(e => e.AdminId);
            base.OnModelCreating(builder);




            //builder.Entity<LectureStudent>().HasKey(sc => new { sc.StudentId, sc.LectureId });
            //        builder.Entity<LectureStudent>()
            //            .HasOne(bc => bc.Lecture)
            //            .WithMany(b => b.LectureStudents)
            //            .HasForeignKey(bc => bc.LectureId);
            //        builder.Entity<LectureStudent>()
            //            .HasOne(bc => bc.Student)
            //            .WithMany(b => b.LectureStudents)
            //            .HasForeignKey(bc => bc.StudentId);
            //builder.Entity<ExamStudent>().HasKey(sc => new { sc.StudentId, sc.ExamId });
            //builder.Entity<ExamStudent>()
            //    .HasOne(bc => bc.Exam)
            //    .WithMany(b => b.ExamStudents)
            //    .HasForeignKey(bc => bc.ExamId);
            //builder.Entity<ExamStudent>()
            //    .HasOne(bc => bc.Student)
            //    .WithMany(b => b.ExamStudents)
            //    .HasForeignKey(bc => bc.StudentId);
            //builder.Entity<DoctorRank>().HasKey(sc => new { sc.DoctorId, sc.RankId });
            //builder.Entity<DoctorRank>()
            //    .HasOne(bc => bc.Doctor)
            //    .WithMany(b => b.DoctorRanks)
            //    .HasForeignKey(bc => bc.DoctorId);
            //builder.Entity<DoctorRank>()
            //    .HasOne(bc => bc.Rank)
            //    .WithMany(b => b.DoctorRanks)
            //    .HasForeignKey(bc => bc.RankId);
            //builder.Entity<DoctorSubject>().HasKey(sc => new { sc.DoctorsId, sc.SubjectsId });
            //builder.Entity<DoctorSubject>()
            //    .HasOne(bc => bc.Doctor)
            //    .WithMany(b => b.DoctorSubjects)
            //    .HasForeignKey(bc => bc.DoctorsId);
            //builder.Entity<DoctorSubject>()
            //    .HasOne(bc => bc.Subject)
            //    .WithMany(b => b.DoctorSubjects)
            //    .HasForeignKey(bc => bc.SubjectsId);
            //builder.Entity<DoctorSubject>().HasKey(sc => new { sc.DoctorsId, sc.SubjectsId});

            //builder.Entity<Lecture_Student>()
            //    .HasOne<Student>(sc => sc.Student)
            //    .WithMany(s => s.Lecture_Students)
            //    .HasForeignKey(sc => sc.StudentId);


            //builder.Entity<Lecture_Student>()
            //    .HasOne<Lecture>(sc => sc.Lecture)
            //    .WithMany(s => s.Lecture_Students)
            //    .HasForeignKey(sc => sc.LectureId);
        }
   
        /*ReferentialAction.NoAction*/
    }
}
