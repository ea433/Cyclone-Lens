using CycloneLens.Models;

namespace Interface_Layer.InterfaceRepositories
{
    public interface ICycloonRepository
    {
        List<Cycloon> GetCyclonen();
        void UpdateCycloon(Cycloon cycloon);
        void LogWijziging(int cycloonId, string actie, int gebruikerId);
        Cycloon? GetById(int id);
    }
}