using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Models.Classes;

namespace CycloneLensTestProject.FakeRepositories
{
    public class FakeObservatieRepository : IObservatieRepository
    {
        private readonly List<Observatie> _observaties = new();

        private int _nextId = 1;

        public void InsertObservatie(ObservatieDTO observatieDto)
        {
            Observatie observatie = new Observatie(
                _nextId++,
                observatieDto.GebruikerId,
                "FakeGebruiker",
                observatieDto.CycloonId,
                observatieDto.Omschrijving,
                observatieDto.AfbeeldingPad,
                observatieDto.Coordinaten,
                observatieDto.Tijdstip
            );

            _observaties.Add(observatie);
        }

        public List<Observatie> GetAllObservaties()
        {
            return _observaties;
        }

        public Observatie? GetById(int id)
        {
            return _observaties.FirstOrDefault(o => o.Id == id);
        }

        public void DeleteObservatie(int id)
        {
            Observatie? observatie = GetById(id);

            if (observatie != null)
            {
                _observaties.Remove(observatie);
            }
        }

        public bool AlGerapporteerd { get; set; }

        public void VoegRapportageToe(int gebruikerId, int observatieId)
        {
            if (AlGerapporteerd)
                throw new Exception("Al gerapporteerd");

            Observatie? observatie = GetById(observatieId) ?? throw new Exception("Observatie bestaat niet.");
        }
    }
}
