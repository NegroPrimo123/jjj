using Domain.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class GradeService : IGradeService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public GradeService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Grade>> GetAll()
        {
            return await _repositoryWrapper.Grade.FindAll();
        }

        public async Task<Grade> GetById(int id)
        {
            var grade = await _repositoryWrapper.Grade
                .FindByCondition(x => x.GradeId == id);
            return grade.First();
        }

        public async System.Threading.Tasks.Task Create(Grade model)
        {
            await _repositoryWrapper.Grade.Create(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Grade model)
        {
            _repositoryWrapper.Grade.Update(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var grade = await _repositoryWrapper.Grade
                .FindByCondition(x => x.GradeId == id);

            _repositoryWrapper.Grade.Delete(grade.First());
            _repositoryWrapper.Save();
        }
    }
}