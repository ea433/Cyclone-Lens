namespace Models.Enums
{
    public enum BassinType
    {
        Noord_Atlantisch,
        Oost_Pacifisch,
        Centraal_Pacifisch
    }

    public static class BassinTypeParser
    {
        public static BassinType ParseBassin(string bassin) => Enum.Parse<BassinType>(bassin.Replace("-", "_"));
        public static string BassinToString(BassinType bassin) => bassin.ToString().Replace("_", "-");
    }
}
