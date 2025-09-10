using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectTeamController : ControllerBase
    {
        private readonly IProjectTeamService _projectTeamService;

        public ProjectTeamController(IProjectTeamService projectTeamService)
        {
            _projectTeamService = projectTeamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _projectTeamService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _projectTeamService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Projectteam projectTeam)
        {
            await _projectTeamService.Create(projectTeam);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Projectteam projectTeam)
        {
            await _projectTeamService.Update(projectTeam);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _projectTeamService.Delete(id);
            return Ok();
        }
    }
}