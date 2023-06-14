namespace Admin.Areas.Admin.ViewModels
{
    public class NewsDetails
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreateTime { get; set; }
        public string AdminName { get; set; }
    }
}
