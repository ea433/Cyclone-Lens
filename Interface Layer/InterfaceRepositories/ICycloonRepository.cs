using CycloneLens.Models;
using Interface_Layer.DTOs;


namespace Interface_Layer.InterfaceRepositories
{
    public interface ICycloonRepository
    {
        List<CycloonDTO> GetCyclonen();
        void UpdateCycloon(CycloonDTO cycloon);
        CycloonDTO? GetById(int id);
    }
}