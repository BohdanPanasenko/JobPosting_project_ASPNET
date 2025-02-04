using Xunit;
using Moq;
using JobPosting_project.Services;
using JobPosting_project.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class ApplicationServiceTests
{
    private readonly ApplicationService _applicationService;
    private readonly ApplicationDbContext _context;

    public ApplicationServiceTests()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;
        _context = new ApplicationDbContext(options);

        _applicationService = new ApplicationService(_context);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreateNewApplication()
    {
        var user = new User { Id = 1, Name = "testuser" };
        var jobPosting = new JobPosting { Id = 1, Title = "Software Developer" };
        _context.Users.Add(user);
        _context.JobPostings.Add(jobPosting);
        await _context.SaveChangesAsync();

        var application = new Application
        {
            UserId = 1,
            JobPostingId = 1,
            Status = ApplicationStatus.Pending
        };

        await _applicationService.CreateAsync(application);
        var createdApplication = await _context.Applications.FirstOrDefaultAsync(a => a.UserId == 1 && a.JobPostingId == 1);

        Assert.NotNull(createdApplication);
        Assert.Equal(ApplicationStatus.Pending, createdApplication.Status);
    }
}