using OpenFibu.Domain.Entities.Journal;

namespace OpenFibu.Domain.Entities.Hauptbuch;

public class Buchung
{
    public string? Id { get; }

    public decimal Betrag { get; set; }
    public SollHaben SollHaben { get; set; }
}