using System.Collections.Generic;
using System.Threading.Tasks;
using JobPosting_project.Models;
using Microsoft.EntityFrameworkCore;
using JobPosting_project.Services.I;

namespace JobPosting_project.Services
{
    public class SkillService : ISkillService
    {
        private readonly ApplicationDbContext _context;

        public SkillService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Skill>> GetAllAsync()
        {
            return await _context.Skills.ToListAsync();
        }

        public async Task<Skill> GetByIdAsync(int id)
        {
            return await _context.Skills.FindAsync(id);
        }

        public async Task CreateAsync(Skill skill)
        {
            _context.Skills.Add(skill);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Skill skill)
        {
            _context.Skills.Update(skill);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Skill skill)
        {
            _context.Skills.Remove(skill);
            await _context.SaveChangesAsync();
        }
    }
}