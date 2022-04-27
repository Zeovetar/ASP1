using System;
using System.Collections.Generic;
namespace MetricsAgent.Responses
{
    public class AllMetricsResponse
    {
        public List<MetricDto> Metrics { get; set; }
    }
    public class MetricDto
    {
        public TimeSpan Time { get; set; }
        public int Value { get; set; }
        public int Id { get; set; }
    }
}
