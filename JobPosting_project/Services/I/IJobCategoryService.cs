using JobPosting_project.Models;

namespace JobPosting_project.Services.I
{
    public interface IJobCategoryService
    {
        Task<IEnumerable<JobCategory>> GetAllAsync();
        Task<JobCategory> GetByIdAsync(int id);
        Task CreateAsync(JobCategory jobCategory);
        Task UpdateAsync(JobCategory jobCategory);
        Task DeleteAsync(JobCategory jobCategory);
    }
}
