using CycloneLens.Models;

namespace CycloneLens.Interfaces
{
    public interface ICycloonRepository
    {
        List<Cycloon> GetCyclonen();
        List<Metadata> GetMetadata();
    }
}