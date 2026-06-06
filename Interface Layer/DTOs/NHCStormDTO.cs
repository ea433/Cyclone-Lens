namespace Interface_Layer.DTOs
{
    public class NhcStormDTO
    {
        public int Id { get; set; }
        public required string NhcId { get; set; }
        public required string Naam { get; set; }
        public required string Categorie { get; set; }
        public required string Bassin { get; set; }
        public int Windsnelheid { get; set; }
        public int Luchtdruk { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public DateTime Tijdstip { get; set; }
    }
}