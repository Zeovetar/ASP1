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
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;

        private IHddMetricsRepository repository;

        public HddMetricsController(IHddMetricsRepository repository, ILogger<HddMetricsController> logger)
        {
            _logger = logger;
            this.repository = repository;
        }
        [HttpPost("create")]
        public IActionResult Create([FromBody] MetricCreateRequest request)
        {
            repository.Create(new HddMetric
            {
                Time = request.Time,
                Value = request.Value
            });
            _logger.LogInformation($"AgentsController: api/HddMetrics/create");
            return Ok();
        }

        [HttpGet("all")]
        public IActionResult GetAll()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetric, MetricDto>());
            var mapper = config.CreateMapper();
            IList<HddMetric> metrics = repository.GetAll();
            var response = new AllMetricsResponse()
            {
                Metrics = new List<MetricDto>()
            };
            foreach (var metric in metrics)
            {
                // Добавляем объекты в ответ, используя маппер
                response.Metrics.Add(mapper.Map<MetricDto>(metric));
            }
            _logger.LogInformation($"AgentsController: api/HddMetrics/all");
            return Ok(response);

        }

        [HttpGet("fromtime/{FromTime}/totime/{ToTime}")]
        public IActionResult GetByTimeToTime([FromRoute] TimeSpan FromTime, [FromRoute] TimeSpan ToTime)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<HddMetric, MetricDto>());
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
            _logger.LogInformation($"AgentsController: api/HddMetrics/all");
            return Ok(response);
        }
    }
}
