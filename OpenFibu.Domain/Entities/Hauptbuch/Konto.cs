using OpenFibu.Domain.Entities.Journal;

namespace OpenFibu.Domain.Entities.Hauptbuch;

public class Konto
{
    public static Konto Erstellen(string bezeichnung, uint kontonummer) => new(bezeichnung, kontonummer);

    public Konto() { }
    private Konto(string bezeichnung, uint kontonummer)
    {
        Bezeichnung = bezeichnung;
        Kontonummer = kontonummer;
    }

    public void Buchen(Buchung buchung)
    {
        Buchungen.Add(buchung);
        if (buchung.SollHaben == SollHaben.Soll)
            Soll += buchung.Betrag;
        else
            Haben += buchung.Betrag;
    }

    public string? Id { get; }
    public string Bezeichnung { get; }
    public uint Kontonummer { get; }
    public decimal Soll { get; private set; }
    public decimal Haben { get; private set; }
    public decimal Saldo => Soll - Haben;
    public List<Buchung> Buchungen { get; set; } = new();
}