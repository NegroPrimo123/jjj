namespace task3.Contracts.Teacher
{
    public class CreateTeacherRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public int DepartmentId { get; set; }
    }
}
