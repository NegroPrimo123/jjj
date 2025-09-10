using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class TeacherService : ITeacherService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public TeacherService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Teacher>> GetAll()
        {
            return _repositoryWrapper.Teacher.FindAll().ToListAsync();
        }

        public Task<Teacher> GetById(int id)
        {
            var teacher = _repositoryWrapper.Teacher
                .FindByCondition(x => x.TeacherId == id).First();
            return System.Threading.Tasks.Task.FromResult(teacher);
        }

        public System.Threading.Tasks.Task Create(Teacher model)
        {
            _repositoryWrapper.Teacher.Create(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Update(Teacher model)
        {
            _repositoryWrapper.Teacher.Update(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Delete(int id)
        {
            var teacher = _repositoryWrapper.Teacher
                .FindByCondition(x => x.TeacherId == id).First();

            _repositoryWrapper.Teacher.Delete(teacher);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}