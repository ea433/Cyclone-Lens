using CycloneLens.Models;

namespace CycloneLens.Services
{
    public class CycloonService
    {
        public List<Cycloon> GetActieveCyclonenNATL()
        {
            var json = File.ReadAllText("Data/cyclonen.json");
            Console.WriteLine(json);

            return new List<Cycloon>();
        }
        // + GetActieveCyclonenNATL(): List<Cycloon> 
        // GetMetadata(cycloon: Cycloon) : List<Metadata>
    }
}

