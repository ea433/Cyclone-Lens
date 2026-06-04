using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Microsoft.SqlServer.Types;
using Models.Classes;

namespace Business_Logic_Layer.Services
{
    public class ObservatieService
    {
        private readonly IObservatieRepository _observatieRepository;

        public ObservatieService(IObservatieRepository observatieRepository)
        {
            _observatieRepository = observatieRepository;
        }

        public void PlaatsObservatie(int gebruikerId, int cycloonId, string omschrijving, SqlGeography? coordinaten)
        {
            if (gebruikerId <= 0)
                throw new Exception("Gebruiker is verplicht");

            if (cycloonId <= 0)
                throw new Exception("Cycloon is verplicht");

            if (string.IsNullOrWhiteSpace(omschrijving))
                throw new Exception("Omschrijving is verplicht");

            if (coordinaten == null)
                throw new Exception("Locatie is verplicht");

            if (coordinaten.Lat.Value == 0 || coordinaten.Long.Value == 0)
                throw new Exception("Ongeldige locatie");

            if (coordinaten.Lat.Value > 90 || coordinaten.Long.Value > 180)
                throw new Exception("Ongeldige locatie");

            ObservatieDTO observatie = new()
            {
                GebruikerId = gebruikerId,
                CycloonId = cycloonId,
                Omschrijving = omschrijving,
                AfbeeldingPad = null,
                Coordinaten = coordinaten,
                Tijdstip = DateTime.Now
            };

            _observatieRepository.InsertObservatie(observatie);
        }

        public List<Observatie> GetAllObservaties()
        {
            return _observatieRepository.GetAllObservaties();
        }

        public Observatie? GetById(int id)
        {
            return _observatieRepository.GetById(id);
        }

        public void DeleteObservatie(int id)
        {
            _observatieRepository.DeleteObservatie(id);
        }

        public void RapporteerObservatie(int gebruikerId, int observatieId)
        {
            try
            {
                _observatieRepository.VoegRapportageToe(gebruikerId, observatieId);
            }
            catch
            {
                throw new Exception("Je hebt deze observatie al gerapporteerd.");
            }
        }
    }
}