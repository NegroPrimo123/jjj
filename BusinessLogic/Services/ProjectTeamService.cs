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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.TeamName))
            {
                throw new ArgumentException(nameof(model.TeamName));
            }

            await _repositoryWrapper.ProjectTeam.Create(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Projectteam model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.TeamName))
            {
                throw new ArgumentException(nameof(model.TeamName));
            }

            await _repositoryWrapper.ProjectTeam.Update(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var projectTeam = await _repositoryWrapper.ProjectTeam
                .FindByCondition(x => x.TeamId == id);

            await _repositoryWrapper.ProjectTeam.Delete(projectTeam.First());
            await _repositoryWrapper.Save();
        }
    }
}