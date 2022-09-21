using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OpenFibu.API.Controllers;
using OpenFibu.API.DTO;
using Xunit;

namespace OpenFibu.API.Test;

public class HealthControllerTest
{
    [Fact]
    public void ItReturnsOkAndHealthResponse()
    {
        // Arrange
        var healthController = new HealthController();

        // Act
        var result = healthController.GetHealth();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);

        var okObjectResult = result.Result as OkObjectResult;
        okObjectResult!.Value.Should().BeOfType<HealthResponse>();

        var healthResponse = okObjectResult.Value as HealthResponse;
        healthResponse.Should().NotBeNull();

        healthResponse!.Status.Should().Be("Alive and well.");
    }
}