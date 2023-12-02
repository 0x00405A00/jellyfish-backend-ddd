using Application.CQS.Role.Commands.CreateRole;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Presentation.Extension;
using Presentation.WebResponse;
using Shared.ApiDataTransferObject;
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

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<RoleDTO>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 401)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 404)]
        [HttpPost()]
        public async Task<IActionResult> CreateRole([FromBody] RoleDTO RoleDTO, CancellationToken cancellationToken)
        {
            var userUuid = HttpContextAccessor.HttpContext.GetUserUuidFromRequest();
            var command = new CreateRoleCommand(userUuid, RoleDTO.Name, RoleDTO.Description);

            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<RoleDTO>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 401)]
        [HttpGet("{guid}")]
        public async Task<IActionResult> GetRole(Guid guid, CancellationToken cancellationToken)
        {
            var command = new Application.CQS.Role.Queries.GetRoleById.GetRoleByIdQuery(guid);

            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }

        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ApiDataTransferObject<List<RoleDTO>>), 200)]
        [ProducesResponseType(typeof(ApiDataTransferObject<>), 401)]
        [HttpGet()]
        public async Task<IActionResult> GetRoles(CancellationToken cancellationToken)
        {
            var command = new Application.CQS.Role.Queries.GetRoles.GetRolesQuery();

            var result = await Sender.Send(command, cancellationToken);
            return result.PrepareResponse();
        }
    }
}