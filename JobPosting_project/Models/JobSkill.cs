namespace JobPosting_project.Models
{
    public class JobSkill
    {
        public int JobPostingId { get; set; }
        public int SkillId { get; set; }

        public JobPosting JobPosting { get; set; }
        public Skill Skill { get; set; }
    }
}
