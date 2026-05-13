using Microsoft.SqlServer.Types;

namespace Models.Classes
{
    public class Observatie
    {
        public int Id { get; }
        public int GebruikerId { get; }
        public int CycloonId { get; }
        public required string Omschrijving { get; init; } // alleen set wanneer object geinitialiseerd wordt, laat required toe (validatie)
        public string? AfbeeldingPad { get; private set; }
        public required SqlGeography Coordinaten { get; init; }
        public DateTime Tijdstip { get; private set; }

        public Observatie(int id, int gebruikerId, int cycloonId, string omschrijving, string? afbeeldingPad, 
        SqlGeography coordinaten, DateTime tijdstip)
        {
            Id = id;
            GebruikerId = gebruikerId;
            CycloonId = cycloonId;
            Omschrijving = omschrijving ?? throw new Exception(nameof(omschrijving));
            AfbeeldingPad = afbeeldingPad; 
            Coordinaten = coordinaten ?? throw new Exception(nameof(coordinaten));
            Tijdstip = tijdstip;
        }
    }
}