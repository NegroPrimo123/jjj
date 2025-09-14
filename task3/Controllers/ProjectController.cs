using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using task3.Contracts.Project;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        /// <summary>
        /// Получение списка всех проектов
        /// </summary>
        /// <returns>Список всех проектов</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _projectService.GetAll());
        }

        /// <summary>
        /// Получение проекта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор проекта</param>
        /// <returns>Данные проекта</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _projectService.GetById(id));
        }

        /// <summary>
        /// Создание нового проекта
        /// </summary>
        ///         /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///         "projectName": "Курсовой проект по C#",
        ///         "description": "Разработка консольного приложения",
        ///         "courseId": 1,
        ///         "maxScore": 100,
        ///         "deadline": "2025-09-12"
        ///     }
        ///
        /// </remarks>
        /// <param name="project">Данные проекта</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateProjectRequest request)
        {
            var projectDto = request.Adapt<Project>();
            await _projectService.Create(projectDto);
            return Ok();
        }

        /// <summary>
        /// Обновление данных проекта
        /// </summary>
        /// <param name="project">Обновленные данные проекта</param>
        /// <returns>Результат операции</returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateProjectRequest request)
        {
            var projectDto = request.Adapt<Project>();
            await _projectService.Update(projectDto);
            return Ok();
        }

        /// <summary>
        /// Удаление проекта по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор проекта</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectService.Delete(id);
            return Ok();
        }
    }
}