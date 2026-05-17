using Interface_Layer.DTOs;
using Models.Classes;

namespace Interface_Layer.InterfaceRepositories
{
    public interface IObservatieRepository
    {
        void InsertObservatie(ObservatieDTO observatie);
        List<Observatie> GetAllObservaties();
        Observatie? GetById(int id);
        void DeleteObservatie(int id);
    }
}
