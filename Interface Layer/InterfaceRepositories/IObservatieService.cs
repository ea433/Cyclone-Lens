using Microsoft.SqlServer.Types;

namespace Interface_Layer.InterfaceRepositories
{
    public interface IObservatieService
    {
        void PlaatsObservatie(int gebruikerId, int cycloonId, string omschrijving, SqlGeography coordinaten);
    }
}
