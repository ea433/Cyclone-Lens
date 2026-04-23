using Data_Access_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;

namespace CycloneLensTestProject.FakeRepositories
{
    public class FakeCycloonRepository : ICycloonRepository
    {
        public CycloonDTO? CycloonById { get; set; }
        public List<CycloonDTO> Cyclonen { get; set; } = new();
        public CycloonDTO? UpdatedCycloon { get; private set; }

        public CycloonDTO? GetById(int id)
        {
            return CycloonById;
        }

        public List<CycloonDTO> GetCyclonen()
        {
            return Cyclonen;
        }

        public void UpdateCycloon(CycloonDTO cycloon)
        {
            UpdatedCycloon = cycloon;
        }
    }
}