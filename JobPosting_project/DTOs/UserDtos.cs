using JobPosting_project.Models;
using System.ComponentModel.DataAnnotations;

namespace JobPosting_project.DTOs
{
        public class UserReadDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public UserRole Role { get; set; }
            public DateTime CreatedAt { get; set; }
            public ICollection<SkillDto> Skills { get; set; }
        }

        public class UserCreateDto
        {
            [Required(ErrorMessage = "Name is required")]
            [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid email address")]
            [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Password is required")]
            [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters")]
            [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,}$",
            ErrorMessage = "Password must contain at least one uppercase letter, one lowercase letter, and one number")]
            public string Password { get; set; }

            [Required(ErrorMessage = "User role is required")]
            public UserRole Role { get; set; }
            [Required]
            public ICollection<UserSkillCreateDto> Skills { get; set; }
        }

        public class UserUpdateDto
        {
            [Required(ErrorMessage = "Name is required")]
            [StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
            public string Name { get; set; }

            [Required(ErrorMessage = "Email is required")]
            [EmailAddress(ErrorMessage = "Invalid email address")]
            [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
            public string Email { get; set; }

            [Required]
            public ICollection<UserSkillCreateDto> Skills { get; set; }
        }

        public class UserSkillReadDto
        {
            public int SkillId { get; set; }
            public string SkillName { get; set; }
        }

        public class UserSkillCreateDto
        {
            [Required]
            public int SkillId { get; set; }
        }
}
