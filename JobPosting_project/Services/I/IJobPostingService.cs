using JobPosting_project.Models;

namespace JobPosting_project.Services.I
{
    public interface IJobPostingService
    {
        Task<IEnumerable<JobPosting>> GetAllAsync();
        Task<JobPosting> GetByIdAsync(int id);
        Task CreateAsync(JobPosting jobPosting);
        Task UpdateAsync(JobPosting jobPosting);
        Task DeleteAsync(JobPosting jobPosting);
    }
}
