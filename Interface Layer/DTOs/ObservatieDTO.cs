using Microsoft.SqlServer.Types;

namespace Interface_Layer.DTOs
{
    public class ObservatieDTO
    {
        public int GebruikerId { get; set; }
        public int CycloonId { get; set; }
        public required string Omschrijving { get; set; }
        public string? AfbeeldingPad { get; set; }
        public DateTime Tijdstip { get; set; }
        public required SqlGeography Coordinaten { get; set; }
    }
}
