using CardInsight.API.Data;
using CardInsight.API.DTOs;
using CardInsight.API.Helpers;
using CardInsight.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CardInsight.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IJwtHelper jwt;
        private readonly IPasswordHasher<User> hasher;

        public AuthController(ApplicationDbContext dbContext, IJwtHelper jwt, IPasswordHasher<User> hasher)
        {
            this.dbContext = dbContext;
            this.jwt = jwt;
            this.hasher = hasher;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthDTO dTO)
        {
            if (await dbContext.Users.AnyAsync(u => u.UserName == dTO.UserName))
                return Conflict("User already axist");
            var user = new User
            {
                UserName = dTO.UserName,
                Role = "User"
            };
            user.PasswordHash = hasher.HashPassword(user,dTO.Password);
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();
            return CreatedAtAction(nameof(Register), new { id = user.Id }, new { user.Id, user.UserName, user.Role });
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthDTO dTO)
        {
            var find = await dbContext.Users.FirstOrDefaultAsync(c => c.UserName == dTO.UserName);
            if (find is null)
                return NotFound("No user found.");
            var result = hasher.VerifyHashedPassword(find,find.PasswordHash, dTO.Password);
            if(result == PasswordVerificationResult.Failed)
                return Unauthorized("Invalid Credential");
            var token = jwt.GenerateToken(find);
            return Ok(token);

        }
    }
}
