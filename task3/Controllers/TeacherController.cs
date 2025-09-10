using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _teacherService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _teacherService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Teacher teacher)
        {
            await _teacherService.Create(teacher);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Teacher teacher)
        {
            await _teacherService.Update(teacher);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _teacherService.Delete(id);
            return Ok();
        }
    }
}