using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
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

        public Task<List<Studentteam>> GetAll()
        {
            return _repositoryWrapper.StudentTeam.FindAll().ToListAsync();
        }

        public Task<Studentteam> GetById(int id)
        {
            var studentTeam = _repositoryWrapper.StudentTeam
                .FindByCondition(x => x.Id == id).First(); // Изменено с StudentTeamId на Id
            return System.Threading.Tasks.Task.FromResult(studentTeam);
        }

        public System.Threading.Tasks.Task Create(Studentteam model)
        {
            _repositoryWrapper.StudentTeam.Create(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Update(Studentteam model)
        {
            _repositoryWrapper.StudentTeam.Update(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Delete(int id)
        {
            var studentTeam = _repositoryWrapper.StudentTeam
                .FindByCondition(x => x.Id == id).First(); // Изменено с StudentTeamId на Id

            _repositoryWrapper.StudentTeam.Delete(studentTeam);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}