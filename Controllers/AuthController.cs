using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CWG.API.Workflow.ServiceContracts.Auth;
using CWG.API.Workflow.ViewModels.Auth;

namespace CWG.API.Controllers
{
    [Route("api/v1")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _repo;
        private readonly IConfiguration _config;
       
        public AuthController(IAuthService repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel _user)
        {
            _user.UserName = _user.UserName.ToLower();

            if (await _repo.UserExists(_user.UserName))
                return BadRequest("Username already exists");

            var createdUser = await _repo.Register(_user, _user.Password);

            //return CreatedAtRoute("GetUser", new {controller = "Users", id = createdUser.UserId} , createdUser);
             return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel userForLoginDto)
        {
            var userFromRepo = await _repo.Login(userForLoginDto.UserName.ToLower(), userForLoginDto.Password);

            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.UserId.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.UserName),
                new Claim(ClaimTypes.Email, userFromRepo.Email),
                new Claim("fullname", userFromRepo.FullName)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8
                .GetBytes(_config.GetSection("JwtIssuerOptions:SecurityKey").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            

            return Ok(new
            {
                token = tokenHandler.WriteToken(token),
                expire = tokenDescriptor.Expires
                //userFromRepo
            });
        }
    }
}