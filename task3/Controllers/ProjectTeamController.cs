using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using task3.Contracts.ProjectTeam;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTeamController : ControllerBase
    {
        private readonly IProjectTeamService _projectTeamService;

        public ProjectTeamController(IProjectTeamService projectTeamService)
        {
            _projectTeamService = projectTeamService;
        }

        /// <summary>
        /// Получение списка всех проектных команд
        /// </summary>
        /// <returns>Список всех проектных команд</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _projectTeamService.GetAll());
        }

        /// <summary>
        /// Получение проектной команды по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор проектной команды</param>
        /// <returns>Данные проектной команды</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _projectTeamService.GetById(id));
        }

        /// <summary>
        /// Создание новой проектной команды
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///         "teamName": "Команда Alpha",
        ///         "projectId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="projectTeam">Данные проектной команды</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateProjectTeamRequest request)
        {
            var projectTeamDto = request.Adapt<Projectteam>();
            await _projectTeamService.Create(projectTeamDto);
            return Ok();
        }

        /// <summary>
        /// Обновление данных проектной команды
        /// </summary>
        /// <param name="projectTeam">Обновленные данные проектной команды</param>
        /// <returns>Результат операции</returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateProjectTeamRequest request)
        {
            var projectTeamDto = request.Adapt<Projectteam>();
            await _projectTeamService.Update(projectTeamDto);
            return Ok();
        }

        /// <summary>
        /// Удаление проектной команды по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор проектной команды</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectTeamService.Delete(id);
            return Ok();
        }
    }
}