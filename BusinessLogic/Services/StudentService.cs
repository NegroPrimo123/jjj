using Domain.Models;
using Domain.Interfaces;
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

        public async Task<List<Student>> GetAll()
        {
            return await _repositoryWrapper.Student.FindAll();
        }

        public async Task<Student> GetById(int id)
        {
            var user = await _repositoryWrapper.Student
                .FindByCondition(x => x.StudentId == id);
            return user.First();
        }

        public async System.Threading.Tasks.Task Create(Student model)
        {
            await _repositoryWrapper.Student.Create(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Student model)
        {
            _repositoryWrapper.Student.Update(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var user = await _repositoryWrapper.Student
                .FindByCondition(x => x.StudentId == id);

            _repositoryWrapper.Student.Delete(user.First());
            _repositoryWrapper.Save();
        }
    }
}