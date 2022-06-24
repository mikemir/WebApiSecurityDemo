using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using WebApiSecurityDemo.Model;
using WebApiSecurityDemo.Model.Dtos;
using WebApiSecurityDemo.Services;
using WebApiSecurityDemo.Utils;

namespace WebApiSecurityDemo.Controllers
{
    //ToDo: Api Version 1.0 Deprecated 01-05-2021
    [ApiVersion("3.0")]
    [ApiVersion("2.0")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        private readonly IAccountService _accountService;
        private readonly ITokenManager _tokenManager;

        public AccountController(ILoggerManager logger,
                                 IAccountService accountService,
                                 ITokenManager tokenManager)
        {
            _logger = logger;
            _accountService = accountService;
            _tokenManager = tokenManager;
        }

        [MapToApiVersion("1.0")] //ToDo: V1 End Point Deprecated 01-05-2021
        [HttpPost("login"), Obsolete("Deprecated")]
        public async Task<IActionResult> Login([FromBody] UserDto user)
        {
            IActionResult result;

            _logger.LogInfo("Comienzo del login de usuario...");

            var userResult = _accountService.Login(user);
            if (userResult == null)
            {
                return NotFound();
            }

            result = Ok(userResult);

            _logger.LogInfo("Fin del login de usuario...");

            return result;
        }

        [MapToApiVersion("2.0")]
        [HttpPost("login")]
        public async Task<IActionResult> LoginV2([FromBody] UserDto user)
        {
            IActionResult result;

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _logger.LogInfo("Comienzo del login de usuario...");

            var request = this.HttpContext.Request;
            var metadata = $"[{request.Host}{request.Path}], [Method: {request.Method}]";
            var data = string.Join(' ', request.Headers);

            var userResult = _accountService.Login(user);
            if (userResult == null)
            {
                _logger.LogInfo($"Usuario o contraseña incorrecta. Headers: {metadata} {data} [IP: {request.HttpContext.Connection.RemoteIpAddress}]");

                return NotFound();
            }

            _logger.LogInfo($"Login exitoso. Headers: {metadata} {data} [IP: {request.HttpContext.Connection.RemoteIpAddress}]");

            result = Ok(userResult);

            _logger.LogInfo("Fin del login de usuario...");

            return result;
        }

        [MapToApiVersion("1.0")]
        [HttpGet("{id}"), Obsolete("Deprecated")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(_accountService.GetById(id));
        }

        [MapToApiVersion("2.0")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdV2(string id)
        {
            return Ok(_accountService.GetByIdV2(id));
        }

        [MapToApiVersion("3.0")]
        [HttpGet, Authorize]
        public async Task<IActionResult> GetByIdV3()
        {
            var id = _tokenManager.GetIdJwt();

            return Ok(_accountService.GetById(id));
        }
    }
}