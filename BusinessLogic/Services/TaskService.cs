//using Domain.Models;
//using Domain.Interfaces;
//using Microsoft.EntityFrameworkCore;
//using TaskModel = Domain.Models.Task;

//namespace BusinessLogic.Services
//{
//    public class TaskService : ITaskService
//    {
//        private IRepositoryWrapper _repositoryWrapper;

//        public TaskService(IRepositoryWrapper repositoryWrapper)
//        {
//            _repositoryWrapper = repositoryWrapper;
//        }

//        public async Task<List<TaskModel>> GetAll()
//        {
//            return await _repositoryWrapper.Task.FindAll();
//        }

//        public async Task<TaskModel> GetById(int id)
//        {
//            var task = await _repositoryWrapper.Task
//                .FindByCondition(x => x.TaskId == id);
//            return task.First();
//        }

//        public async System.Threading.Tasks.Task Create(TaskModel model)
//        {
//            if (model == null)
//            {
//                throw new ArgumentNullException(nameof(model));
//            }

//            if (string.IsNullOrWhiteSpace(model.TaskName))
//            {
//                throw new ArgumentException("Task name cannot be empty or whitespace", nameof(model.TaskName));
//            }

//            if (model.ProjectId <= 0)
//            {
//                throw new ArgumentException("ProjectId must be greater than 0", nameof(model.ProjectId));
//            }

//            if (model.TaskMaxScore <= 0)
//            {
//                throw new ArgumentException("Task max score must be greater than 0", nameof(model.TaskMaxScore));
//            }

//            await _repositoryWrapper.Task.Create(model);
//            await _repositoryWrapper.Save();
//        }

//        public async System.Threading.Tasks.Task Update(TaskModel model)
//        {
//            if (model == null)
//            {
//                throw new ArgumentNullException(nameof(model));
//            }

//            if (string.IsNullOrWhiteSpace(model.TaskName))
//            {
//                throw new ArgumentException("Task name cannot be empty or whitespace", nameof(model.TaskName));
//            }

//            if (model.ProjectId <= 0)
//            {
//                throw new ArgumentException("ProjectId must be greater than 0", nameof(model.ProjectId));
//            }

//            if (model.TaskMaxScore <= 0)
//            {
//                throw new ArgumentException("Task max score must be greater than 0", nameof(model.TaskMaxScore));
//            }

//            await _repositoryWrapper.Task.Update(model);
//            await _repositoryWrapper.Save();
//        }

//        public async System.Threading.Tasks.Task Delete(int id)
//        {
//            var task = await _repositoryWrapper.Task
//                .FindByCondition(x => x.TaskId == id);

//            if (task == null || !task.Any())
//            {
//                throw new InvalidOperationException($"Task with id {id} not found");
//            }

//            await _repositoryWrapper.Task.Delete(task.First());
//            await _repositoryWrapper.Save();
//        }
//    }
//}