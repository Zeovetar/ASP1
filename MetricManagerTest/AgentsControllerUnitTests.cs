using MetricsManager.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using Xunit;
namespace MetricsManagerTests
{
    public class AgentControllerUnitTests
    {
        private AgentsController controller;
        public AgentControllerUnitTests()
        {
            var _holder1 = new AgentsHolder();
            controller = new AgentsController(_holder1);
        }
        [Fact]
        public void RegisterAgent_ReturnsOk()
        {
            //Arrange
            Uri agentUri = new("http://localhost:5100/agent1");
            AgentInfo agent = new() { AgentId = 1, AgentAddress = agentUri };

            //Act
            var result = controller.RegisterAgent(agent);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void GetRegisteredAgent_ReturnsOk()
        {
            //Arrange
            //Act
            var result = controller.RegisteredAgent();
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void RegisteredAgentByID_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            //Act
            var result = controller.EnableAgentById(agentId);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
        [Fact]
        public void DisableAgentByID_ReturnsOk()
        {
            //Arrange
            var agentId = 1;
            //Act
            var result = controller.DisableAgentById(agentId);
            // Assert
            _ = Assert.IsAssignableFrom<IActionResult>(result);
        }
    }
}