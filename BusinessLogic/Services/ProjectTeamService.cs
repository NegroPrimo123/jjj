using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class ProjectTeamService : IProjectTeamService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public ProjectTeamService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Projectteam>> GetAll()
        {
            return _repositoryWrapper.ProjectTeam.FindAll().ToListAsync();
        }

        public Task<Projectteam> GetById(int id)
        {
            var projectTeam = _repositoryWrapper.ProjectTeam
                .FindByCondition(x => x.TeamId == id).First(); // Изменено с ProjectTeamId на Id
            return System.Threading.Tasks.Task.FromResult(projectTeam);
        }

        public System.Threading.Tasks.Task Create(Projectteam model)
        {
            _repositoryWrapper.ProjectTeam.Create(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Update(Projectteam model)
        {
            _repositoryWrapper.ProjectTeam.Update(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Delete(int id)
        {
            var projectTeam = _repositoryWrapper.ProjectTeam
                .FindByCondition(x => x.TeamId == id).First(); // Изменено с ProjectTeamId на Id

            _repositoryWrapper.ProjectTeam.Delete(projectTeam);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}