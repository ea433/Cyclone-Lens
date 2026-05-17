using Microsoft.SqlServer.Types;

namespace Models.Classes
{
    public class Observatie
    {
        public int Id { get; }
        public int GebruikerId { get; }
        public string GebruikerNaam { get; }
        public int CycloonId { get; }
        public string Omschrijving { get; private set; } // alleen set wanneer object geinitialiseerd wordt, laat required toe (validatie)
        public string? AfbeeldingPad { get; private set; }
        public SqlGeography? Coordinaten { get; private set; }
        public DateTime Tijdstip { get; private set; }

        public Observatie(int id, int gebruikerId, string gebruikerNaam, int cycloonId, string omschrijving, string? afbeeldingPad, 
        SqlGeography coordinaten, DateTime tijdstip)
        {
            Id = id;
            GebruikerId = gebruikerId;
            GebruikerNaam = gebruikerNaam;
            CycloonId = cycloonId;
            Omschrijving = omschrijving ?? throw new Exception(nameof(omschrijving));
            AfbeeldingPad = afbeeldingPad; 
            Coordinaten = coordinaten ?? throw new Exception(nameof(coordinaten));
            Tijdstip = tijdstip;
        }
    }
}