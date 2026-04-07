using CycloneLens.Models;
using Models.Classes;

namespace CycloneLens.Interfaces
{
    public interface ICycloonRepository
    {
        List<Cycloon> GetCyclonen();
        List<CycloonData> GetMetadata();
        void UpdateCycloon(Cycloon cycloon);
        void AddMetadata(CycloonData metadata);
        void LogWijziging(int cycloonId, string actie, int gebruikerId);
        Cycloon? GetById(int id);
    }
}