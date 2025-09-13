namespace task3.Contracts.Project
{
    public class CreateProjectRequest
    {
        public string ProjectName { get; set; } = null!;
        public string? Description { get; set; }
        public int CourseId { get; set; }
        public int MaxScore { get; set; }
        public DateOnly Deadline { get; set; }
    }
}
