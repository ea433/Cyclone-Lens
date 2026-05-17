namespace Presentation.Models
{
    public class ObservatieViewModel
    {
        public int Id { get; set; } 
        public int CycloonId { get; set; }
        public string? Omschrijving { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? Inzender { get; set; }  
        public DateTime Datum { get; set; }  
    }
}
