using System.ComponentModel.DataAnnotations;

namespace JobPosting_project.DTOs
{
    public class SkillDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class SkillCreateDto
    {
        [Required(ErrorMessage = "Skill name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Skill name must be between 2 and 50 characters")]
        public string Name { get; set; }
    }

}
