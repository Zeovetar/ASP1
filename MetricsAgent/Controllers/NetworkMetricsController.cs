using MetricsAgent.DAL;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System;
using AutoMapper;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;

        private INetworkMetricsRepository repository;

        public NetworkMetricsController(INetworkMetricsRepository repository, ILogger<NetworkMetricsController> logger)
        {
            _logger = logger;
            this.repository = repository;
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] MetricCreateRequest request)
        {
            repository.Create(new NetworkMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            _logger.LogInformation($"AgentsController: api/NetworkMetrics/create");
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetric, MetricDto>());
            var mapper = config.CreateMapper();
            IList<NetworkMetric> metrics = repository.GetAll();
            var response = new AllMetricsResponse()
            {
                Metrics = new List<MetricDto>()
            };
            foreach (var metric in metrics)
            {
                // Добавляем объекты в ответ, используя маппер
                response.Metrics.Add(mapper.Map<MetricDto>(metric));
            }
            _logger.LogInformation($"AgentsController: api/NetworkMetrics/all");
            return Ok(response);

        }

        [HttpGet("fromtime/{FromTime}/totime/{ToTime}")]
        public IActionResult GetByTimeToTime([FromRoute] TimeSpan FromTime, [FromRoute] TimeSpan ToTime)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<NetworkMetric, MetricDto>());
            var mapper = config.CreateMapper();
            var metrics = repository.GetByTimeToTime(FromTime, ToTime);
            var response = new AllMetricsResponse()
            {
                Metrics = new List<MetricDto>()
            };
            foreach (var metric in metrics)
            {
                response.Metrics.Add(mapper.Map<MetricDto>(metric));
            }
            _logger.LogInformation($"AgentsController: api/NetworkMetrics/all");
            return Ok(response);
        }
    }
}
