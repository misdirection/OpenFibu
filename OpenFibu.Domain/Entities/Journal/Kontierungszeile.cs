namespace OpenFibu.Domain.Entities.Journal;

public class Kontierungszeile
{
    public string Id { get; set; }
    public decimal Betrag { get; set; }
    public string KontoId { get; set; }
    public uint Kontonummer { get; set; }
}