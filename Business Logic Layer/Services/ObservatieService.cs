using Data_Access_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Microsoft.SqlServer.Types;

namespace Business_Logic_Layer.Services
{
    public class ObservatieService
    {
        private readonly IObservatieRepository _dal;

        public ObservatieService(IObservatieRepository dal)
        {
            _dal = dal;
        }

        public void PlaatsObservatie(
            int gebruikerId,
            int cycloonId,
            string omschrijving,
            SqlGeography? coordinaten)
        {
            if (gebruikerId <= 0)
                throw new Exception("Gebruiker is verplicht");

            if (cycloonId <= 0)
                throw new Exception("Cycloon is verplicht");

            if (string.IsNullOrWhiteSpace(omschrijving))
                throw new Exception("Omschrijving is verplicht");

            if (coordinaten == null)
                throw new Exception("Locatie is verplicht");

            var observatie = new ObservatieDTO
            {
                GebruikerId = gebruikerId,
                CycloonId = cycloonId,
                Omschrijving = omschrijving,
                AfbeeldingPad = null,
                Coordinaten = coordinaten,
                Tijdstip = DateTime.Now
            };

            _dal.InsertObservatie(observatie);
        }
    }
}