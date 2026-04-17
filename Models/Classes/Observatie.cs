using System;
using Microsoft.SqlServer.Types;

namespace Models.Classes
{
    public class Observatie
    {
        public int Id { get; }
        public int GebruikerId { get; }
        public int CycloonId { get; }
        public string Omschrijving { get; private set; }
        public string? AfbeeldingPad { get; private set; }
        public SqlGeography Coordinaten { get; private set; }
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