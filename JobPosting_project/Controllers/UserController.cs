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
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users.Select(u => u.ToUserReadDto()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();
            return Ok(user.ToUserReadDto());
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = dto.ToUser();
            try
            {
                await _userService.CreateAsync(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user.ToUserReadDto());
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto dto)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            user.Name = dto.Name;
            user.Email = dto.Email;
            user.UserSkills = dto.Skills.Select(us => new UserSkill
            {
                SkillId = us.SkillId
            }).ToList();

            await _userService.UpdateAsync(user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null) return NotFound();

            await _userService.DeleteAsync(user);
            return NoContent();
        }
    }
}