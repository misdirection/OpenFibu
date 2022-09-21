using System.Diagnostics.CodeAnalysis;

namespace OpenFibu.API;

[ExcludeFromCodeCoverage(Justification = "We dont test startup classes")]
public class Program
{
    public static async Task Main(string[] args)
    {
        var configuration = BuildConfiguration();

        var host = CreateHostBuilder(args, configuration).Build();

        await host.RunAsync();
    }

    private static IHostBuilder CreateHostBuilder(string[] args, IConfiguration configuration)
    {
        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                    webBuilder.UseConfiguration(configuration);
                }
            );
    }

    private static IConfiguration BuildConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder();

        configurationBuilder.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        configurationBuilder.AddJsonFile("appsettings.Development.json", optional: true, reloadOnChange: true);

        configurationBuilder.AddUserSecrets<Startup>();
        configurationBuilder.AddEnvironmentVariables();

        return configurationBuilder.Build();
    }
}