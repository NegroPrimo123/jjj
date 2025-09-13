using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using task3.Contracts.Task;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        /// <summary>
        /// Получение списка всех задач
        /// </summary>
        /// <returns>Список всех задач</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _taskService.GetAll());
        }

        /// <summary>
        /// Получение задачи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns>Данные задачи</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _taskService.GetById(id));
        }

        /// <summary>
        /// Создание новой задачи
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///         "TaskName": Проектирование БД,
        ///         "ProjectId": "1",
        ///         "TaskDeadline": "2025-09-12",
        ///         "TaskMaxScore": 100
        ///     }
        ///
        /// </remarks>
        /// <param name="task">Данные задачи</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateTaskRequest request)
        {
            var taskDto = request.Adapt<Domain.Models.Task>();
            await _taskService.Create(taskDto);
            return Ok();
        }

        /// <summary>
        /// Обновление данных задачи
        /// </summary>
        /// <param name="task">Обновленные данные задачи</param>
        /// <returns>Результат операции</returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateTaskRequest request)
        {
            var taskDto = request.Adapt<Domain.Models.Task>();
            await _taskService.Update(taskDto);
            return Ok();
        }

        /// <summary>
        /// Удаление задачи по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор задачи</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.Delete(id);
            return Ok();
        }
    }
}