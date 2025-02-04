namespace JobPosting_project.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public UserRole Role { get; set; } 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Application> Applications { get; set; }
        public ICollection<UserSkill> UserSkills { get; set; }
        public List<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }

    public enum UserRole
    {
        User,
        Admin
    }
}
