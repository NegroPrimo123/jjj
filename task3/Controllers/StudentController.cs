using BusinessLogic.Services;
using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using task3.Contracts.User;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IStudentService _userService;
        public UserController(IStudentService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Получение списка всех студентов
        /// </summary>
        /// <returns>Список всех пользователей</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _userService.GetAll());
        }

        /// <summary>
        /// Получение студента по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Данные пользователя</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _userService.GetById(id));
        }

        /// <summary>
        /// Создание нового студента
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///        "firstName" : "Иван",
        ///        "lastname" : "Иванов",
        ///        "groupId" : "1",
        ///     }
        ///
        /// </remarks>
        /// <param name="request">Данные студента</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateUserRequest request)
        {
            var userDto = request.Adapt<Student>();
            await _userService.Create(userDto);
            return Ok();
        }

        /// <summary>
        /// Обновление данных студента
        /// </summary>
        /// <param name="user">Обновленные данные студента</param>
        /// <returns>Результат операции</returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateUserRequest request)
        {
            var studentDto = request.Adapt<Student>();
            await _userService.Update(studentDto);
            return Ok();
        }

        /// <summary>
        /// Удаление студента по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор студента</param>
        /// <returns>Результат операции</returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await _userService.Delete(id);
            return Ok();
        }
    }
}