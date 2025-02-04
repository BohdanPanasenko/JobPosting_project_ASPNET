using JobPosting_project.DTOs;
using JobPosting_project.Models;
using JobPosting_project.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace JobPosting_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly TokenService _tokenService;

        public AuthController(ApplicationDbContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginDTO loginDTO)
        {
            User? user = await _context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Email == loginDTO.Email && u.Password == loginDTO.Password);

            if (user == null)
            {
                return Unauthorized("Invalid user credentials.");
            }

            string token = _tokenService.IssueToken(user);
            RefreshToken refreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshTokens.Add(refreshToken);
            await _context.SaveChangesAsync();

            return Ok(new TokenResponse()
            {
                AccessToken = token,
                RefreshToken = refreshToken.Value
            });
        }

        [HttpPost("refreshToken")]
        public async Task<ActionResult<TokenResponse>> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            User? user = await _context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u =>
                    u.RefreshTokens.Any(rt =>
                        rt.Value == refreshTokenRequest.RefreshToken
                        && rt.ExpiresAt > DateTime.Now));

            if (user == null)
            {
                return Unauthorized("Invalid refresh token.");
            }

            user.RefreshTokens.RemoveAll(rt => rt.Value == refreshTokenRequest.RefreshToken);

            string token = _tokenService.IssueToken(user);
            RefreshToken newRefreshToken = _tokenService.GenerateRefreshToken();

            user.RefreshTokens.Add(newRefreshToken);
            await _context.SaveChangesAsync();

            return Ok(new TokenResponse()
            {
                AccessToken = token,
                RefreshToken = newRefreshToken.Value
            });
        }

        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            string? UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (UserId == null)
            {
                return Unauthorized("User Id not found in JWT token.");
            }

            int authenticatedUserId = int.Parse(UserId);

            User? user = await _context.Users
                .Include(u => u.RefreshTokens)
                .FirstOrDefaultAsync(u => u.Id == authenticatedUserId);

            if (user == null)
            {
                return Unauthorized("User not found.");
            }

            user.RefreshTokens.Clear();
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}