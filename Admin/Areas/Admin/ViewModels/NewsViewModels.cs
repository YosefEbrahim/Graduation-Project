namespace Admin.Areas.Admin.ViewModels
{
    public class NewsViewModels
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreateTime { get; set; }
        public string AdminId { get; set; }
    }
}
