using Models.Classes;
using Microsoft.SqlServer.Types;
using Models.Enums;

namespace CycloneLensTestProject.ModelTests
{
    public class RapportageModelUnitTest
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Assert
            int id = 1;
            DateTime tijdstip = new DateTime(2024, 10, 1, 12, 0, 0);
            var gebruiker = new Gebruiker(1, "Jan Honing", "jan@email.com", "wachtwoord123", GebruikerType.Gebruiker);
            var observatie = new Observatie(1, 1, "Jan Honing", 1, "desc", null, SqlGeography.Point(51.45, 3.57, 4326), new DateTime(2024, 10, 1, 12, 0, 0));

            // Act
            var rapportage = new Rapportage(id, tijdstip, gebruiker, observatie);

            // Assert
            Assert.Equal(id, rapportage.Id);
            Assert.Equal(tijdstip, rapportage.Tijdstip);
            Assert.Equal(gebruiker, rapportage.Gebruiker);
            Assert.Equal(observatie, rapportage.Observatie);
        }
    }
}