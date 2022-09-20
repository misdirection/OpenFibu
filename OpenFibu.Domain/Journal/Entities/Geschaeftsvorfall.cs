using Ardalis.GuardClauses;
using OpenFibu.Domain.Guards;

namespace OpenFibu.Domain.Entities.Journal;

public class Geschaeftsvorfall
{
    public string? Id { get; set; }
    public string LaufendeNummer { get; set; }
    public string Belegnummer { get; set; }
    public DateOnly Belegdatum { get; set; }
    public DateOnly Buchungsdatum { get; set; }
    public string? Buchungstext { get; set; }
    public bool IstGebucht { get; set; } = false;

    public decimal Saldo { get; set; }
    public IReadOnlyCollection<Buchung> Buchungen => _buchungen;
    private readonly List<Buchung> _buchungen = new();

    public static Geschaeftsvorfall Erstellen(string belegnummer, DateOnly buchungsdatum, DateOnly belegdatum, string? buchungstext)
    {
        return new Geschaeftsvorfall(belegnummer, buchungsdatum, belegdatum, buchungstext);
    }

    public Geschaeftsvorfall() { }

    private Geschaeftsvorfall(string belegnummer, DateOnly buchungsdatum, DateOnly belegdatum, string? buchungstext)
    {
        Belegnummer = belegnummer;
        Buchungsdatum = buchungsdatum;
        Belegdatum = belegdatum;
        Buchungstext = buchungstext;
    }

    public void Buchen()
    {
        Guard.Against.Zero(Saldo);
        Guard.Against.Zero(Buchungen.Count);
        Guard.Against.IstGebucht(this);


        IstGebucht = true;
    }

    public void BuchungHinzufuegen(Buchung buchung)
    {
        _buchungen.Add(buchung);
    }
}