using JobPosting_project.Models;
using System.ComponentModel.DataAnnotations;

namespace JobPosting_project.DTOs
{
    public class ApplicationReadDto
    {
        public int Id { get; set; }
        public DateTime AppliedDate { get; set; }
        public ApplicationStatus Status { get; set; }
        public UserReadDto Applicant { get; set; }
        public JobPostingReadDto JobPosting { get; set; }
    }

    public class ApplicationCreateDto
    {
        [Required(ErrorMessage = "Job posting is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid job posting")]
        public int JobPostingId { get; set; }

        [Required(ErrorMessage = "Applicant is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid applicant")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Application status is required")]
        public ApplicationStatus Status { get; set; } = ApplicationStatus.Pending;
    }

    public class ApplicationUpdateDto
    {
        [Required(ErrorMessage = "Application status is required")]
        public ApplicationStatus Status { get; set; }
    }
}
