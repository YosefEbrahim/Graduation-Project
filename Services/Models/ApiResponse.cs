namespace Services.Models
{
    public class ApiResponse
    {
        public bool Success => Errors == null;
        public string Message { get; set; }
        public List<int> Errors { get; private set; }

        public void AddError(int error)
        {
            if (Errors == null)
                Errors = new List<int>();

            Errors.Add(error);
        }

        public void AddErrors(List<int> errors)
        {
            if (Errors == null)
                Errors = new List<int>();

            Errors.AddRange(errors);
        }
    }

    public class ApiRespnse<T> : ApiResponse
    {
        public T Data { get; set; }
    }
}

