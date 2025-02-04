using JobPosting_project.Models;

namespace JobPosting_project.Services.I
{
    public interface ISkillService
    {
        Task<IEnumerable<Skill>> GetAllAsync();
        Task<Skill> GetByIdAsync(int id);
        Task CreateAsync(Skill skill);
        Task UpdateAsync(Skill skill);
        Task DeleteAsync(Skill skill);
    }
}
