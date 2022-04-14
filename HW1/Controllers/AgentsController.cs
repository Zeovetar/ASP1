using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly ILogger<CpuMetricsController> _logger;
        public AgentsController(ILogger<CpuMetricsController> logger)
        {
            _logger = logger;
            
        }

        private readonly AgentsHolder _holder;
//        public AgentsController()
//        {
//        }
        public AgentsController(AgentsHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _holder.Add(agentInfo);
            _logger.LogInformation($"AgentController: register");
            return Ok();
        }

        [HttpGet("registered")]
        public IActionResult RegisteredAgent()
        {
            List<int> agents = new();
            foreach (AgentInfo iter in _holder.Values)
            {
                    agents.Add(iter.AgentId);
            }
            _logger.LogInformation($"AgentController: registered");
            return Ok(agents);
        }
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"AgentController: enable/{agentId}");
            return Ok();
        }
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            _logger.LogInformation($"AgentController: disable/{agentId}");
            return Ok();
        }
    }
}