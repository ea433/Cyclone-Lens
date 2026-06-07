using Interface_Layer.DTOs;
using Models.Classes;
using Models.Enums;

namespace Business_Logic_Layer.Mappers
{
    public class GebruikerMapper
    {
        public static Gebruiker ToDomain(GebruikerDTO dto)
        {
            return new Gebruiker(dto.Id, dto.Gebruikersnaam, dto.WachtwoordHash, (GebruikerType)dto.GebruikerType);
        }
    }
}
