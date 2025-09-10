using BusinessLogic.Interfaces;
using DataAccess.Models;
using DataAccess.Wrapper;
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

        public Task<List<Department>> GetAll()
        {
            return _repositoryWrapper.Department.FindAll().ToListAsync();
        }

        public Task<Department> GetById(int id)
        {
            var department = _repositoryWrapper.Department
                .FindByCondition(x => x.DepartmentId == id).First();
            return System.Threading.Tasks.Task.FromResult(department);
        }

        public System.Threading.Tasks.Task Create(Department model)
        {
            _repositoryWrapper.Department.Create(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Update(Department model)
        {
            _repositoryWrapper.Department.Update(model);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }

        public System.Threading.Tasks.Task Delete(int id)
        {
            var department = _repositoryWrapper.Department
                .FindByCondition(x => x.DepartmentId == id).First();

            _repositoryWrapper.Department.Delete(department);
            _repositoryWrapper.Save();
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}