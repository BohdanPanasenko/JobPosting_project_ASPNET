using Microsoft.AspNetCore.Mvc;
using JobPosting_project.DTOs;
using JobPosting_project.Models;
using JobPosting_project.Services;
using System.Linq;
using System.Threading.Tasks;
using JobPosting_project.Mappers;
using JobPosting_project.Services.I;

namespace JobPosting_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobPostingController : ControllerBase
    {
        private readonly IJobPostingService _jobPostingService;

        public JobPostingController(IJobPostingService jobPostingService)
        {
            _jobPostingService = jobPostingService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var jobPostings = await _jobPostingService.GetAllAsync();
            var pagedJobPostings = jobPostings.Skip((page - 1) * pageSize).Take(pageSize);
            return Ok(pagedJobPostings.Select(jp => jp.ToJobPostingReadDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var jobPosting = await _jobPostingService.GetByIdAsync(id);
            if (jobPosting == null) return NotFound();
            return Ok(jobPosting.ToJobPostingReadDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JobPostingCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var jobPosting = dto.ToJobPosting();
            await _jobPostingService.CreateAsync(jobPosting);
            return CreatedAtAction(nameof(GetById), new { id = jobPosting.Id }, jobPosting.ToJobPostingReadDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JobPostingUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var jobPosting = await _jobPostingService.GetByIdAsync(id);
            if (jobPosting == null) return NotFound();

            jobPosting.Title = dto.Title;
            jobPosting.Description = dto.Description;
            jobPosting.Location = dto.Location;
            jobPosting.CategoryId = dto.CategoryId;
            await _jobPostingService.UpdateAsync(jobPosting);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var jobPosting = await _jobPostingService.GetByIdAsync(id);
            if (jobPosting == null) return NotFound();

            await _jobPostingService.DeleteAsync(jobPosting);
            return NoContent();
        }
    }
}