using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;

        public CpuMetricsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            _logger.LogDebug(1, "NLog встроен в CpuMetricsController");
        }


        [HttpGet("agent/{agentId}/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAgent([FromRoute] int agentId,
        [FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"api/metrics/cpu/agent/{agentId}/from/{fromTime}/to/{toTime}");
            return Ok();
        }
        [HttpGet("cluster/from/{fromTime}/to/{toTime}")]
        public IActionResult GetMetricsFromAllCluster([FromRoute] TimeSpan
        fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation($"api/metrics/cpu/cluster/from/{fromTime}/to/{toTime}");
            return Ok();
        }
    }
}

