using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using SharedLibrary.Data;
using SharedLibrary.Models.User;

namespace WebApiUpg.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly SqlDbContext _context;
        private readonly IConfiguration _configuration;

        public UsersController(SqlDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            try
            {
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };
                user.CreatePasswordWithHash(model.Password);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return new OkResult();
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == model.Email);

                if (user != null)
                {
                    if (user.ValidatePasswordHash(model.Password))
                    {
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var expiresDate = DateTime.Now.AddDays(1);

                        var tokenDesc = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                new Claim("userId", user.Id.ToString()),
                                new Claim("Expires", expiresDate.ToString())
                            }),
                            Expires = expiresDate,
                            SigningCredentials = new SigningCredentials(
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value)),
                                    SecurityAlgorithms.HmacSha512Signature)

                        };

                        var _accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDesc));

                        return new OkObjectResult(_accessToken);

                    }
                }
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
            return new BadRequestResult();
        }

        [Authorize]
        [HttpGet("all")]
        public IActionResult AllUsers()
        {
            try
            {

                var users = _context.Users.ToList();
                return new OkObjectResult(users);
            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }



    }
}
