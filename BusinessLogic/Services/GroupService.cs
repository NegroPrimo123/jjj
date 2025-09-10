using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class GroupService : IGroupService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public GroupService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public Task<List<Group>> GetAll()
        {
            return _repositoryWrapper.Group.FindAll().ToListAsync();
        }

        public Task<Group> GetById(int id)
        {
            var group = _repositoryWrapper.Group
                .FindByCondition(x => x.GroupId == id).First();
            return System.Threading.Tasks.Task.FromResult(group);
        }

        public System.Threading.Tasks.Task Create(Group model)
        {
            _repositoryWrapper.Group.Create(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Update(Group model)
        {
            _repositoryWrapper.Group.Update(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Delete(int id)
        {
            var group = _repositoryWrapper.Group
                .FindByCondition(x => x.GroupId == id).First();

            _repositoryWrapper.Group.Delete(group);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}