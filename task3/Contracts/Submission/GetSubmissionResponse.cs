namespace task3.Contracts.Submission
{
    public class GetSubmissionResponse
    {
        public int TeamId { get; set; }
        public int TaskId { get; set; }
        public DateTime SubmissionDate { get; set; }
        public string? FilePath { get; set; }
        public string Status { get; set; } = null!;
    }
}
