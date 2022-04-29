using MetricsAgent.Controllers;
using MetricsAgent.DAL;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
namespace MetricsAgentTests
{
    public class DotnetMetricsControllerUnitTests
    {
        private DotnetMetricsController controller;
        private Mock<IDotnetMetricsRepository> mock;
        public DotnetMetricsControllerUnitTests()
        {
            mock = new Mock<IDotnetMetricsRepository>();
            controller = new DotnetMetricsController(mock.Object, Mock.Of<ILogger<DotnetMetricsController>>());
        }
        [Fact]
        public void Create_ShouldCall_Create_From_Repository()
        {
            mock.Setup(repository =>
            repository.Create(It.IsAny<DotnetMetric>())).Verifiable();
            var result = controller.Create(new
            MetricsAgent.Requests.MetricCreateRequest
            {
                Time = TimeSpan.FromSeconds(1),
                Value = 50
            });
            mock.Verify(repository => repository.Create(It.IsAny<DotnetMetric>()),
            Times.AtMostOnce());
        }

        [Fact]
        public void GetAll_ShouldCall_GetAll_From_Repository()
        {
            var mockMetrics = new List<DotnetMetric>()
            {
                { new DotnetMetric() { Id = 1, Time = TimeSpan.FromSeconds(5), Value = 100 } },
                { new DotnetMetric() { Id = 2, Time = TimeSpan.FromSeconds(10), Value = 110 } }
            };
            mock.Setup(repository =>
            repository.GetAll()).Returns(mockMetrics);
            var result = controller.GetAll();
            mock.Verify(repository => repository.GetAll(),
            Times.AtMostOnce());
        }

        [Fact]
        public void GetByTimeToTime()
        {
            var mockMetrics = new List<DotnetMetric>()
            {
                { new DotnetMetric() { Id = 1, Time = TimeSpan.FromSeconds(5), Value = 100 } },
                { new DotnetMetric() { Id = 2, Time = TimeSpan.FromSeconds(10), Value = 110 } }
            };
            mock.Setup(repository =>
            repository.GetByTimeToTime(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>())).Returns(mockMetrics);
            var result = controller.GetByTimeToTime(TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(2));
            mock.Verify(repository => repository.GetByTimeToTime(It.IsAny<TimeSpan>(), It.IsAny<TimeSpan>()),
            Times.AtMostOnce());
        }
    }
}
