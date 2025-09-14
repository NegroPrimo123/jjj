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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.FirstName))
            {
                throw new ArgumentException(nameof(model.FirstName));
            }

            await _repositoryWrapper.Student.Create(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Student model)
        {
            await _repositoryWrapper.Student.Update(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var user = await _repositoryWrapper.Student
                .FindByCondition(x => x.StudentId == id);

            await _repositoryWrapper.Student.Delete(user.First());
            await _repositoryWrapper.Save();
        }
    }
}