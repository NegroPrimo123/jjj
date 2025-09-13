using Domain.Models;
using Domain.Interfaces;
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

        public async Task<List<Course>> GetAll()
        {
            return await _repositoryWrapper.Course.FindAll();
        }

        public async Task<Course> GetById(int id)
        {
            var course = await _repositoryWrapper.Course
                .FindByCondition(x => x.CourseId == id);
            return course.First();
        }

        public async System.Threading.Tasks.Task Create(Course model)
        {
            await _repositoryWrapper.Course.Create(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Course model)
        {
            _repositoryWrapper.Course.Update(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var course = await _repositoryWrapper.Course
                .FindByCondition(x => x.CourseId == id);

            _repositoryWrapper.Course.Delete(course.First());
            _repositoryWrapper.Save();
        }
    }
}