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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.GroupName))
            {
                throw new ArgumentException(nameof(model.GroupName));
            }

            await _repositoryWrapper.Group.Create(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Group model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrEmpty(model.GroupName))
            {
                throw new ArgumentException(nameof(model.GroupName));
            }

            await _repositoryWrapper.Group.Update(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var group = await _repositoryWrapper.Group
                .FindByCondition(x => x.GroupId == id);

            await _repositoryWrapper.Group.Delete(group.First());
            await _repositoryWrapper.Save();
        }
    }
}