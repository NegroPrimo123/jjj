namespace task3.Contracts.Task
{
    public class GetTaskResponse
    {
        public string TaskName { get; set; } = null!;
        public int ProjectId { get; set; }
        public DateOnly TaskDeadline { get; set; }
        public int TaskMaxScore { get; set; }
    }
}
