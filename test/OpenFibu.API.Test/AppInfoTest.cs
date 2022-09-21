using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using OpenFibu.API.Controllers;
using OpenFibu.API.DTO;
using Xunit;

namespace OpenFibu.API.Test;

public class AppInfoTest
{
    [Fact]
    public void ItReturnsOkAndAppInfoResponse()
    {
        // Arrange
        var appInfoController = new AppInfoController();

        // Act
        var result = appInfoController.GetAppInfo();

        // Assert
        result.Result.Should().BeOfType<OkObjectResult>()
            .Which.StatusCode.Should().Be((int)HttpStatusCode.OK);
        ((OkObjectResult)result.Result!).Value.Should().BeOfType<AppInfoResponse>();
    }
}