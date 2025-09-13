namespace task3.Contracts.Course
{
    public class CreateCourseRequest
    {
        public string CourseName { get; set; } = null!;
        public int TeacherId { get; set; }
    }
}
