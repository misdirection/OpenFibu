using OpenFibu.Domain.Shared.Enums;
using OpenFibu.Shared;

namespace OpenFibu.Domain.Hauptbuch.Entities;

public class Buchung : Entity
{
    public string? Id { get; }
    public decimal Betrag { get; set; }
    public SollHaben SollHaben { get; set; }
}