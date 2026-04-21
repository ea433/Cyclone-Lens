using Microsoft.SqlServer.Types;

namespace Data_Access_Layer.DTOs
{
    public class MetadataDTO
    {
        public int Id { get; set; }
        public int CycloonId { get; set; }
        public int Categorie { get; set; }
        public double Windsnelheid { get; set; }
        public double Luchtdruk { get; set; }
        public required SqlGeography Coordinaten { get; set; }
        public DateTime Tijdstip { get; set; }
    }
}
