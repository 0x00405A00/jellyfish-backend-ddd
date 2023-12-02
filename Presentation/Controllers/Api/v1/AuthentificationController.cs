using Application.CQS.Auth.Command.CreateAuth;
using Application.CQS.Auth.Command.RefreshAuth;
using Application.CQS.Auth.Command.RemoveAuth;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extension;
using Shared.ApiDataTransferObject;
using Shared.DataTransferObject;
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
        [ProducesResponseType(typeof(AuthDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDTO userLoginDTO)
        {

            var command = new CreateAuthCommand(userLoginDTO.Email, userLoginDTO.Password, HttpContext.Connection.LocalIpAddress, HttpContext.Connection.LocalPort, HttpContext.Connection.RemoteIpAddress, HttpContext.Connection.RemotePort, HttpContext.Request.Headers.UserAgent);
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest();
        }
        [AllowAnonymous]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AuthDTO), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [HttpPost("refresh/{refreshToken}")]
        public async Task<IActionResult> Refresh(string refreshToken)
        {
            var token = HttpContext.GetAuthorizationHeader();
            var command = new RefreshAuthCommand(token, refreshToken);
            var result = await _sender.Send(command);
            return result.IsSuccess ? Ok(result.Value) : BadRequest();
        }
        [Authorize]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Guid), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
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
