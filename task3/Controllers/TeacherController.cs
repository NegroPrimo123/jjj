using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using task3.Contracts.Teacher;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;

        public TeacherController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        /// <summary>
        /// Получение списка всех преподавателей
        /// </summary>
        /// <returns>Список всех преподавателей</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _teacherService.GetAll());
        }

        /// <summary>
        /// Получение преподавателя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор преподавателя</param>
        /// <returns>Данные преподавателя</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _teacherService.GetById(id));
        }

        /// <summary>
        /// Создание нового преподавателя
        /// </summary>
        ///         /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///         "FirstName": Иван,
        ///         "LastName": "Иванов",
        ///         "DepartmentId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="teacher">Данные преподавателя</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateTeacherRequest request)
        {
            var teacherDto = request.Adapt<Teacher>();
            await _teacherService.Create(teacherDto);
            return Ok();
        }

        /// <summary>
        /// Обновление данных преподавателя
        /// </summary>
        /// <param name="teacher">Обновленные данные преподавателя</param>
        /// <returns>Результат операции</returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateTeacherRequest request)
        {
            var teacherDto = request.Adapt<Teacher>();
            await _teacherService.Update(teacherDto);
            return Ok();
        }

        /// <summary>
        /// Удаление преподавателя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор преподавателя</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teacherService.Delete(id);
            return Ok();
        }
    }
}