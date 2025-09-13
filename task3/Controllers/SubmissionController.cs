using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using task3.Contracts.Submission;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubmissionController : ControllerBase
    {
        private readonly ISubmissionService _submissionService;

        public SubmissionController(ISubmissionService submissionService)
        {
            _submissionService = submissionService;
        }

        /// <summary>
        /// Получение списка всех отправленных работ
        /// </summary>
        /// <returns>Список всех отправленных работ</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _submissionService.GetAll());
        }

        /// <summary>
        /// Получение отправленной работы по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор отправленной работы</param>
        /// <returns>Данные отправленной работы</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _submissionService.GetById(id));
        }

        /// <summary>
        /// Создание новой отправленной работы
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///         "teamId": 1,
        ///         "taskId": 1,
        ///         "submissionDate": "2025-09-12 23:20:00",
        ///         "filePath": "/files/analysis.docx",
        ///         "status": "submitted"
        ///     }
        ///
        /// </remarks>
        /// <param name="submission">Данные отправленной работы</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateSubmissionRequest request)
        {
            var submissionDto = request.Adapt<Submission>();
            await _submissionService.Create(submissionDto);
            return Ok();
        }

        /// <summary>
        /// Обновление данных отправленной работы
        /// </summary>
        /// <param name="submission">Обновленные данные отправленной работы</param>
        /// <returns>Результат операции</returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateSubmissionRequest request)
        {
            var submissionDto = request.Adapt<Submission>();
            await _submissionService.Update(submissionDto);
            return Ok();
        }

        /// <summary>
        /// Удаление отправленной работы по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор отправленной работы</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _submissionService.Delete(id);
            return Ok();
        }
    }
}