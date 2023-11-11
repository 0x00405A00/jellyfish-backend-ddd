using Application.CQS.Role.Commands.CreateRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Abstractions;
using Presentation.Extension;
using Presentation.WebResponse;
using Shared.Const;
using Shared.DataTransferObject;
using System.Net.Mime;
using WebApi.Abstractions;

namespace Presentation.Controllers.Api.v1
{
    [Authorize(Policy = AuthorizationConst.Policy.AdminPolicy)]
    public class RoleController : ApiController
    {

        private readonly ILogger<RoleController> _logger;

        public RoleController(ISender sender, IHttpContextAccessor httpContextAccessor, ILogger<RoleController> logger) : base(sender, httpContextAccessor)
        {
            _logger = logger;
        }

        
        [HttpPost()]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> CreateUser([FromBody] RoleDTO RoleDTO, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new CreateRoleCommand(userUuid, RoleDTO.Name, RoleDTO.Description);

            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }

        [HttpGet("{guid}")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetUser(Guid guid, CancellationToken cancellationToken)
        {
            var command = new Application.CQS.Role.Queries.GetRoleById.GetRoleByIdQuery(guid);

            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }

        [HttpGet()]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken)
        {
            var command = new Application.CQS.Role.Queries.GetRoles.GetRolesQuery();

            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }
    }
}