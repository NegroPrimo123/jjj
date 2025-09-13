using Domain.Models;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryWrapper
    {
        IStudentRepository Student { get; }
        ICourseRepository Course { get; }
        IDepartmentRepository Department { get; }
        IGradeRepository Grade { get; }
        IGroupRepository Group { get; }
        IProjectRepository Project { get; }
        IProjectTeamRepository ProjectTeam { get; }
        IStudentTeamRepository StudentTeam { get; }
        ISubmissionRepository Submission { get; }
        ITaskRepository Task { get; }
        ITeacherRepository Teacher { get; }
        System.Threading.Tasks.Task Save();
    }
}