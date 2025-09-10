using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _groupService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _groupService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Group group)
        {
            await _groupService.Create(group);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Group group)
        {
            await _groupService.Update(group);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _groupService.Delete(id);
            return Ok();
        }
    }
}