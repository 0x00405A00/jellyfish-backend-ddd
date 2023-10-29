using Application.CQS.Mail.Queries.GetMails;
using Domain.Const;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Net;

namespace Presentation.Controllers
{
    [ApiExplorerSettings(IgnoreApi = false)]
    [Authorize(Policy = AuthorizationConst.Policy.AdminPolicy)]
    [Route("health")]
    public class HealthController : ControllerBase
    {
        private readonly HealthCheckService _healthCheckService;
        private readonly ISender sender;

        public HealthController(HealthCheckService healthCheckService,ISender sender)
        {
            _healthCheckService = healthCheckService;
            this.sender = sender;
        }

        /// <summary>
        /// Get Health
        /// </summary>
        /// <remarks>Provides an indication about the health of the API</remarks>
        /// <response code="200">API is healthy</response>
        /// <response code="503">API is unhealthy or in degraded state</response>
        [HttpGet]
        [ProducesResponseType(typeof(HealthReport), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var report = await _healthCheckService.CheckHealthAsync();

            return report.Status == HealthStatus.Healthy ? Ok(report) : StatusCode((int)HttpStatusCode.ServiceUnavailable, report);
        }
        [HttpGet("mail-queue")]
        [ProducesResponseType(typeof(HealthReport), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetMailQueue()
        {
            var command = new GetMailsQuery();
            var mails = await sender.Send(command);

            return Ok(mails.Value);
        }
    }
}
