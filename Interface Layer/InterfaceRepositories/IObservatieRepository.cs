using Microsoft.SqlServer.Types;
using Models.Classes;

namespace Interface_Layer.InterfaceRepositories
{
    public interface IObservatieRepository
    {
        void InsertObservatie(Observatie observatie);
    }
}
