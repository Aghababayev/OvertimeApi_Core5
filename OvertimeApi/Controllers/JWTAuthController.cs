using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using OvertimeApi.DataAceess;
using OvertimeApi.ViewModels;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace OvertimeApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class JWTAuthController : ControllerBase
    {
        private readonly Context _context;
        private readonly IConfiguration _configuration;

        public JWTAuthController(Context context,IConfiguration configuration)
        {
            _context = context;
         _configuration = configuration;
        }
        [HttpPost]
        public async Task<ActionResult<LoginResultVM>> Login(UserLoginVM userLoginVM)
        {

            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == userLoginVM.UserName && x.UserPassword == userLoginVM.UserPassword);
            if (user == null)
            {
                return NotFound();
            }
            var token = GenerateJwtToken(user);
            return Ok(new LoginResultVM

            {
                UserId = user.UserId,
                AutToken = token
            });

          
        }
       private string GenerateJwtToken(User user)
        {
            var tokenhandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                    new Claim(ClaimTypes.Name, user.UserName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenhandler.CreateToken(tokenDescriptor);
            return tokenhandler.WriteToken(token);  


        }

    }
}
