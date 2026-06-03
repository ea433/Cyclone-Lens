using Models.Enums;

namespace Business_Logic_Layer.Mappers
{
    public static class CycloonMapper
    {
        // Alle methodes zijn static zodat je ze kan aanroepen zonder een instantie van CycloonMapper te hoefen te maken
        
        // Enum -> String
        public static StatusType ParseStatus(string status) => Enum.Parse<StatusType>(status);

        public static BassinType ParseBassin(string bassin) => Enum.Parse<BassinType>(bassin.Replace("-", "_"));

        public static CategorieType ParseCategorie(int categorie) => (CategorieType)categorie;

        // String -> Enum
        public static string StatusToString(StatusType status) => status.ToString();
        public static string BassinToString(BassinType bassin) => bassin.ToString().Replace("_", "-");
    }
}
