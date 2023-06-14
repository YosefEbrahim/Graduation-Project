namespace Services.ViewModels
{
    public class LectureStudentsViewModel
    {
        public string StudentId { get; set; }
        public string StudentName { get; set; }
        public string UniversityId { get; set; }
        public string Major { get; set; }
        public string LectureId { get; set; }
        public string LecNumber { get; set; }
        public string SubjectName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }

    public class LectureStudentsChatbotViewModel
    {

        public string LecNumber { get; set; }
        public string SubjectName { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
