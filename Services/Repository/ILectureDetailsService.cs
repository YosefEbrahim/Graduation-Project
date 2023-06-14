using Admin.Areas.Admin.ViewModels;
using Services.ViewModels;

namespace Services.Repository
{
    public interface ILectureDetailsService
    {
        byte[] Make_Lecture(LectureViewModel Lec);
    }
}
