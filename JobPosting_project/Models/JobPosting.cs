namespace JobPosting_project.Models
{
    public class JobPosting
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime PostedDate { get; set; } = DateTime.UtcNow;
        public int CategoryId { get; set; }

        public JobCategory Category { get; set; }
        public ICollection<Application> Applications { get; set; }
        public ICollection<JobSkill> RequiredSkills { get; set; }
    }
}
