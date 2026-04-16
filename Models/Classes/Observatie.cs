using System;
using System.Collections.Generic;
using Microsoft.SqlServer.Types;
using System.Text;

namespace Models.Classes
{
    public class Observatie
    {
        public int Id { get; }
        public string Omschrijving { get; private set; }
        public string AfbeeldingPad { get; private set; }
        public SqlGeography Coordinaten { get; private set; }
        public DateTime Tijdstip { get; private set; }

        public Observatie(int id, string omschrijving, string afbeeldingPad,
            SqlGeography coordinaten, DateTime tijdstip)
        {
            this.Id = Id;
            this.Omschrijving = omschrijving ?? throw new Exception(nameof(omschrijving));
            this.AfbeeldingPad = afbeeldingPad ?? throw new Exception(nameof(afbeeldingPad));
            this.Coordinaten = coordinaten ?? throw new Exception(nameof(coordinaten));
            this.Tijdstip = tijdstip;
        }
    }
}
