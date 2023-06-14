namespace Services.ViewModels
{
    public class GenericViewModel
    {
        public class SubjectsViewModel
        {
            public string Id { get; set; }
            public string SubjectName { get; set; }
        }
        public class DepartmentsViewModel
        {
            public string Id { get; set; }
            public string DepartmentName { get; set; }
            public string Description { get; set; }
        }
        public class RanksViewModel
        {
            public string Id { get; set; }
            public string RankName { get; set; }
        }
    }
}
