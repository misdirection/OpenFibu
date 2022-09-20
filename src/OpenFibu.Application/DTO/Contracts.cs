using OpenFibu.Domain.Shared.Enums;

namespace OpenFibu.Application.DTO;

public record GeschaeftsvorfallDto
{
    public GeschaeftsvorfallDto()
    {
    }

    public GeschaeftsvorfallDto(string? id, string belegnummer, DateOnly buchungsdatum, DateOnly belegdatum, List<BuchungDto> buchungen)
    {
        Id = id;
        Belegnummer = belegnummer;
        Buchungsdatum = buchungsdatum;
        Belegdatum = belegdatum;
        Buchungen = buchungen;
    }

    public string? Id { get; init; }
    public string Belegnummer { get; init; }
    public DateOnly Buchungsdatum { get; init; }
    public DateOnly Belegdatum { get; init; }
    public IEnumerable<BuchungDto> Buchungen { get; init; }
}

public record BuchungDto
{
    public BuchungDto()
    {
    }

    public BuchungDto(string? id, uint? sollkonto, uint? kontonummer, decimal betrag)
    {
        Id = id;
        Sollkonto = sollkonto;
        Habenkonto = kontonummer;
        Betrag = betrag;
    }

    public string? Id { get; set; }
    public uint? Sollkonto { get; set; }
    public uint? Habenkonto { get; set; }
    public decimal Betrag { get; set; }
}

public record SteuerschluesselDto(string? Id, string Bezeichnung, decimal Steuersatz, uint Steuerkonto);

public record VorkontierungsDto
{
    public VorkontierungsDto()
    {
    }

    public VorkontierungsDto(string? id, string laufendeNummer, string belegnummer, DateOnly buchungsdatum,
        DateOnly belegdatum, List<KontierungszeilenDto> kontierungszeilen)
    {
        Id = id;
        LaufendeNummer = laufendeNummer;
        Belegnummer = belegnummer;
        Buchungsdatum = buchungsdatum;
        Belegdatum = belegdatum;
        Kontierungszeilen = kontierungszeilen;
    }

    public string? Id { get; init; }
    public string LaufendeNummer { get; init; }
    public string Belegnummer { get; init; }
    public DateOnly Buchungsdatum { get; init; }
    public DateOnly Belegdatum { get; init; }
    public IEnumerable<KontierungszeilenDto> Kontierungszeilen { get; init; }
}

public record KontierungszeilenDto
{
    public KontierungszeilenDto()
    {
    }

    public KontierungszeilenDto(string? id, string kontoId, string steuerschluesselId, decimal betrag)
    {
        Id = id;
        KontoId = kontoId;
        SteuerschluesselId = steuerschluesselId;
        Betrag = betrag;
    }

    public string? Id { get; init; }
    public string? KontoId { get; init; }
    public string SteuerschluesselId { get; init; }
    public SollHaben SollHaben { get; init; }
    public decimal Betrag { get; init; }
}