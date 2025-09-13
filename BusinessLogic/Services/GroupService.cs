using Domain.Models;
using Domain.Interfaces;
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

        public async Task<List<Group>> GetAll()
        {
            return await _repositoryWrapper.Group.FindAll();
        }

        public async Task<Group> GetById(int id)
        {
            var group = await _repositoryWrapper.Group
                .FindByCondition(x => x.GroupId == id);
            return group.First();
        }

        public async System.Threading.Tasks.Task Create(Group model)
        {
            await _repositoryWrapper.Group.Create(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Group model)
        {
            _repositoryWrapper.Group.Update(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var group = await _repositoryWrapper.Group
                .FindByCondition(x => x.GroupId == id);

            _repositoryWrapper.Group.Delete(group.First());
            _repositoryWrapper.Save();
        }
    }
}