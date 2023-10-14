using Application.CQS.Auth.Command.CreateAuth;
using Application.CQS.Auth.Command.RemoveAuth;
using Application.DataTransferObject;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Mime;
using WebApi.Abstractions;

namespace Presentation.Controllers.Api.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthentificationController : ApiController
    {
        private readonly ISender _sender;
        private readonly ILogger<AuthentificationController> _logger;

        public AuthentificationController(ISender sender, IHttpContextAccessor httpContextAccessor, ILogger<AuthentificationController> logger)
            : base(sender, httpContextAccessor)
        {
            this._sender = sender;
            _logger = logger;
        }
        [AllowAnonymous]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {
            
            var command = new CreateAuthCommand(userLoginDTO.Email, userLoginDTO.Password);
            var result = await _sender.Send(command);
            return result.IsSuccess?Ok(result.Value):BadRequest();
        }
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            var jwt = HttpContextAccessor.HttpContext.Request.Headers.Authorization;
            if(!jwt.Any())
            {
                return BadRequest();
            }
            var command = new RemoveAuthCommand(jwt);
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest();
        }
    }
}
