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
    public class JobCategoryController : ControllerBase
    {
        private readonly IJobCategoryService _jobCategoryService;

        public JobCategoryController(IJobCategoryService jobCategoryService)
        {
            _jobCategoryService = jobCategoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var jobCategories = await _jobCategoryService.GetAllAsync();
            return Ok(jobCategories.Select(jc => jc.ToJobCategoryDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var jobCategory = await _jobCategoryService.GetByIdAsync(id);
            if (jobCategory == null) return NotFound();
            return Ok(jobCategory.ToJobCategoryDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] JobCategoryCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var jobCategory = dto.ToJobCategory();
            await _jobCategoryService.CreateAsync(jobCategory);
            return CreatedAtAction(nameof(GetById), new { id = jobCategory.Id }, jobCategory.ToJobCategoryDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] JobCategoryCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var jobCategory = await _jobCategoryService.GetByIdAsync(id);
            if (jobCategory == null) return NotFound();

            jobCategory.CategoryName = dto.CategoryName;
            await _jobCategoryService.UpdateAsync(jobCategory);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var jobCategory = await _jobCategoryService.GetByIdAsync(id);
            if (jobCategory == null) return NotFound();

            await _jobCategoryService.DeleteAsync(jobCategory);
            return NoContent();
        }
    }
}