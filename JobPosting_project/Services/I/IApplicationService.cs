
using JobPosting_project.Models;
namespace JobPosting_project.Services.I
{
    public interface IApplicationService
    {
        Task<IEnumerable<Application>> GetAllAsync();
        Task<Application> GetByIdAsync(int id);
        Task CreateAsync(Application application);
        Task UpdateAsync(Application application);
        Task DeleteAsync(Application application);
    }
}
