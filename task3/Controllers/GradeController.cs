using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using task3.Contracts.Grade;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController : ControllerBase
    {
        private readonly IGradeService _gradeService;

        public GradeController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }

        /// <summary>
        /// Получение списка всех оценок
        /// </summary>
        /// <returns>Список всех оценок</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _gradeService.GetAll());
        }

        /// <summary>
        /// Получение оценки по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор оценки</param>
        /// <returns>Данные оценки</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _gradeService.GetById(id));
        }

        /// <summary>
        /// Получение оценки по идентификатору
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///         "submissionId": 1,
        ///         "teacherId": 1,
        ///         "score": 18,
        ///         "feedback": "Отличное проектирование архитектуры",
        ///         "gradingDate": "2025-09-12"
        ///     }
        ///     
        /// </remarks>
        /// <param name="id">Идентификатор оценки</param>
        /// <returns>Данные оценки</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateGradeRequest request)
        {
            var gradeDto = request.Adapt<Grade>();
            await _gradeService.Create(gradeDto);
            return Ok();
        }

        /// <summary>
        /// Обновление данных оценки
        /// </summary>
        /// <param name="grade">Обновленные данные оценки</param>
        /// <returns>Результат операции</returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateGradeRequest request)
        {
            var gradeDto = request.Adapt<Grade>();
            await _gradeService.Update(gradeDto);
            return Ok();
        }

        /// <summary>
        /// Удаление оценки по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор оценки</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _gradeService.Delete(id);
            return Ok();
        }
    }
}