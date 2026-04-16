using CycloneLens.Models;

namespace Interface_Layer.InterfaceRepositories
{
    public interface ICycloonRepository
    {
        List<Cycloon> GetCyclonen();
        void UpdateCycloon(Cycloon cycloon);
        Cycloon? GetById(int id);
    }
}