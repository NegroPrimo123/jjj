using Domain.Models;
using Domain.Interfaces;
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

        public async Task<List<Submission>> GetAll()
        {
            return await _repositoryWrapper.Submission.FindAll();
        }

        public async Task<Submission> GetById(int id)
        {
            var submission = await _repositoryWrapper.Submission
                .FindByCondition(x => x.SubmissionId == id);
            return submission.First();
        }

        public async System.Threading.Tasks.Task Create(Submission model)
        {
            await _repositoryWrapper.Submission.Create(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Submission model)
        {
            _repositoryWrapper.Submission.Update(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var submission = await _repositoryWrapper.Submission
                .FindByCondition(x => x.SubmissionId == id);

            _repositoryWrapper.Submission.Delete(submission.First());
            _repositoryWrapper.Save();
        }
    }
}