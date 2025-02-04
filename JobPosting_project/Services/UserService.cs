using System.Collections.Generic;
using System.Threading.Tasks;
using JobPosting_project.Models;
using Microsoft.EntityFrameworkCore;
using JobPosting_project.Services.I;

namespace JobPosting_project.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.UserSkills)
                .ThenInclude(us => us.Skill)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.UserSkills)
                .ThenInclude(us => us.Skill)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task CreateAsync(User user)
        {
            // Ensure that the Skill entities are loaded and tracked
            foreach (var userSkill in user.UserSkills)
            {
                var skill = await _context.Skills.FindAsync(userSkill.SkillId);
                if (skill == null)
                {
                    throw new KeyNotFoundException($"Skill with Id {userSkill.SkillId} not found");
                }
                userSkill.Skill = skill;
            }

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}