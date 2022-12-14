using OpenFibu.Shared;

namespace OpenFibu.Domain.Stammdaten.Entities;

public class Steuerschluessel : Entity, IAggregateRoot
{
    public Steuerschluessel()
    {
    }

    private Steuerschluessel(string bezeichnung, uint steuerkonto, decimal steuersatz)
    {
        Bezeichnung = bezeichnung;
        Steuerkonto = steuerkonto;
        Steuersatz = steuersatz;
    }

    public string? Id { get; set; }
    public string Bezeichnung { get; private set; }
    public uint Steuerkonto { get; private set; }
    public decimal Steuersatz { get; private set; }


    public static Steuerschluessel Erstellen(string bezeichnung, uint steuerkonto, decimal steuersatz)
        => new(bezeichnung, steuerkonto, steuersatz);
}