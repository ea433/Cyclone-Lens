using CycloneLens.Models;
using Data_Access_Layer.DTOs;


namespace Interface_Layer.InterfaceRepositories
{
    public interface ICycloonRepository
    {
        List<CycloonDTO> GetCyclonen();
        void UpdateCycloon(CycloonDTO cycloon);
        CycloonDTO? GetById(int id);
    }
}