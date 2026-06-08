namespace Models.Enums
{
    public enum StatusType
    {
        Actief,
        Inactief
    }

    public static class StatusTypeParser
    {
        public static StatusType ParseStatus(string status) => Enum.Parse<StatusType>(status);
        public static string StatusToString(StatusType status) => status.ToString();

    }
}

