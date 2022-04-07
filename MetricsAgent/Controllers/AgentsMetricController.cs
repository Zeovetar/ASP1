using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        [HttpGet("cpu/from/{fromTime}/to/{toTime}")]
        public IActionResult GetCpuMetrics([FromRoute] TimeSpan 
            fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
        [HttpGet("dotnet/errors-count/from/{fromTime}/to/{toTime}")]
        public IActionResult GetDotnetMetrics([FromRoute] TimeSpan 
            fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
        [HttpGet("network/from/{fromTime}/to/{toTime}")]
        public IActionResult GetNetworkMetrics([FromRoute] TimeSpan
            fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
        [HttpGet("hdd/left/from/{fromTime}/to/{toTime}")]
        public IActionResult GetHDDMetrics([FromRoute] TimeSpan 
            fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
        [HttpGet("ram/available/from/{fromTime}/to/{toTime}")]
        public IActionResult GetRAMMetrics([FromRoute] TimeSpan
            fromTime, [FromRoute] TimeSpan toTime)
        {
            return Ok();
        }
    }
}