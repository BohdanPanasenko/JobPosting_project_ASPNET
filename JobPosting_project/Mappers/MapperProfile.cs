using JobPosting_project.DTOs;
using JobPosting_project.Models;

namespace JobPosting_project.Mappers
{
    public static class ApplicationMapper
    {
        public static ApplicationReadDto ToApplicationReadDto(this Application application)
        {
            return new ApplicationReadDto()
            {
                Id = application.Id,
                AppliedDate = application.AppliedDate,
                Status = application.Status,
                Applicant = application.Applicant?.ToUserReadDto(),
                JobPosting = application.JobPosting?.ToJobPostingReadDto()
            };
        }
        public static Application ToApplication(this ApplicationCreateDto dto)
        {
            return new Application
            {
                JobPostingId = dto.JobPostingId,
                UserId = dto.UserId,
                Status = dto.Status,
                AppliedDate = DateTime.UtcNow 
            };
        }
        public static Application ToApplication(this ApplicationUpdateDto dto)
        {
            return new Application()
            {
                Status = dto.Status
            };
        }
    }

    public static class JobPostingMapper
    {
        public static JobPostingReadDto ToJobPostingReadDto(this JobPosting jobPosting)
        {
            return new JobPostingReadDto()
            {
                Id = jobPosting.Id,
                Title = jobPosting.Title,
                Description = jobPosting.Description,
                Location = jobPosting.Location,
                PostedDate = jobPosting.PostedDate,
                CategoryName = jobPosting.Category?.CategoryName,
                RequiredSkills = jobPosting.RequiredSkills?.Select(js => js.Skill.ToSkillDto()).ToList()
            };
        }
        public static JobPosting ToJobPosting(this JobPostingCreateDto dto)
        {
            return new JobPosting()
            {
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                CategoryId = dto.CategoryId,
                RequiredSkills = dto.SkillIds.Select(skillId => new JobSkill { SkillId = skillId }).ToList()
            };
        }
        public static JobPosting ToJobPosting(this JobPostingUpdateDto dto)
        {
            return new JobPosting()
            {
                Title = dto.Title,
                Description = dto.Description,
                Location = dto.Location,
                CategoryId = dto.CategoryId
            };
        }
    }

    public static class SkillMapper
    {
        public static SkillDto ToSkillDto(this Skill skill)
        {
            if (skill == null)
            {
                throw new ArgumentNullException(nameof(skill), "Skill cannot be null");
            }

            return new SkillDto()
            {
                Id = skill.Id,
                Name = skill.Name
            };
        }
        public static Skill ToSkill(this SkillCreateDto dto)
        {
            return new Skill()
            {
                Name = dto.Name
            };
        }
    }

    public static class UserMapper
    {
        public static UserReadDto ToUserReadDto(this User user)
        {
            return new UserReadDto()
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Role = user.Role,
                CreatedAt = user.CreatedAt,
                Skills = user.UserSkills?.Select(us => us.Skill.ToSkillDto()).ToList()
            };
        }
        public static User ToUser(this UserCreateDto dto)
        {
            return new User()
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password,
                Role = dto.Role,
                UserSkills = dto.Skills.Select(us => new UserSkill
                {
                    SkillId = us.SkillId
                }).ToList()
            };
        }
        public static User ToUser(this UserUpdateDto dto)
        {
            return new User()
            {
                Name = dto.Name,
                Email = dto.Email,
                UserSkills = dto.Skills.Select(us => new UserSkill
                {
                    SkillId = us.SkillId
                }).ToList()
            };
        }
    }

    public static class JobCategoryMapper
    {
        public static JobCategoryDto ToJobCategoryDto(this JobCategory jobCategory)
        {
            return new JobCategoryDto()
            {
                Id = jobCategory.Id,
                CategoryName = jobCategory.CategoryName
            };
        }
        public static JobCategory ToJobCategory(this JobCategoryCreateDto dto)
        {
            return new JobCategory()
            {
                CategoryName = dto.CategoryName
            };
        }
    }
}