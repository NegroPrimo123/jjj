using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _courseService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _courseService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Course course)
        {
            await _courseService.Create(course);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Course course)
        {
            await _courseService.Update(course);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.Delete(id);
            return Ok();
        }
    }
}