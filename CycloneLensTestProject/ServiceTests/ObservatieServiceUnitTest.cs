using Business_Logic_Layer.Services;
using CycloneLensTestProject.FakeRepositories;
using Microsoft.SqlServer.Types;

namespace CycloneLensTestProject.ServiceTests
{
    public class ObservatieServiceUnitTest
    {
        private readonly FakeObservatieRepository _observatieRepo;
        private readonly ObservatieService _service;

        public ObservatieServiceUnitTest()
        {
            _observatieRepo = new FakeObservatieRepository();
            _service = new ObservatieService(_observatieRepo);
        }

        // UC07 - Happy path
        [Fact]
        public void PlaatsObservatie_Succeeds_WhenValidData()
        {
            var coordinaten = SqlGeography.Point(10, 10, 4326);

            var exception = Record.Exception(() =>
                _service.PlaatsObservatie(1, 1, "Testomschrijving", coordinaten));

            Assert.Null(exception);
        }

        // UC07 - Exception [1]: verplichte velden niet ingevuld
        [Fact]
        public void PlaatsObservatie_ThrowsException_WhenOmschrijvingIsEmpty()
        {
            var coordinaten = SqlGeography.Point(10, 10, 4326);

            Assert.Throws<ArgumentException>(() =>
                _service.PlaatsObservatie(1, 1, "", coordinaten));
        }

        [Fact]
        public void PlaatsObservatie_ThrowsException_WhenGebruikerIdIsInvalid()
        {
            var coordinaten = SqlGeography.Point(10, 10, 4326);

            Assert.Throws<ArgumentException>(() =>
                _service.PlaatsObservatie(0, 1, "Testomschrijving", coordinaten));
        }

        [Fact]
        public void PlaatsObservatie_ThrowsException_WhenCycloonIdIsInvalid()
        {
            var coordinaten = SqlGeography.Point(10, 10, 4326);

            Assert.Throws<ArgumentException>(() =>
                _service.PlaatsObservatie(1, 0, "Testomschrijving", coordinaten));
        }

        // UC07 - Exception [2]: ongeldige locatie
        [Fact]
        public void PlaatsObservatie_ThrowsException_WhenCoordinatenIsNull()
        {
            Assert.Throws<ArgumentException>(() =>
                _service.PlaatsObservatie(1, 1, "Testomschrijving", null));
        }

        [Fact]
        public void PlaatsObservatie_ThrowsException_WhenCoordinatenOutOfRange()
        {
            var coordinaten = SqlGeography.Point(91, 181, 4326);

            Assert.Throws<ArgumentException>(() =>
                _service.PlaatsObservatie(1, 1, "Testomschrijving", coordinaten));
        }

        // UC09 - Happy path
        [Fact]
        public void GetAllObservaties_ReturnsAllObservaties()
        {
            var result = _service.GetAllObservaties();

            Assert.NotNull(result);
        }

        // UC10 - Happy path
        [Fact]
        public void DeleteObservatie_Succeeds_WhenObservatieExists()
        {
            var exception = Record.Exception(() =>
                _service.DeleteObservatie(1));

            Assert.Null(exception);
        }

        // UC11 - Happy path
        [Fact]
        public void RapporteerObservatie_Succeeds_WhenNotYetReported()
        {
            var exception = Record.Exception(() =>
                _service.RapporteerObservatie(1, 1));

            Assert.Null(exception);
        }

        // UC11 - Exception [1]: al gerapporteerd
        [Fact]
        public void RapporteerObservatie_ThrowsException_WhenAlreadyReported()
        {
            _observatieRepo.AlGerapporteerd = true;

            Assert.Throws<InvalidOperationException>(() =>
                _service.RapporteerObservatie(1, 1));
        }
    }
}