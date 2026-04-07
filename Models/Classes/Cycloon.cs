using Logic.Enums;

namespace CycloneLens.Models
{
    public class Cycloon
    {
        public int Id { get; }
        public string Naam { get; private set; }
        public StatusType Status { get; private set; }
        public BassinType Bassin { get; private set; }
        public List<CycloonData> Metadata { get; set; } = new();

        public Cycloon(int id, string naam, StatusType status, BassinType bassin)
        {
            this.Id = id;
            this.Naam = naam ?? throw new ArgumentNullException(nameof(naam));
            this.Status = status;
            this.Bassin = bassin;
        }
    }
}
    
