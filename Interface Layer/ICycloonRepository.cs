using CycloneLens.Models;
using Models.Classes;

namespace CycloneLens.Interfaces
{
    public interface ICycloonRepository
    {
        List<Cycloon> GetCyclonen();
        List<Metadata> GetMetadata();
        void UpdateCycloon(Cycloon cycloon);
        void AddMetadata(Metadata metadata);
        void LogWijziging(int cycloonId, string actie, int gebruikerId);
        Cycloon? GetById(int id);
    }
}