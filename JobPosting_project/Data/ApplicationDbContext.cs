using JobPosting_project.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext
{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        { }

        public DbSet<User> Users { get; set; }
        public DbSet<JobPosting> JobPostings { get; set; }
        public DbSet<Application> Applications { get; set; }
        public DbSet<Skill> Skills { get; set; }
        public DbSet<JobCategory> JobCategories { get; set; }
        public DbSet<UserSkill> UserSkills { get; set; }
        public DbSet<JobSkill> JobSkills { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserSkill>()
                .HasKey(us => new { us.UserId, us.SkillId });
            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.User)
                .WithMany(u => u.UserSkills)
                .HasForeignKey(us => us.UserId);
            modelBuilder.Entity<UserSkill>()
                .HasOne(us => us.Skill)
                .WithMany(s => s.UserSkills)
                .HasForeignKey(us => us.SkillId);

            modelBuilder.Entity<JobSkill>()
                .HasKey(js => new { js.JobPostingId, js.SkillId });
            modelBuilder.Entity<JobSkill>()
                .HasOne(js => js.JobPosting)
                .WithMany(j => j.RequiredSkills)
                .HasForeignKey(js => js.JobPostingId);
            modelBuilder.Entity<JobSkill>()
                .HasOne(js => js.Skill)
                .WithMany(s => s.JobSkills)
                .HasForeignKey(js => js.SkillId);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Applicant)
                .WithMany(u => u.Applications)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<Application>()
                .HasOne(a => a.JobPosting)
                .WithMany(j => j.Applications)
                .HasForeignKey(a => a.JobPostingId)
                .OnDelete(DeleteBehavior.Restrict); 

            modelBuilder.Entity<JobPosting>()
                .HasOne(j => j.Category)
                .WithMany(c => c.JobPostings)
                .HasForeignKey(j => j.CategoryId);

        }
}

