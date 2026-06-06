using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Models.Classes;

namespace Business_Logic_Layer.Services
{
    public class NhcStormService
    {
        private readonly INhcStormRepository _repository;

        public NhcStormService(INhcStormRepository repository)
        {
            _repository = repository;
        }

        public void SyncFromNhc(List<NhcStorm> storms)
        {
            foreach (NhcStorm storm in storms)
            {
                NhcStormDTO dto = new()
                {
                    NhcId = storm.Id,
                    Naam = storm.Name,
                    Categorie = storm.Classification,
                    Bassin = ParseBassin(storm.Id),
                    Windsnelheid = int.Parse(storm.Intensity),
                    Luchtdruk = int.Parse(storm.Pressure),
                    Latitude = storm.LatitudeNumeric,
                    Longitude = storm.LongitudeNumeric,
                    Tijdstip = storm.LastUpdate
                };

                _repository.Insert(dto);
            }
        }

        public List<NhcStormDTO> GetAll()
        {
            return _repository.GetAll();
        }

        private string ParseBassin(string nhcId)
        {
            return nhcId.StartsWith("al") ? "Noord-Atlantisch" : "Oost-Pacifisch";
        }
    }
}