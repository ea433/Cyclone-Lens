using Models.Classes;
using Microsoft.SqlServer.Types;

namespace CycloneLensTestProject.ModelTests
{
    public class ObservatieTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int id = 1;
            int gebruikerId = 5;
            string gebruikerNaam = "Gianna Beenen";
            int cycloonId = 10;
            string omschrijving = "Sterke wind waargenomen";
            string? afbeeldingPad = "image.jpg";
            SqlGeography coordinaten = SqlGeography.Point(51.45, 3.57, 4326);
            DateTime tijdstip = new DateTime(2024, 10, 1, 12, 0, 0);

            // Act
            var observatie = new Observatie(
                id,
                gebruikerId,
                gebruikerNaam,
                cycloonId,
                omschrijving,
                afbeeldingPad,
                coordinaten,
                tijdstip);

            // Assert
            Assert.Equal(id, observatie.Id);
            Assert.Equal(gebruikerId, observatie.GebruikerId);
            Assert.Equal(cycloonId, observatie.CycloonId);
            Assert.Equal(omschrijving, observatie.Omschrijving);
            Assert.Equal(afbeeldingPad, observatie.AfbeeldingPad);
            Assert.Equal(coordinaten, observatie.Coordinaten);
            Assert.Equal(tijdstip, observatie.Tijdstip);
        }

        [Fact]
        public void Constructor_AllowsNullAfbeeldingPad()
        {
            // Arrange
            SqlGeography coordinaten = SqlGeography.Point(51.45, 3.57, 4326);

            // Act
            var observatie = new Observatie(
                1,
                5,
                "Gianna Beenen",
                10,
                "Test",
                null,
                coordinaten,
                DateTime.Now);

            // Assert
            Assert.Null(observatie.AfbeeldingPad);
        }

        [Fact]
        // Act (new observatie) en Assert
        public void Constructor_Throws_WhenOmschrijvingIsNull()
        {
            var ex = Assert.Throws<Exception>(() =>
                new Observatie(
                    1,
                    5,
                    "Jan Honing",
                    10,
                    null!,
                    "img.jpg",
                    SqlGeography.Point(51.45, 3.57, 4326),
                    DateTime.Now));

            Assert.Equal("omschrijving", ex.Message);
        }

        [Fact]
        // Act (new observatie) en Assert
        public void Constructor_Throws_WhenCoordinatenIsNull()
        {
            var ex = Assert.Throws<Exception>(() =>
                new Observatie(
                    1,
                    5,
                    "Jan Honing",
                    10,
                    "Test",
                    "img.jpg",
                    null!,
                    DateTime.Now));

            Assert.Equal("coordinaten", ex.Message);
        }
    }
}
