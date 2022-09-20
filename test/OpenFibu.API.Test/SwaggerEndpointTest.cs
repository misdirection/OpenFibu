using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace OpenFibu.API.Test;

public class SwaggerEndpointTest : IClassFixture<CustomWebApplicationFactory>
{
    private readonly WebApplicationFactory<Startup> _factory;

    public SwaggerEndpointTest(CustomWebApplicationFactory factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task ItIsReached()
    {
        // Arrange
        var client = _factory.CreateClient();

        // Act
        var response = await client.GetAsync("/swagger/v1/swagger.json");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }
}