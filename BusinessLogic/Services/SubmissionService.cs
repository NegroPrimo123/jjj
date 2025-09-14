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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.TeamId <= 0)
            {
                throw new ArgumentException(nameof(model.TeamId));
            }

            if (model.TaskId <= 0)
            {
                throw new ArgumentException(nameof(model.TaskId));
            }

            if (string.IsNullOrWhiteSpace(model.FilePath))
            {
                throw new ArgumentException(nameof(model.FilePath));
            }

            if (string.IsNullOrWhiteSpace(model.Status))
            {
                throw new ArgumentException(nameof(model.Status));
            }

            await _repositoryWrapper.Submission.Create(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Submission model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.TeamId <= 0)
            {
                throw new ArgumentException(nameof(model.TeamId));
            }

            if (model.TaskId <= 0)
            {
                throw new ArgumentException(nameof(model.TaskId));
            }

            if (string.IsNullOrWhiteSpace(model.FilePath))
            {
                throw new ArgumentException(nameof(model.FilePath));
            }

            if (string.IsNullOrWhiteSpace(model.Status))
            {
                throw new ArgumentException(nameof(model.Status));
            }

            await _repositoryWrapper.Submission.Update(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var submission = await _repositoryWrapper.Submission
                .FindByCondition(x => x.SubmissionId == id);

            if (submission == null || !submission.Any())
            {
                throw new ArgumentException($"Submission with id {id} not found");
            }

            await _repositoryWrapper.Submission.Delete(submission.First());
            await _repositoryWrapper.Save();
        }
    }
}