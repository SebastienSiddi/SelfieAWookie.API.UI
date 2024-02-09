using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SelfieAWookie.API.UI.Application.DTOs;
using SelfieAWookies.Core.Selfies.Infrastructures.Configurations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SelfieAWookie.API.UI.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticateController : ControllerBase
    {
        #region Fields
        private readonly SecurityOption _option = null;
        private readonly UserManager<IdentityUser> _userManager = null;
        private readonly IConfiguration _configuration = null;
        #endregion

        #region Constructors
        public AuthenticateController(UserManager<IdentityUser> userManager, IConfiguration configuration, IOptions<SecurityOption> options)
        {
            this._option = options.Value;
            this._userManager = userManager;
            this._configuration = configuration;
        }
        #endregion

        #region Public methods
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] AuthenticateUserDto dtoUser)
        {
            IActionResult result = this.BadRequest();

            var user = new IdentityUser(dtoUser.Login);
            user.Email = dtoUser.Login;
            user.UserName = dtoUser.Name;
            var success = await this._userManager.CreateAsync(user, dtoUser.Password);

            if (success.Succeeded)
            {
                dtoUser.Token = this.GenerateJwtToken(user);
                result = this.Ok(dtoUser);
            }

            return result;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthenticateUserDto dtoUser)
        {
            IActionResult result = this.BadRequest();

            var user = await this._userManager.FindByEmailAsync(dtoUser.Login);
            if (user != null)
            {
                var verif = await this._userManager.CheckPasswordAsync(user, dtoUser.Password);
                if (verif)
                {

                    result = this.Ok(new AuthenticateUserDto()
                    {
                        Login = user.Email,
                        Name = user.UserName,
                        Token = this.GenerateJwtToken(user)
                    });
                }
            }

            return result;
        }
        #endregion

        #region Internal methods
        private string GenerateJwtToken(IdentityUser user)
        {          
            var jwtTokenHandler = new JwtSecurityTokenHandler();
           
            var key = Encoding.UTF8.GetBytes(this._option.Key);
           
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim("Id", user.Id),
                new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),              
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            }),              
                Expires = DateTime.UtcNow.AddHours(6),              
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(tokenDescriptor);

            var jwtToken = jwtTokenHandler.WriteToken(token);

            return jwtToken;
        }
        #endregion
    }
}