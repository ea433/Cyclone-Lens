using Interface_Layer.DTOs;
using Models.Classes;
using Models.Enums;

namespace Business_Logic_Layer.Mappers
{
    public static class CycloonMapper
    {
        // DTO -> Domeinmodel
        public static Cycloon ToDomain(CycloonDTO dto)
        {
            return new Cycloon(dto.Id, dto.Naam, StatusTypeParser.ParseStatus(dto.Status), BassinTypeParser.ParseBassin(dto.Bassin));
        }

        public static CycloonDTO ToDTO(Cycloon cycloon)
        {
            return new CycloonDTO
            {
                Id = cycloon.Id,
                Naam = cycloon.Naam,
                Status = StatusTypeParser.StatusToString(cycloon.Status),
                Bassin = BassinTypeParser.BassinToString(cycloon.Bassin)
            };
        }

        public static MetadataDTO ToDTO(Metadata metadata)
        {
            return new MetadataDTO
            {
                Id = metadata.Id,
                CycloonId = metadata.Cycloon_Id,
                Categorie = (int)metadata.Categorie,
                Windsnelheid = metadata.Windsnelheid,
                Luchtdruk = metadata.Luchtdruk,
                Coordinaten = metadata.Coordinaten,
                Tijdstip = metadata.Tijdstip
            };
        }
    }
}
