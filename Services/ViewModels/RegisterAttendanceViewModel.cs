namespace Services.ViewModels
{
    public class RegisterAttendanceViewModel
    {
        public string LecId { get; set; }
        public string StdId { get; set; }
        public DateTime ScanTime => DateTime.UtcNow.AddHours(3);
    }
}
