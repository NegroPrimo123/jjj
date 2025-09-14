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
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.DepartmentName))
            {
                throw new ArgumentException("Department name cannot be empty or whitespace", nameof(model.DepartmentName));
            }

            await _repositoryWrapper.Department.Create(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Update(Department model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            if (string.IsNullOrWhiteSpace(model.DepartmentName))
            {
                throw new ArgumentException("Department name cannot be empty or whitespace", nameof(model.DepartmentName));
            }

            await _repositoryWrapper.Department.Update(model);
            await _repositoryWrapper.Save();
        }

        public async System.Threading.Tasks.Task Delete(int id)
        {
            var department = await _repositoryWrapper.Department
                .FindByCondition(x => x.DepartmentId == id);

            await _repositoryWrapper.Department.Delete(department.First());
            await _repositoryWrapper.Save();
        }
    }
}