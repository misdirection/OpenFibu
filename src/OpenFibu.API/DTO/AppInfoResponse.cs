using System.Reflection;

namespace OpenFibu.API.DTO;

public record AppInfoResponse
{
    public string Version { get; } = Assembly.GetEntryAssembly()!.GetName().Version!.ToString();
    public string Name { get; } = Assembly.GetEntryAssembly()!.GetName().Name!;
}