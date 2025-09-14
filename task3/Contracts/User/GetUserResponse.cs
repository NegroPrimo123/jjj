using System.Text.RegularExpressions;

namespace task3.Contracts.User
{
    public class GetUserResponse
    {
        public int StudentId { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public int GroupId { get; set; }
    }
}
