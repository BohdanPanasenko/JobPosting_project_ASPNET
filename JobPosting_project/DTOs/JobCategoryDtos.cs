using System.ComponentModel.DataAnnotations;

namespace JobPosting_project.DTOs
{
    public class JobCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
    }

    public class JobCategoryCreateDto
    {
        [Required(ErrorMessage = "Category name is required")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Category name must be between 2 and 100 characters")]
        public string CategoryName { get; set; }
    }
}
