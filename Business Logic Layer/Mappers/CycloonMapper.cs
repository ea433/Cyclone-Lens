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
    }
}
