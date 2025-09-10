using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _projectService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _projectService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Project project)
        {
            await _projectService.Create(project);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Project project)
        {
            await _projectService.Update(project);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectService.Delete(id);
            return Ok();
        }
    }
}