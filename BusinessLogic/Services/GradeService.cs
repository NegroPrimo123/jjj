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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Score < 0 || model.Score > 100)
            {
                throw new ArgumentException(nameof(model.Score));
            }

            await _repositoryWrapper.Grade.Create(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Grade model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.Score < 0 || model.Score > 100)
            {
                throw new ArgumentException(nameof(model.Score));
            }

            await _repositoryWrapper.Grade.Update(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var grade = await _repositoryWrapper.Grade
                .FindByCondition(x => x.GradeId == id);

            await _repositoryWrapper.Grade.Delete(grade.First());
            await _repositoryWrapper.Save();
        }
    }
}