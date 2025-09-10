using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _gradeService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _gradeService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Grade grade)
        {
            await _gradeService.Create(grade);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Grade grade)
        {
            await _gradeService.Update(grade);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _gradeService.Delete(id);
            return Ok();
        }
    }
}