using System.Security.Claims;
using learnhub_be.Common.Implementations;
using learnhub_be.Common.Models.RequestModels;
using learnhub_be.DataAccess.Interfaces;
using learnhub_be.DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace learnhub_be.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IDapi _dapi;

        public AuthenticationController(IDapi dapi)
        {
            this._dapi = dapi;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel request)
        {
            if (string.IsNullOrEmpty(request.Password) || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Email))
            {
                return BadRequest(new { error = "Empty request" });
            }

            var found = await _dapi.Users.FindAsync(u => u.Email == request.Email && u.Username == request.Username);
            

            if (found.Any())
            {
                return BadRequest(new { error = "Email already exists" });
            }

            request.Password = PasswordHelper.EncryptPassword(request.Password);

            var user = new User
            {
                Email = request.Email,
                Username = request.Username,
                Password = request.Password
            };

            await _dapi.BeginTransactionAsync();
            try
            {
                await _dapi.Users.AddAsync(user);
                await _dapi.CompleteAsync();
                await _dapi.CommitTransactionAsync();
            }
            catch(Exception ex)
            {
                await _dapi.RollbackTransactionAsync();
                throw;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"User"),
                new Claim(ClaimTypes.Actor,request.Username)

            };

            var token = TokenHelper.GenerateJwtToken(claims);

            return Ok(new { Token = token });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModels request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                return BadRequest(new { error = "Empty request" });
            }


            if (request.Email == Environment.GetEnvironmentVariable("ADMIN_EMAIL"))
            {
                if (request.Password != Environment.GetEnvironmentVariable("ADMIN_PASS"))
                {
                    return BadRequest(new { error = "Password for admin is not correct" });
                }

                var adminClaims = new[]
                {
                    new Claim(ClaimTypes.Role, "Admin"),
                    new Claim(ClaimTypes.Actor,"Admin")
                };

                var adminToken = TokenHelper.GenerateJwtToken(adminClaims);
                return Ok(new { Token = adminToken });
            }



            var user = await _dapi.Users.FindAsync(u=>u.Email == request.Email);

            if (user.Any()==false)
            {
                return BadRequest(new { error = "The user was not found" });
            }

            if (PasswordHelper.VerifyPassword(request.Password, user.First().Password) == false)
            {
                return BadRequest(new { error = "Password is wrong" });
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Role,"User"),
                new Claim(ClaimTypes.Actor,user.First().Username)
            };

            var token = TokenHelper.GenerateJwtToken(claims);
            return Ok(new { Token = token });
        }
    }
}
