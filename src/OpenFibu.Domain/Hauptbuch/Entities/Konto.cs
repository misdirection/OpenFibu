using OpenFibu.Domain.Shared.Enums;
using OpenFibu.Shared;

namespace OpenFibu.Domain.Hauptbuch.Entities;

public class Konto : Entity, IAggregateRoot
{
    public Konto()
    {
    }

    private Konto(string bezeichnung, uint kontonummer)
    {
        Bezeichnung = bezeichnung;
        Kontonummer = kontonummer;
    }

    public string? Id { get; }
    public string Bezeichnung { get; }
    public uint Kontonummer { get; }
    public decimal Soll { get; private set; }
    public decimal Haben { get; private set; }
    public decimal Saldo => Soll - Haben;
    public List<Buchung> Buchungen { get; set; } = new();
    public static Konto Erstellen(string bezeichnung, uint kontonummer) => new(bezeichnung, kontonummer);

    public void Buchen(Buchung buchung)
    {
        Buchungen.Add(buchung);
        if (buchung.SollHaben == SollHaben.Soll)
            Soll += buchung.Betrag;
        else
            Haben += buchung.Betrag;
    }
}