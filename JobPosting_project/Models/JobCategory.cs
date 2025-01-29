namespace JobPosting_project.Models
{
    public class JobCategory
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }

        public ICollection<JobPosting> JobPostings { get; set; }
    }
}
