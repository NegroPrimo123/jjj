using Domain.Models;
using DataAccess.Repositories;
using Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Wrapper;

namespace DataAccess.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private Task2DbContext _repoContext;

        private IStudentRepository? _student;
        private ICourseRepository? _course;
        private IDepartmentRepository? _department;
        private IGradeRepository? _grade;
        private IGroupRepository? _group;
        private IProjectRepository? _project;
        private IProjectTeamRepository? _projectTeam;
        private IStudentTeamRepository? _studentTeam;
        private ISubmissionRepository? _submission;
        private ITaskRepository? _task;
        private ITeacherRepository? _teacher;

        public IStudentRepository Student
        {
            get
            {
                if (_student == null)
                {
                    _student = new StudentRepository(_repoContext);
                }
                return _student;
            }
        }

        public ICourseRepository Course
        {
            get
            {
                if (_course == null)
                {
                    _course = new CourseRepository(_repoContext);
                }
                return _course;
            }
        }

        public IDepartmentRepository Department
        {
            get
            {
                if (_department == null)
                {
                    _department = new DepartmentRepository(_repoContext);
                }
                return _department;
            }
        }

        public IGradeRepository Grade
        {
            get
            {
                if (_grade == null)
                {
                    _grade = new GradeRepository(_repoContext);
                }
                return _grade;
            }
        }

        public IGroupRepository Group
        {
            get
            {
                if (_group == null)
                {
                    _group = new GroupRepository(_repoContext);
                }
                return _group;
            }
        }

        public IProjectRepository Project
        {
            get
            {
                if (_project == null)
                {
                    _project = new ProjectRepository(_repoContext);
                }
                return _project;
            }
        }

        public IProjectTeamRepository ProjectTeam
        {
            get
            {
                if (_projectTeam == null)
                {
                    _projectTeam = new ProjectTeamRepository(_repoContext);
                }
                return _projectTeam;
            }
        }

        public IStudentTeamRepository StudentTeam
        {
            get
            {
                if (_studentTeam == null)
                {
                    _studentTeam = new StudentTeamRepository(_repoContext);
                }
                return _studentTeam;
            }
        }

        public ISubmissionRepository Submission
        {
            get
            {
                if (_submission == null)
                {
                    _submission = new SubmissionRepository(_repoContext);
                }
                return _submission;
            }
        }

        public ITaskRepository Task
        {
            get
            {
                if (_task == null)
                {
                    _task = new TaskRepository(_repoContext);
                }
                return _task;
            }
        }

        public ITeacherRepository Teacher
        {
            get
            {
                if (_teacher == null)
                {
                    _teacher = new TeacherRepository(_repoContext);
                }
                return _teacher;
            }
        }

        public RepositoryWrapper(Task2DbContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }

        public async System.Threading.Tasks.Task Save()
        {
            await _repoContext.SaveChangesAsync();
        }
    }
}