namespace task3.Contracts.Grade
{
    public class GetGradeResponse
    {
        public int SubmissionId { get; set; }
        public int TeacherId { get; set; }
        public int Score { get; set; }
        public string? Feedback { get; set; }
        public DateOnly GradingDate { get; set; }
    }
}
