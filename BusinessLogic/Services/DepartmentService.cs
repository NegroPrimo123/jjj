using Domain.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class DepartmentService : IDepartmentService
    {
        private IRepositoryWrapper _repositoryWrapper;

        public DepartmentService(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        public async Task<List<Department>> GetAll()
        {
            return await _repositoryWrapper.Department.FindAll();
        }

        public async Task<Department> GetById(int id)
        {
            var department = await _repositoryWrapper.Department
                .FindByCondition(x => x.DepartmentId == id);
            return department.First();
        }

        public async System.Threading.Tasks.Task Create(Department model)
        {
            await _repositoryWrapper.Department.Create(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Department model)
        {
            _repositoryWrapper.Department.Update(model);
            _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var department = await _repositoryWrapper.Department
                .FindByCondition(x => x.DepartmentId == id);

            _repositoryWrapper.Department.Delete(department.First());
            _repositoryWrapper.Save();
        }
    }
}