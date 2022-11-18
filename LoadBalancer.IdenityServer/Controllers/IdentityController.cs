using LoadBalancerIdentityServer.Models.Identity;
using LoadBalancer.IdenityServer.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LoadBalancer.IdenityServer.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User> userManager;
        private readonly AppSettings appSettings;

        public IdentityController(UserManager<User> userManager, IOptions<AppSettings> appSettings)
        {
            this.userManager = userManager;
            this.appSettings = appSettings.Value;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register(RegisterRequestModel model)
        {
            var user = new User
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await this.userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(user, Roles.User.ToString());
                return Ok();
            }

            return BadRequest(result.Errors);
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<string>> Login(LoginRequestModel model)
        {
            var user = await this.userManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                return Unauthorized();
            }

            var passwordValid = await this.userManager.CheckPasswordAsync(user, model.Password);

            if (!passwordValid)
            {
                return Unauthorized();
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret!);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var encryptedToken = tokenHandler.WriteToken(token);

            return encryptedToken;
        }

        [HttpPost]
        [Route("logout")]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}