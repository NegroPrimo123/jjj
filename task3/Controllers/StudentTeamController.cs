using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using task3.Contracts.StudentTeam;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentTeamController : ControllerBase
    {
        private readonly IStudentTeamService _studentTeamService;

        public StudentTeamController(IStudentTeamService studentTeamService)
        {
            _studentTeamService = studentTeamService;
        }

        /// <summary>
        /// Получение списка всех студенческих команд
        /// </summary>
        /// <returns>Список всех студенческих команд</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _studentTeamService.GetAll());
        }

        /// <summary>
        /// Получение студенческой команды по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор студенческой команды</param>
        /// <returns>Данные студенческой команды</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _studentTeamService.GetById(id));
        }

        /// <summary>
        /// Создание новой студенческой команды
        /// </summary>
        ///         /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///         "studentId": 1,
        ///         "teamId": 1,
        ///     }
        ///
        /// </remarks>
        /// <param name="studentTeam">Данные студенческой команды</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateStudentTeamRequest request)
        {
            var studentTeamDto = request.Adapt<Studentteam>();
            await _studentTeamService.Create(studentTeamDto);
            return Ok();
        }

        /// <summary>
        /// Обновление данных студенческой команды
        /// </summary>
        /// <param name="studentTeam">Обновленные данные студенческой команды</param>
        /// <returns>Результат операции</returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateStudentTeamRequest request)
        {
            var studentTeamDto = request.Adapt<Studentteam>();
            await _studentTeamService.Update(studentTeamDto);
            return Ok();
        }

        /// <summary>
        /// Удаление студенческой команды по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор студенческой команды</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentTeamService.Delete(id);
            return Ok();
        }
    }
}