using BusinessLogic.Interfaces;
using DataAccess.Models;
using Microsoft.AspNetCore.Mvc;

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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _submissionService.GetAll());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _submissionService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Add(Submission submission)
        {
            await _submissionService.Create(submission);
            return Ok();
        }

        [HttpPut]
        public async Task<IActionResult> Update(Submission submission)
        {
            await _submissionService.Update(submission);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _submissionService.Delete(id);
            return Ok();
        }
    }
}