//using BusinessLogic.Interfaces;
//using DataAccess.Models;
//using DataAccess.Wrapper;
//using Microsoft.EntityFrameworkCore;
//using TaskModel = DataAccess.Models.Task;

//namespace BusinessLogic.Services
//{
//    public class TaskService : ITaskService
//    {
//        private IRepositoryWrapper _repositoryWrapper;

//        public TaskService(IRepositoryWrapper repositoryWrapper)
//        {
//            _repositoryWrapper = repositoryWrapper;
//        }

//        public Task<List<System.Threading.Tasks.Task>> GetAll()
//        {
//            return _repositoryWrapper.Task.FindAll().ToListAsync();
//        }

//        public Task<System.Threading.Tasks.Task> GetById(int id)
//        {
//            var task = _repositoryWrapper.Task
//                .FindByCondition(x => x.TaskId == id).First();
//            return System.Threading.Tasks.Task.FromResult(task);
//        }

//        public System.Threading.Tasks.Task Create(System.Threading.Tasks.Task model)
//        {
//            _repositoryWrapper.Task.Create(model);
//            _repositoryWrapper.Save();
//            return System.Threading.Tasks.Task.CompletedTask;
//        }

//        public System.Threading.Tasks.Task Update(System.Threading.Tasks.Task model)
//        {
//            _repositoryWrapper.Task.Update(model);
//            _repositoryWrapper.Save();
//            return System.Threading.Tasks.Task.CompletedTask;
//        }

//        public System.Threading.Tasks.Task Delete(int id)
//        {
//            var task = _repositoryWrapper.Task
//                .FindByCondition(x => x.TaskId == id).First();

//            _repositoryWrapper.Task.Delete(task);
//            _repositoryWrapper.Save();
//            return System.Threading.Tasks.Task.CompletedTask;
//        }
//    }
//}