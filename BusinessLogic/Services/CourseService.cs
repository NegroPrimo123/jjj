using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class CourseService : ICourseService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public CourseService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Course>> GetAll()
        {
            return _repositoryWrapper.Course.FindAll().ToListAsync();
        }

        public Task<Course> GetById(int id)
        {
            var course = _repositoryWrapper.Course
                .FindByCondition(x => x.CourseId == id).First();
            return System.Threading.Tasks.Task.FromResult(course);
        }

        public System.Threading.Tasks.Task Create(Course model)
        {
            _repositoryWrapper.Course.Create(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Update(Course model)
        {
            _repositoryWrapper.Course.Update(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Delete(int id)
        {
            var course = _repositoryWrapper.Course
                .FindByCondition(x => x.CourseId == id).First();

            _repositoryWrapper.Course.Delete(course);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}