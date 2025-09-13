using Domain.Models;
using Domain.Interfaces;
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

        public async Task<List<Projectteam>> GetAll()
        {
            return await _repositoryWrapper.ProjectTeam.FindAll();
        }

        public async Task<Projectteam> GetById(int id)
        {
            var projectTeam = await _repositoryWrapper.ProjectTeam
                .FindByCondition(x => x.TeamId == id);
            return projectTeam.First();
        }

        public async System.Threading.Tasks.Task Create(Projectteam model)
        {
            await _repositoryWrapper.ProjectTeam.Create(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Projectteam model)
        {
            _repositoryWrapper.ProjectTeam.Update(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var projectTeam = await _repositoryWrapper.ProjectTeam
                .FindByCondition(x => x.TeamId == id);

            _repositoryWrapper.ProjectTeam.Delete(projectTeam.First());
            _repositoryWrapper.Save();
        }
    }
}