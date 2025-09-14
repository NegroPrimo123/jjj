namespace task3.Contracts.Submission
{
    public class CreateSubmissionRequest
    {
        public int TeamId { get; set; }
        public int TaskId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string? FilePath { get; set; }
        public string Status { get; set; } = null!;
    }
}
