using System.ComponentModel.DataAnnotations;

namespace JobPosting_project.DTOs
{
    public class RefreshTokenRequest
    {
        [Required]
        public required string RefreshToken { get; set; }
    }
}
