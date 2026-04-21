using Microsoft.SqlServer.Types;
using Models.Classes;
using Data_Access_Layer.DTOs;

namespace Interface_Layer.InterfaceRepositories
{
    public interface IObservatieRepository
    {
        void InsertObservatie(ObservatieDTO observatie);
    }
}
