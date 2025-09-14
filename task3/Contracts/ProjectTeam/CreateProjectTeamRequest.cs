namespace task3.Contracts.ProjectTeam
{
    public class CreateProjectTeamRequest
    {
        public string TeamName { get; set; } = null!;
        public int ProjectId { get; set; }
    }
}
