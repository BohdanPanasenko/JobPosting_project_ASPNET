namespace JobPosting_project.Models
{
    public class Application
    {
        public int Id { get; set; }
        public DateTime AppliedDate { get; set; } = DateTime.UtcNow;
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
        public int UserId { get; set; }
        public int JobPostingId { get; set; }

        public User Applicant { get; set; }
        public JobPosting JobPosting { get; set; }
    }

    public enum ApplicationStatus
    {
        Pending,
        Approved,
        Rejected
    }
}
