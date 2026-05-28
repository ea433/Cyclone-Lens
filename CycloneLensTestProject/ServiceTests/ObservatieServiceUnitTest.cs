using Business_Logic_Layer.Services;
using CycloneLensTestProject.FakeRepositories;
using Microsoft.SqlServer.Types;
using Models.Classes;

namespace CycloneLensTestProject.ServiceTests
{
    public class ObservatieServiceUnitTest
    {
        [Fact]
        public void PlaatsObservatie_ThrowsException_WhenGebruikerIdIsInvalid()
        {
            // Arrange
            var service = new ObservatieService(
                new FakeObservatieRepository());

            // Act & Assert
            var exception = Assert.Throws<Exception>(() =>
                service.PlaatsObservatie(
                    0,
                    1,
                    "Test observatie",
                    SqlGeography.Point(10, 10, 4326)));

            Assert.Equal("Gebruiker is verplicht", exception.Message);
        }

        [Fact]
        public void PlaatsObservatie_ThrowsException_WhenCycloonIdIsInvalid()
        {
            // Arrange
            var service = new ObservatieService(
                new FakeObservatieRepository());

            // Act & Assert
            var exception = Assert.Throws<Exception>(() =>
                service.PlaatsObservatie(
                    1,
                    0,
                    "Test observatie",
                    SqlGeography.Point(10, 10, 4326)));

            Assert.Equal("Cycloon is verplicht", exception.Message);
        }
    }
}