namespace OpenFibu.API.DTO;

public record HealthResponse
{
    public string Status { get; } = "Alive and well.";
}