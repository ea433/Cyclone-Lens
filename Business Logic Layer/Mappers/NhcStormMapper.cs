using Interface_Layer.DTOs;
using Models.Classes;
using Models.Enums;

namespace Business_Logic_Layer.Mappers
{
    public static class NhcStormMapper
    {
        public static NhcStormDTO ToDTO(NhcStorm storm)
        {
            return new NhcStormDTO
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
        }
    }
}