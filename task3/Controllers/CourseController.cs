using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using task3.Contracts.Course;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService _courseService;

        public CourseController(ICourseService courseService)
        {
            _courseService = courseService;
        }

        /// <summary>
        /// Получение списка всех курсов
        /// </summary>
        /// <returns>Список всех курсов</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _courseService.GetAll());
        }

        /// <summary>
        /// Получение курса по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор курса</param>
        /// <returns>Данные курса</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _courseService.GetById(id));
        }

        /// <summary>
        /// Создание нового курса
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///         "courseName": "Программирование на C#",
        ///         "teacherId": 1
        ///     }
        ///
        /// </remarks>
        /// <param name="course">Данные курса</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateCourseRequest request)
        {
            var courseDto = request.Adapt<Course>();
            await _courseService.Create(courseDto);
            return Ok();
        }

        /// <summary>
        /// Обновление данных курса
        /// </summary>
        /// <param name="course">Обновленные данные курса</param>
        /// <returns>Результат операции</returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateCourseRequest request)
        {
            var courseDto = request.Adapt<Course>();
            await _courseService.Update(courseDto);
            return Ok();
        }

        /// <summary>
        /// Удаление курса по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор курса</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.Delete(id);
            return Ok();
        }
    }
}