using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebAPIProject.Data;
using WebAPIProject.Infrastructre;
using WebAPIProject.Models;

namespace WebAPIProject.Controllers
{
    /// <summary>
    /// This is Authentication section
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        //public static User user = new User();
        
        public readonly WebAPIProjectContext _context;
        public readonly IConfiguration _configuration;

        public AuthController(WebAPIProjectContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserCredential>> Register(UserCredential request)
        {
            
            _context.UserCredential.Add(request);
            await _context.SaveChangesAsync();

            return Ok("User Registered Successfully");
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(User request)
        {
            var data = _context.UserCredential.Where(m => m.Username == request.Username).FirstOrDefault();
            if (data==null || data.Username != request.Username)
            {
                new Error("User Not Found.");
                return BadRequest("User Not Found.");
            }
            if (data.Password != request.Password)
            {
                new Error("Password Wrong");
                return BadRequest("Password Wrong");
            }


            string token = CreateToken(request,data.Role);
            
            return Ok(token);
        }
        private string CreateToken(User user,string role)
        {
            
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, role)

            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
                GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(10),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
