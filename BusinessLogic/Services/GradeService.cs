using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
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

        public Task<List<Grade>> GetAll()
        {
            return _repositoryWrapper.Grade.FindAll().ToListAsync();
        }

        public Task<Grade> GetById(int id)
        {
            var grade = _repositoryWrapper.Grade
                .FindByCondition(x => x.GradeId == id).First();
            return System.Threading.Tasks.Task.FromResult(grade);
        }

        public System.Threading.Tasks.Task Create(Grade model)
        {
            _repositoryWrapper.Grade.Create(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Update(Grade model)
        {
            _repositoryWrapper.Grade.Update(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Delete(int id)
        {
            var grade = _repositoryWrapper.Grade
                .FindByCondition(x => x.GradeId == id).First();

            _repositoryWrapper.Grade.Delete(grade);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}