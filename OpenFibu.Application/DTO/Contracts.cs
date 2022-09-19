namespace OpenFibu.Application.DTO
{
    public record GeschaeftsvorfallDto
    {
        public GeschaeftsvorfallDto() { }
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
        public BuchungDto() { }
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
}
