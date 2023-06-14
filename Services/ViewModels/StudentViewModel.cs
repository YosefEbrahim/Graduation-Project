namespace Services.ViewModels
{
    public class StudentViewModel
    {
        public string Message { get; set; }
        public DateTime CreateDate { get; set; }
        public string File { get; set; }
        public string DoctorName { get; set; }
    }


    public class StudentQuestionViewModel
    {

        public string QuestionId { get; set; }
        public string QuestionContent { get; set; }
        public string answer1 { get; set; }
        public string answer2 { get; set; }
        public string answer3 { get; set; }
        public string answer4 { get; set; }
        public string ExamName{ get; set; }
        public int Duration{ get; set; }
    }
    public class AnswerViewModel
    {
        public string QuestionId { get; set; }
        public string Answer { get; set; }
    }
    public class StudentExamsViewModel
    {

        public string ExamId { get; set; }
        public string Degree { get; set; }
        public string MainDegree { get; set; }
        public string SubjectName { get; set; }
        public string ExamName { get; set; }
        public string DoctorName { get; set; }
        public bool Status { get; set; }
        public DateTime StartTime { get; set; }
    }
}
