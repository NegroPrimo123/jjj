using Domain.Interfaces;
using Domain.Models;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using task3.Contracts.Group;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IGroupService _groupService;

        public GroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }

        /// <summary>
        /// Получение списка всех групп
        /// </summary>
        /// <returns>Список всех групп</returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _groupService.GetAll());
        }

        /// <summary>
        /// Получение группы по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор группы</param>
        /// <returns>Данные группы</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _groupService.GetById(id));
        }

        /// <summary>
        /// Создание новой группы
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///          "groupName": "24ИС-21-2"
        ///     }
        ///
        /// </remarks>
        /// <param name="group">Данные группы</param>
        /// <returns>Результат операции</returns>
        [HttpPost]
        public async Task<IActionResult> Add(CreateGroupRequest request)
        {
            var groupDto = request.Adapt<Group>();
            await _groupService.Create(groupDto);
            return Ok();
        }

        /// <summary>
        /// Обновление данных группы
        /// </summary>
        /// <remarks>
        /// Пример запроса:
        ///
        ///     POST /Todo
        ///     {
        ///          "groupName": "24ИС-21-2"
        ///     }
        ///
        /// </remarks>
        /// <param name="group">Обновленные данные группы</param>
        /// <returns>Результат операции</returns>
        [HttpPut]
        public async Task<IActionResult> Update(CreateGroupRequest request)
        {
            var groupDto = request.Adapt<Group>();
            await _groupService.Update(groupDto);
            return Ok();
        }

        /// <summary>
        /// Удаление группы по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор группы</param>
        /// <returns>Результат операции</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _groupService.Delete(id);
            return Ok();
        }
    }
}