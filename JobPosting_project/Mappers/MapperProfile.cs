using AutoMapper;
using JobPosting_project.DTOs;
using JobPosting_project.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // User 
        CreateMap<User, UserReadDto>()
            .ForMember(dest => dest.Skills,
                opt => opt.MapFrom(src => src.UserSkills.Select(us => us.Skill)));
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();

        // JobPosting 
        CreateMap<JobPosting, JobPostingReadDto>()
            .ForMember(dest => dest.CategoryName,
                opt => opt.MapFrom(src => src.Category.CategoryName))
            .ForMember(dest => dest.RequiredSkills,
                opt => opt.MapFrom(src => src.RequiredSkills.Select(js => js.Skill)));
        CreateMap<JobPostingCreateDto, JobPosting>();
        CreateMap<JobPostingUpdateDto, JobPosting>();

        // Application
        CreateMap<Application, ApplicationReadDto>();
        CreateMap<ApplicationCreateDto, Application>();
        CreateMap<ApplicationUpdateDto, Application>();

        // Skill 
        CreateMap<Skill, SkillDto>();
        CreateMap<SkillCreateDto, Skill>();

        // JobCategory 
        CreateMap<JobCategory, JobCategoryDto>();
        CreateMap<JobCategoryCreateDto, JobCategory>();
    }
}