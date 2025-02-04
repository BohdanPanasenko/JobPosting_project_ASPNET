using Xunit;
using Moq;
using JobPosting_project.Services;
using JobPosting_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

public class JobPostingServiceTests
{
    private readonly JobPostingService _jobPostingService;
    private readonly ApplicationDbContext _context;

    public JobPostingServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(options);

        _jobPostingService = new JobPostingService(_context);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllJobPostings()
    {
        var jobPostings = new List<JobPosting>
        {
            new JobPosting { Id = 1, Title = "Software Developer" },
            new JobPosting { Id = 2, Title = "Data Scientist" }
        };
        _context.JobPostings.AddRange(jobPostings);
        await _context.SaveChangesAsync();
        var result = await _jobPostingService.GetAllAsync();
        Assert.Equal(2, result.Count());
        Assert.Contains(result, jp => jp.Title == "Software Developer");
        Assert.Contains(result, jp => jp.Title == "Data Scientist");
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnJobPosting()
    {
        var jobPosting = new JobPosting
        {
            Id = 1,
            Title = "Software Developer",
            Description = "Develop software applications",
            Location = "Remote"
        };
        _context.JobPostings.Add(jobPosting);
        await _context.SaveChangesAsync();

        var result = await _jobPostingService.GetByIdAsync(1);

        Assert.NotNull(result);
        Assert.Equal("Software Developer", result.Title);
    }
}