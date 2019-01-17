using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using CWG.API.Workflow.ServiceContracts.Auth;
using CWG.API.Workflow.ViewModels.Auth;

namespace CWG.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _repo;
        private readonly IConfiguration _config;
       
        public UserController(IUserService repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            int _logedUserid = -1;
            var currentUser = HttpContext.User;
            _logedUserid = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(new
            {
                status = "ok",
                code = 200,
                messages = "",
                result = await _repo.GetUserInfo(_logedUserid)
            });
        }

        
    }
}