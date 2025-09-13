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
            await _repositoryWrapper.Teacher.Create(model);
        }

        public async System.Threading.Tasks.Task Update(Teacher model)
        {
            _repositoryWrapper.Teacher.Update(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var teacher = await _repositoryWrapper.Teacher
                .FindByCondition(x => x.TeacherId == id);

            _repositoryWrapper.Teacher.Delete(teacher.First());
            _repositoryWrapper.Save();
        }
    }
}