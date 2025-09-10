using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

namespace BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentTeamController : ControllerBase
    {
        private readonly IStudentTeamService _studentTeamService;

        public StudentTeamController(IStudentTeamService studentTeamService)
        {
            _studentTeamService = studentTeamService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _studentTeamService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _studentTeamService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Studentteam studentTeam)
        {
            await _studentTeamService.Create(studentTeam);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Studentteam studentTeam)
        {
            await _studentTeamService.Update(studentTeam);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _studentTeamService.Delete(id);
            return Ok();
        }
    }
}