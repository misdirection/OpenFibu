using OpenFibu.Domain.Shared.Enums;
using OpenFibu.Domain.Stammdaten.Entities;
using OpenFibu.Shared;

namespace OpenFibu.Domain.Vorkontierung.Entities;

public class Kontierungszeile : Entity
{
    private Kontierungszeile(decimal betrag, string kontoId, SollHaben sollHaben, Steuerschluessel steuerschluessel)
    {
        Betrag = betrag;
        KontoId = kontoId;
        SollHaben = sollHaben;
        Steuerschluessel = steuerschluessel;
    }

    public string? Id { get; set; }
    public decimal Betrag { get; set; }
    public string KontoId { get; set; }
    public SollHaben SollHaben { get; set; }
    public Steuerschluessel Steuerschluessel { get; set; }

    public static Kontierungszeile Erstellen(decimal betrag, string kontoId, SollHaben sollHaben,
        Steuerschluessel steuerschluessel)
    {
        return new Kontierungszeile(betrag, kontoId, sollHaben, steuerschluessel);
    }
}