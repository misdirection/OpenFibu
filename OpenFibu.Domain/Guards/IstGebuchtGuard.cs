using Ardalis.GuardClauses;
using OpenFibu.Domain.Entities.Journal;

namespace OpenFibu.Domain.Guards;

public static class IstGebuchtGuard
{
    public static void IstGebucht(this IGuardClause guardClause, Geschaeftsvorfall geschaeftsvorfall)
    {
        if (geschaeftsvorfall.IstGebucht)
            throw new Exception();
    }
}