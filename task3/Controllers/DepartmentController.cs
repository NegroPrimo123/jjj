using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using task3.Contracts.Grade;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// Получение списка всех кафедр
        /// </summary>
        /// <returns>Список всех кафедр</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _departmentService.GetAll());
        }

        /// <summary>
        /// Получение кафедры по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор кафедры</param>
        /// <returns>Данные кафедры</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _departmentService.GetById(id));
        }

        /// <summary>
        /// Создание новой кафедры
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///         "departmentName": "Кафедра информационных технологий"
        ///     }
        ///
        /// </remarks>
        /// <param name="department">Данные кафедры</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateGradeRequest request)
        {
            var departmentDto = request.Adapt<Department>();
            await _departmentService.Create(departmentDto);
            return Ok();
        }

        /// <summary>
        /// Обновление данных кафедры
        /// </summary>
        /// <param name="department">Обновленные данные кафедры</param>
        /// <returns>Результат операции</returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateGradeRequest request)
        {
            var departmentDto = request.Adapt<Department>();
            await _departmentService.Update(departmentDto);
            return Ok();
        }

        /// <summary>
        /// Удаление кафедры по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор кафедры</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _departmentService.Delete(id);
            return Ok();
        }
    }
}