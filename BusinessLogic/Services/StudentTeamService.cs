using Domain.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class StudentTeamService : IStudentTeamService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public StudentTeamService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Studentteam>> GetAll()
        {
            return await _repositoryWrapper.StudentTeam.FindAll();
        }

        public async Task<Studentteam> GetById(int id)
        {
            var studentTeam = await _repositoryWrapper.StudentTeam
                .FindByCondition(x => x.Id == id);
            return studentTeam.First();
        }

        public async System.Threading.Tasks.Task Create(Studentteam model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.StudentId <= 0)
            {
                throw new ArgumentException(nameof(model.StudentId));
            }

            if (model.TeamId <= 0)
            {
                throw new ArgumentException(nameof(model.TeamId));
            }

            await _repositoryWrapper.StudentTeam.Create(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Studentteam model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (model.StudentId <= 0)
            {
                throw new ArgumentException(nameof(model.StudentId));
            }

            if (model.TeamId <= 0)
            {
                throw new ArgumentException(nameof(model.TeamId));
            }

            await _repositoryWrapper.StudentTeam.Update(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var studentTeam = await _repositoryWrapper.StudentTeam
                .FindByCondition(x => x.Id == id);

            await _repositoryWrapper.StudentTeam.Delete(studentTeam.First());
            await _repositoryWrapper.Save();
        }
    }
}