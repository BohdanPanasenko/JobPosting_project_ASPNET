using System.Collections.Generic;
using System.Threading.Tasks;
using JobPosting_project.Models;
using Microsoft.EntityFrameworkCore;
using JobPosting_project.Services.I;

namespace JobPosting_project.Services
{
    public class JobCategoryService : IJobCategoryService
    {
        private readonly ApplicationDbContext _context;

        public JobCategoryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<JobCategory>> GetAllAsync()
        {
            return await _context.JobCategories.ToListAsync();
        }

        public async Task<JobCategory> GetByIdAsync(int id)
        {
            return await _context.JobCategories.FindAsync(id);
        }

        public async Task CreateAsync(JobCategory jobCategory)
        {
            _context.JobCategories.Add(jobCategory);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(JobCategory jobCategory)
        {
            _context.JobCategories.Update(jobCategory);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(JobCategory jobCategory)
        {
            _context.JobCategories.Remove(jobCategory);
            await _context.SaveChangesAsync();
        }
    }
}