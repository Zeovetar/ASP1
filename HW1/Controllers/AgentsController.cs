using Microsoft.AspNetCore.Mvc;
using System;
namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentsController : ControllerBase
    {
        private readonly AgentsHolder _holder;
        public AgentsController(AgentsHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("register")]
        public IActionResult RegisterAgent([FromBody] AgentInfo agentInfo)
        {
            _holder.Add(agentInfo);
            return Ok();
        }

        [HttpGet("registered")]
        public IActionResult RegisteredAgent()
        {
            foreach (AgentInfo iter in _holder.Values)
            {
                    iter.AgentId;
            }
            return Ok();
        }
        [HttpPut("enable/{agentId}")]
        public IActionResult EnableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }
        [HttpPut("disable/{agentId}")]
        public IActionResult DisableAgentById([FromRoute] int agentId)
        {
            return Ok();
        }
    }
}