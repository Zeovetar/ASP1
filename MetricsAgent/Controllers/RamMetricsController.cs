using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamMetricsController : ControllerBase
    {
        private readonly ILogger<RamMetricsController> _logger;

        private IRamMetricsRepository repository;
        public RamMetricsController(IRamMetricsRepository repository, ILogger<RamMetricsController> logger)
        {
            _logger = logger;
            this.repository = repository;
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] MetricCreateRequest request)
        {
            repository.Create(new RamMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            _logger.LogInformation($"AgentsController: api/rammetrics/create");
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
        [HttpGet("FromTime/{FromTime}/ToTime/{ToTime}")]
        public IActionResult GetByTimeToTime([FromRoute] TimeSpan FromTime, [FromRoute] TimeSpan ToTime)
        {
            var metrics = repository.GetByTimeToTime(FromTime, ToTime);
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
