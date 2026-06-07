using Interface_Layer.DTOs;
using Models.Classes;
using Models.Enums;

namespace Business_Logic_Layer.Mappers
{
    public static class CycloonMapper
    {
        // Parse: String -> Enum
        public static StatusType ParseStatus(string status) => Enum.Parse<StatusType>(status);

        public static BassinType ParseBassin(string bassin) => Enum.Parse<BassinType>(bassin.Replace("-", "_"));

        public static CategorieType ParseCategorie(int categorie) => (CategorieType)categorie;

        // ToString: Enum -> String
        public static string StatusToString(StatusType status) => status.ToString();
        public static string BassinToString(BassinType bassin) => bassin.ToString().Replace("_", "-");

        // DTO -> Domeinmodel
        public static Cycloon ToDomain(CycloonDTO dto)
        {
            return new Cycloon(
                dto.Id,
                dto.Naam,
                ParseStatus(dto.Status),
                ParseBassin(dto.Bassin)
            );
        }
    }
}
