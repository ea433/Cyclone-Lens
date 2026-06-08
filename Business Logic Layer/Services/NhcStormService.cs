using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Models.Classes;
using Models.Enums;

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
                    Bassin = BassinTypeParser.BassinToString(BassinTypeParser.ParseNhcBassin(storm.Id)),
                    Windsnelheid = int.Parse(storm.Intensity),
                    Luchtdruk = int.Parse(storm.Pressure),
                    Latitude = storm.LatitudeNumeric,
                    Longitude = storm.LongitudeNumeric,
                    Tijdstip = storm.LastUpdate
                };

                _repository.Insert(dto);
            }
        }
    }
}