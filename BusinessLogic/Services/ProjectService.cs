using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
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

        public Task<List<Project>> GetAll()
        {
            return _repositoryWrapper.Project.FindAll().ToListAsync();
        }

        public Task<Project> GetById(int id)
        {
            var project = _repositoryWrapper.Project
                .FindByCondition(x => x.ProjectId == id).First();
            return System.Threading.Tasks.Task.FromResult(project);
        }

        public System.Threading.Tasks.Task Create(Project model)
        {
            _repositoryWrapper.Project.Create(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Update(Project model)
        {
            _repositoryWrapper.Project.Update(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Delete(int id)
        {
            var project = _repositoryWrapper.Project
                .FindByCondition(x => x.ProjectId == id).First();

            _repositoryWrapper.Project.Delete(project);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}