using System.Collections.Generic;
using System.Threading.Tasks;
using JobPosting_project.Models;
using Microsoft.EntityFrameworkCore;
using JobPosting_project.Services.I;

namespace JobPosting_project.Services
{
    public class ApplicationService : IApplicationService
    {
        private readonly ApplicationDbContext _context;

        public ApplicationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Application>> GetAllAsync()
        {
            return await _context.Applications
                .Include(a => a.Applicant)
                    .ThenInclude(u => u.UserSkills)
                        .ThenInclude(us => us.Skill)
                .Include(a => a.JobPosting)
                    .ThenInclude(jp => jp.RequiredSkills)
                        .ThenInclude(js => js.Skill)
                .Include(a => a.JobPosting)
                    .ThenInclude(jp => jp.Category)
                .ToListAsync();
        }

        public async Task<Application> GetByIdAsync(int id)
        {
            return await _context.Applications
                .Include(a => a.Applicant)
                    .ThenInclude(u => u.UserSkills)
                        .ThenInclude(us => us.Skill)
                .Include(a => a.JobPosting)
                    .ThenInclude(jp => jp.RequiredSkills)
                        .ThenInclude(js => js.Skill)
                .Include(a => a.JobPosting)
                    .ThenInclude(jp => jp.Category)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task CreateAsync(Application application)
        {
            _context.Applications.Add(application);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Application application)
        {
            _context.Applications.Update(application);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Application application)
        {
            _context.Applications.Remove(application);
            await _context.SaveChangesAsync();
        }
    }
}