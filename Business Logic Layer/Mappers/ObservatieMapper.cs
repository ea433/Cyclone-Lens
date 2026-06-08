using Interface_Layer.DTOs;
using Microsoft.SqlServer.Types;

namespace Business_Logic_Layer.Mappers
{
    public static class ObservatieMapper
    {
        public static ObservatieDTO ToDTO(int gebruikerId, int cycloonId, string omschrijving, SqlGeography? coordinaten)
        {
            return new ObservatieDTO
            {
                GebruikerId = gebruikerId,
                CycloonId = cycloonId,
                Omschrijving = omschrijving,
                AfbeeldingPad = null,
                Coordinaten = coordinaten!,
                Tijdstip = DateTime.Now
            };
        }
    }
}