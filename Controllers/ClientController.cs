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
using CWG.API.Workflow.ServiceContracts.ClientModule;
using CWG.API.Workflow.ViewModels.Auth;
using CWG.API.Workflow.ViewModels.ClientModule;

namespace CWG.API.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _repo;
        private readonly IConfiguration _config;
       
        public ClientController(IClientService repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            // int _logedUserid = -1;
            // var currentUser = HttpContext.User;
            // _logedUserid = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            
            return Ok(new
            {
                status = "ok",
                code = 200,
                messages = "",
                result = await _repo.GetClients()
            });
        }

        [HttpGet("{clientId}")]
        public async Task<IActionResult> Get(int clientId)
        {
            int _logedUserid = -1;
            var currentUser = HttpContext.User;
            _logedUserid = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(new
            {
                status = "ok",
                code = 200,
                messages = "",
                result = await _repo.GetClient(clientId)
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ClientDetailViewModel model)
        {
            int _logedUserid = -1;
            var currentUser = HttpContext.User;
            _logedUserid = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            return Ok(new
            {
                status = "ok",
                code = 200,
                messages = "",
                result = await _repo.InsertClient(model)
            });
        }

        [HttpPut("{clientId}")]
        public async Task<IActionResult> Put(int clientId, [FromBody] ClientDetailViewModel model)
        {
            int _logedUserid = -1;
            var currentUser = HttpContext.User;
            _logedUserid = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            await _repo.UpdateClient(model);

            return Ok(new
            {
                status = "ok",
                code = 200,
                messages = "",
                result = model.ClientId
            });
        }

        [HttpPatch("{clientId}")]
        public async Task<IActionResult> Patch(int clientId, [FromBody] ClientDetailViewModel model)
        {
            int _logedUserid = -1;
            var currentUser = HttpContext.User;
            _logedUserid = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);
            model.ClientId = clientId;
            var _clientDetail = await _repo.GetClient(clientId);
            if(_clientDetail==null)
                return NotFound(new
                {
                    status = "ok",
                    code = 200,
                    messages = "",
                    result = _clientDetail.ClientId
                });
            
            model.Patch(_clientDetail);
            await _repo.UpdateClient(_clientDetail);

            return Ok(new
            {
                status = "ok",
                code = 200,
                messages = "",
                result = _clientDetail.ClientId
            });
        }

        [HttpDelete("{clientId}")]
        public async Task<IActionResult> Delete(int clientId)
        {
            int _logedUserid = -1;
            var currentUser = HttpContext.User;
            _logedUserid = Int32.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            var _clientDetail = await _repo.GetClient(clientId);
            if(_clientDetail==null)
                return NotFound(new
                {
                    status = "ok",
                    code = 200,
                    messages = "",
                    result = clientId
                });
            
            await _repo.DeleteClient(clientId);

            return Ok(new
            {
                status = "ok",
                code = 200,
                messages = "",
                result = clientId
            });
        }

        
    }
}