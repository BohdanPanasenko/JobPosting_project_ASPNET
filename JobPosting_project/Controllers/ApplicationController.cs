using Microsoft.AspNetCore.Mvc;
using JobPosting_project.DTOs;
using JobPosting_project.Models;
using JobPosting_project.Services;
using System.Linq;
using System.Threading.Tasks;
using JobPosting_project.Mappers;
using JobPosting_project.Services.I;
using Microsoft.AspNetCore.Authorization;

namespace JobPosting_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IApplicationService _applicationService;

        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var applications = await _applicationService.GetAllAsync();
            return Ok(applications.Select(a => a.ToApplicationReadDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var application = await _applicationService.GetByIdAsync(id);
            if (application == null) return NotFound();
            return Ok(application.ToApplicationReadDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ApplicationCreateDto dto) //
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var application = dto.ToApplication();
            await _applicationService.CreateAsync(application);
            return CreatedAtAction(nameof(GetById), new { id = application.Id }, application.ToApplicationReadDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ApplicationUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var application = await _applicationService.GetByIdAsync(id);
            if (application == null) return NotFound();

            application.Status = dto.Status;
            await _applicationService.UpdateAsync(application);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var application = await _applicationService.GetByIdAsync(id);
            if (application == null) return NotFound();

            await _applicationService.DeleteAsync(application);
            return NoContent();
        }
    }
}