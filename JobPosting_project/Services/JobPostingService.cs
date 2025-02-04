using System.Collections.Generic;
using System.Threading.Tasks;
using JobPosting_project.Models;
using Microsoft.EntityFrameworkCore;
using JobPosting_project.Services.I;

namespace JobPosting_project.Services
{
    public class JobPostingService : IJobPostingService
    {
        private readonly ApplicationDbContext _context;

        public JobPostingService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobPosting>> GetAllAsync()
        {
            return await _context.JobPostings
                .Include(jp => jp.Category)
                .Include(jp => jp.RequiredSkills)
                .ThenInclude(js => js.Skill)
                .ToListAsync();
        }

        public async Task<JobPosting> GetByIdAsync(int id)
        {
            return await _context.JobPostings
                .Include(jp => jp.Category)
                .Include(jp => jp.RequiredSkills)
                .ThenInclude(js => js.Skill)
                .FirstOrDefaultAsync(jp => jp.Id == id);
        }

        public async Task CreateAsync(JobPosting jobPosting)
        {
            foreach (var jobSkill in jobPosting.RequiredSkills)
            {
                var skill = await _context.Skills.FindAsync(jobSkill.SkillId);
                if (skill == null)
                {
                    throw new KeyNotFoundException($"Skill with Id {jobSkill.SkillId} not found");
                }
                jobSkill.Skill = skill;
            }

            _context.JobPostings.Add(jobPosting);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobPosting jobPosting)
        {
            _context.JobPostings.Update(jobPosting);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(JobPosting jobPosting)
        {
            _context.JobPostings.Remove(jobPosting);
            await _context.SaveChangesAsync();
        }
    }
}