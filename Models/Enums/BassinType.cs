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
        public static BassinType ParseNhcBassin(string nhcId)
        {
            if (nhcId.StartsWith("al"))
                return BassinType.Noord_Atlantisch;
            else if (nhcId.StartsWith("cp"))
                return BassinType.Centraal_Pacifisch;
            else
                return BassinType.Oost_Pacifisch;
        }
    }
}
