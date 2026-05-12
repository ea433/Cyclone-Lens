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
            RapportageStatusType status = RapportageStatusType.Open;
            DateTime tijdstip = new DateTime(2024, 10, 1, 12, 0, 0);
            var gebruiker = new Gebruiker(1, "Jan Jansen", "jan@email.com", "wachtwoord123", UserType.Gebruiker);
            var observatie = new Observatie(1, 1, 1, "desc", null, SqlGeography.Point(51.45, 3.57, 4326), new DateTime(2024, 10, 1, 12, 0, 0));

            // Act
            var rapportage = new Rapportage(id, status, tijdstip, gebruiker, observatie);

            // Assert
            Assert.Equal(id, rapportage.Id);
            Assert.Equal(status, rapportage.Status);
            Assert.Equal(tijdstip, rapportage.Tijdstip);
            Assert.Equal(gebruiker, rapportage.Gebruiker);
            Assert.Equal(observatie, rapportage.Observatie);
        }
    }
}