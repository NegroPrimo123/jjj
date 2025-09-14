using Domain.Models;
using Domain.Interfaces;
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

        public async Task<List<Teacher>> GetAll()
        {
            return await _repositoryWrapper.Teacher.FindAll();
        }

        public async Task<Teacher> GetById(int id)
        {
            var teacher = await _repositoryWrapper.Teacher
                .FindByCondition(x => x.TeacherId == id);
            return teacher.First();
        }

        public async System.Threading.Tasks.Task Create(Teacher model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.FirstName))
            {
                throw new ArgumentException(nameof(model.FirstName));
            }

            if (string.IsNullOrEmpty(model.LastName))
            {
                throw new ArgumentException(nameof(model.LastName));
            }

            await _repositoryWrapper.Teacher.Create(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Teacher model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.FirstName))
            {
                throw new ArgumentException(nameof(model.FirstName));
            }

            if (string.IsNullOrEmpty(model.LastName))
            {
                throw new ArgumentException(nameof(model.LastName));
            }

            await _repositoryWrapper.Teacher.Update(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var teacher = await _repositoryWrapper.Teacher
                .FindByCondition(x => x.TeacherId == id);

            await _repositoryWrapper.Teacher.Delete(teacher.First());
            await _repositoryWrapper.Save();
        }
    }
}