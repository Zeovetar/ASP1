using MetricsAgent.DAL;
//using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;

        public NetworkMetricsController(ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
        }

        private ICpuMetricsRepository repository;
        public NetworkMetricsController(INetworkMetricsRepository repository)
        {
            this.repository = repository;
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] MetricCreateRequest request)
        {
            repository.Create(new Metric
            {
                Time = request.Time,
                Value = request.Value
            });
            _logger.LogInformation($"AgentsController: api/Networkmetrics/create");
            return Ok();
        }
        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var metrics = repository.GetAll();
            var response = new AllMetricsResponse()
            {
                Metrics = new List<MetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(new MetricDto
                {
                    Time = metric.Time,
                    Value = metric.Value,
                    Id = metric.Id
                });
            }
            _logger.LogInformation($"AgentsController: api/rammetrics/all");
            return Ok(response);
        }
    }
}
