using Interface_Layer.InterfaceRepositories;
using Microsoft.SqlServer.Types;
using Models.Classes;

namespace Business_Logic_Layer.Services
{
    public class ObservatieService : IObservatieService
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
            SqlGeography coordinaten)
        {
            if (gebruikerId <= 0)
                throw new Exception("Gebruiker is verplicht");

            if (string.IsNullOrWhiteSpace(omschrijving))
                throw new Exception("Omschrijving is verplicht");

            if (coordinaten == null)
                throw new Exception("Locatie is verplicht");

            var observatie = new Observatie(
                0,
                gebruikerId,
                cycloonId,
                omschrijving,
                null,
                coordinaten,
                DateTime.Now
            );

            _dal.InsertObservatie(observatie);
        }
    }
}