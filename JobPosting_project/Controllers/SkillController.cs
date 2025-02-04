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
    public class SkillController : ControllerBase
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var skills = await _skillService.GetAllAsync();
            return Ok(skills.Select(s => s.ToSkillDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var skill = await _skillService.GetByIdAsync(id);
            if (skill == null) return NotFound();
            return Ok(skill.ToSkillDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SkillCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var skill = dto.ToSkill();
            await _skillService.CreateAsync(skill);
            return CreatedAtAction(nameof(GetById), new { id = skill.Id }, skill.ToSkillDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SkillCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var skill = await _skillService.GetByIdAsync(id);
            if (skill == null) return NotFound();

            skill.Name = dto.Name;
            await _skillService.UpdateAsync(skill);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var skill = await _skillService.GetByIdAsync(id);
            if (skill == null) return NotFound();

            await _skillService.DeleteAsync(skill);
            return NoContent();
        }
    }
}