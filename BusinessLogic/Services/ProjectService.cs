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
            await _repositoryWrapper.Project.Create(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Project model)
        {
            _repositoryWrapper.Project.Update(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var project = await _repositoryWrapper.Project
                .FindByCondition(x => x.ProjectId == id);

            _repositoryWrapper.Project.Delete(project.First());
            _repositoryWrapper.Save();
        }
    }
}