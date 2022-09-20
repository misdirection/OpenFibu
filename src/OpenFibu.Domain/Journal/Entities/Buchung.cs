using OpenFibu.Domain.Shared.Enums;
using OpenFibu.Shared;

namespace OpenFibu.Domain.Journal.Entities;

public class Buchung : Entity
{
    public Buchung()
    {
    }

    private Buchung(uint kontonummer, decimal betrag, SollHaben sollHaben)
    {
        Kontonummer = kontonummer;
        Betrag = betrag;
        SollHaben = sollHaben;
    }

    public string? Id { get; set; }

    public string? KontoId { get; set; }
    public uint Kontonummer { get; set; }
    public decimal Betrag { get; set; }
    public SollHaben SollHaben { get; set; }

    public static Buchung Erstellen(uint kontonummer, decimal betrag, SollHaben sollHaben)
        => new(kontonummer, betrag, sollHaben);
}