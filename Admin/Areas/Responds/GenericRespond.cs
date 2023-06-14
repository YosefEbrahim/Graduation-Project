using Models.Models;

namespace Admin.Areas.Responds
{
    public class GenericRespond<T>
    {
        public T GenericRes { get; set; }
        public List<string> Errors { get; set; }
    }
}
