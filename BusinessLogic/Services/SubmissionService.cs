using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class SubmissionService : ISubmissionService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public SubmissionService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Submission>> GetAll()
        {
            return _repositoryWrapper.Submission.FindAll().ToListAsync();
        }

        public Task<Submission> GetById(int id)
        {
            var submission = _repositoryWrapper.Submission
                .FindByCondition(x => x.SubmissionId == id).First();
            return System.Threading.Tasks.Task.FromResult(submission);
        }

        public System.Threading.Tasks.Task Create(Submission model)
        {
            _repositoryWrapper.Submission.Create(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Update(Submission model)
        {
            _repositoryWrapper.Submission.Update(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Delete(int id)
        {
            var submission = _repositoryWrapper.Submission
                .FindByCondition(x => x.SubmissionId == id).First();

            _repositoryWrapper.Submission.Delete(submission);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}