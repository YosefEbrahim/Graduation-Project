using System.ComponentModel.DataAnnotations.Schema;

namespace Models.Models
{
    public class News
    {
        public News()
        {
            this.Id = Guid.NewGuid().ToString();
            this.CreateTime = DateTime.UtcNow.AddHours(3);

        }
        public string Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public byte[]? Image { get; set; }
        public DateTime CreateTime { get; set; }
        [ForeignKey("Admin")]
        public string AdminId { get; set; }
        public Admin Admin { get; set; }
    }
}
