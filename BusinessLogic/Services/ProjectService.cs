using Domain.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class ProjectService : IProjectService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ProjectService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Project>> GetAll()
        {
            return await _repositoryWrapper.Project.FindAll();
        }

        public async Task<Project> GetById(int id)
        {
            var project = await _repositoryWrapper.Project
                .FindByCondition(x => x.ProjectId == id);
            return project.First();
        }

        public async System.Threading.Tasks.Task Create(Project model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.ProjectName))
            {
                throw new ArgumentException("Project name cannot be empty or whitespace", nameof(model.ProjectName));
            }

            if (model.CourseId <= 0)
            {
                throw new ArgumentException("CourseId must be greater than 0", nameof(model.CourseId));
            }

            if (model.MaxScore <= 0)
            {
                throw new ArgumentException("Max score must be greater than 0", nameof(model.MaxScore));
            }

            await _repositoryWrapper.Project.Create(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Project model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.ProjectName))
            {
                throw new ArgumentException("Project name cannot be empty or whitespace", nameof(model.ProjectName));
            }

            if (model.CourseId <= 0)
            {
                throw new ArgumentException("CourseId must be greater than 0", nameof(model.CourseId));
            }

            if (model.MaxScore <= 0)
            {
                throw new ArgumentException("Max score must be greater than 0", nameof(model.MaxScore));
            }

            await _repositoryWrapper.Project.Update(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var project = await _repositoryWrapper.Project
                .FindByCondition(x => x.ProjectId == id);

            await _repositoryWrapper.Project.Delete(project.First());
            await _repositoryWrapper.Save();
        }
    }
}