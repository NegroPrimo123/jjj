//using Domain.Models;
//using Domain.Interfaces;
//using Microsoft.EntityFrameworkCore;

//namespace BusinessLogic.Services
//{
//    public class TaskService : ITaskService
//    {
//        private IRepositoryWrapper _repositoryWrapper;

//        public TaskService(IRepositoryWrapper repositoryWrapper)
//        {
//            _repositoryWrapper = repositoryWrapper;
//        }

//        public async Task<List<System.Threading.Tasks.Task>> GetAll()
//        {
//            return await _repositoryWrapper.Task.FindAll();
//        }

//        public async Task<System.Threading.Tasks.Task> GetById(int id)
//        {
//            var task = await _repositoryWrapper.Task
//                .FindByCondition(x => x.TaskId == id);
//            return task.First();
//        }

//        public async System.Threading.Tasks.Task Create(System.Threading.Tasks.Task model)
//        {
//            await _repositoryWrapper.Task.Create(model);
//            _repositoryWrapper.Save();
//        }

//        public async System.Threading.Tasks.Task Update(System.Threading.Tasks.Task model)
//        {
//            _repositoryWrapper.Task.Update(model);
//            _repositoryWrapper.Save();
//        }

//        public async System.Threading.Tasks.Task Delete(int id)
//        {
//            var task = await _repositoryWrapper.Task
//                .FindByCondition(x => x.TaskId == id);

//            _repositoryWrapper.Task.Delete(task.First());
//            _repositoryWrapper.Save();
//        }
//    }
//}