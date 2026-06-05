using System;
using System.Collections.Generic;
using System.Text;

namespace Interface_Layer.DTOs
{
    public class GebruikerDTO
    {
        public int Id { get; set; }
        public required string Gebruikersnaam { get; set; }
        public required string WachtwoordHash { get; set; }
        public int GebruikerType { get; set; }
    }
}
