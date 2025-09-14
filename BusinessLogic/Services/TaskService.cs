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
//            if (model == null)
//            {
//                throw new ArgumentNullException(nameof(model));
//            }

//            if (string.IsNullOrEmpty(model.TaskName))
//            {
//                throw new ArgumentException(nameof(model.TaskName));
//            }

//            if (model.ProjectId <= 0)
//            {
//                throw new ArgumentException(nameof(model.ProjectId));
//            }

//            await _repositoryWrapper.Task.Create(model);
//            await _repositoryWrapper.Save();
//        }

//        public async System.Threading.Tasks.Task Update(System.Threading.Tasks.Task model)
//        {
//            if (model == null)
//            {
//                throw new ArgumentNullException(nameof(model));
//            }

//            if (string.IsNullOrEmpty(model.TaskName))
//            {
//                throw new ArgumentException(nameof(model.TaskName));
//            }

//            if (model.ProjectId <= 0)
//            {
//                throw new ArgumentException( nameof(model.ProjectId));
//            }

//            await _repositoryWrapper.Task.Update(model);
//            await _repositoryWrapper.Save();
//        }

//        public async System.Threading.Tasks.Task Delete(int id)
//        {
//            var task = await _repositoryWrapper.Task
//                .FindByCondition(x => x.TaskId == id);

//            await _repositoryWrapper.Task.Delete(task.First());
//            await _repositoryWrapper.Save();
//        }
//    }
//}