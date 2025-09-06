using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class StudentService : IStudentService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public StudentService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Student>> GetAll()
        {
            return _repositoryWrapper.Student.FindAll().ToListAsync();
        }

        public Task<Student> GetById(int id)
        {
            var user = _repositoryWrapper.Student
                .FindByCondition(x => x.StudentId == id).First();
            return System.Threading.Tasks.Task.FromResult(user);
        }

        public System.Threading.Tasks.Task Create(Student model)
        {
            _repositoryWrapper.Student.Create(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Update(Student model)
        {
            _repositoryWrapper.Student.Update(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Delete(int id)
        {
            var user = _repositoryWrapper.Student
                .FindByCondition(x => x.StudentId == id).First();

            _repositoryWrapper.Student.Delete(user);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}