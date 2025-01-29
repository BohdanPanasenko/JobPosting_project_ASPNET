using System.ComponentModel.DataAnnotations;

namespace JobPosting_project.DTOs
{
        public class JobPostingReadDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Location { get; set; }
            public DateTime PostedDate { get; set; }
            public string CategoryName { get; set; }
            public ICollection<SkillDto> RequiredSkills { get; set; }
        }

    public class JobPostingCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(5000, MinimumLength = 50, ErrorMessage = "Description must be between 50 and 5000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "At least one skill is required")]
        [MinLength(1, ErrorMessage = "At least one skill must be selected")]
        public ICollection<int> SkillIds { get; set; }
    }

    public class JobPostingUpdateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(5000, MinimumLength = 50, ErrorMessage = "Description must be between 50 and 5000 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Category is required")]
        [Range(1, int.MaxValue, ErrorMessage = "Please select a valid category")]
        public int CategoryId { get; set; }
    }
}
