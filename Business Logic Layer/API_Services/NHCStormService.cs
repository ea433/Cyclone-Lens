using Interface_Layer.API_DTOs;
using Interface_Layer.API_InterfaceRepositories;
using Models.Classes;
using Business_Logic_Layer.Mappers;

namespace Business_Logic_Layer.API_Services
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

        // Om evt nog later op website te tonen
        public List<NhcStormDTO> GetAll()
        {
            return _repository.GetAll();
        }
    }
}