using CycloneLens.Models;

namespace CycloneLens.Interfaces
{
    public interface ICycloonRepository
    {
        List<Cycloon> GetCyclonen();
        List<Metadata> GetMetadata();
        void UpdateCycloon(Cycloon cycloon);
        void AddMetadata(Metadata metadata);
    }
}