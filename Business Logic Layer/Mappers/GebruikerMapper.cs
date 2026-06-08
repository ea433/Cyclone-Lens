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

        public static GebruikerDTO ToDTO(string gebruikersnaam, string wachtwoordHash)
        {
            return new GebruikerDTO
            {
                Gebruikersnaam = gebruikersnaam,
                WachtwoordHash = wachtwoordHash,
                GebruikerType = (int)GebruikerType.Gebruiker
            };
        }
    }
}
