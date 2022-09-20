using System.Net.Http.Headers;
using OpenFibu.Shared;

namespace OpenFibu.Domain.Vorkontierung.Entities;

public sealed class Vorkontierung : Entity, IAggregateRoot
{
    public Vorkontierung(string laufendeNummer, string belegnummer, DateOnly belegdatum, DateOnly buchungsdatum, string? buchungstext)
    {
        LaufendeNummer = laufendeNummer;
        Belegnummer = belegnummer;
        Belegdatum = belegdatum;
        Buchungsdatum = buchungsdatum;
        Buchungstext = buchungstext;
    }

    public string? Id { get; set; }
    public string LaufendeNummer { get; set; }
    public string Belegnummer { get; set; }
    public DateOnly Belegdatum { get; set; }
    public DateOnly Buchungsdatum { get; set; }
    public string? Buchungstext { get; set; }
    public List<Kontierungszeile> Kontierungszeilen { get; } = new();


    public void KontierungszeileHinzufuegen(Kontierungszeile kontierungszeile)
    {
        Kontierungszeilen.Add(kontierungszeile);
    }

    public static Vorkontierung Erstellen(string laufendeNummer, string belegnummer, DateOnly buchungsdatum, DateOnly belegdatum, string? buchungstext)
    {
        return new Vorkontierung(laufendeNummer, belegnummer, buchungsdatum, belegdatum, buchungstext);
    }
}