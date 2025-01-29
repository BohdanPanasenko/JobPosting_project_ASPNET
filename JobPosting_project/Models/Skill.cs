﻿namespace JobPosting_project.Models
{
    public class Skill
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<UserSkill> UserSkills { get; set; }
        public ICollection<JobSkill> JobSkills { get; set; }
    }
}
