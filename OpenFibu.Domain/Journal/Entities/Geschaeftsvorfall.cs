using Ardalis.GuardClauses;
using OpenFibu.Shared;

namespace OpenFibu.Domain.Journal.Entities;

public class Geschaeftsvorfall : Entity, IAggregateRoot
{
    public string? Id { get; set; }
    
    public string LaufendeNummer { get; set; }
    public string Belegnummer { get; set; }
    public DateOnly Belegdatum { get; set; }
    public DateOnly Buchungsdatum { get; set; }
    public string? Buchungstext { get; set; }

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
    }

    public void BuchungHinzufuegen(Buchung buchung)
    {
        _buchungen.Add(buchung);
    }
}