using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;

namespace OpenFibu.API.Test;

// We dont need this for now, but later if we want to e.g. a custom
// database implementation for mock data this comes handy to switch
// environment or to rewrite dependency injection.
public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
{
    protected override IHostBuilder CreateHostBuilder()
    {
        return Host.CreateDefaultBuilder().ConfigureWebHost((builder) => { builder.UseStartup<Startup>(); });
    }
}