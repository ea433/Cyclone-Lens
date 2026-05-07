namespace Interface_Layer.InterfaceRepositories
{ 
    public interface ILoggingRepository
    {
        void LogWijziging(int cycloonId, string actie, int gebruikerId);
    }
}
