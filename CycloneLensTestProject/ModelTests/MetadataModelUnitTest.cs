using Models.Classes;
using Models.Enums;
using Microsoft.SqlServer.Types;

namespace CycloneLensTestProject.ModelTests
{
    public class MetadataTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int id = 1;
            int cycloonId = 10;
            CategorieType categorie = CategorieType.Categorie_1;
            double windsnelheid = 120.5;
            double luchtdruk = 980.2;
            SqlGeography coordinaten = SqlGeography.Point(51.4545, 3.5700, 4326);
            DateTime tijdstip = new DateTime(2024, 10, 1, 12, 30, 0);

            // Act
            var metadata = new Metadata(id, cycloonId, categorie, windsnelheid, luchtdruk, coordinaten, tijdstip);

            // Assert
            Assert.Equal(id, metadata.Id);
            Assert.Equal(cycloonId, metadata.Cycloon_Id);
            Assert.Equal(categorie, metadata.Categorie);
            Assert.Equal(windsnelheid, metadata.Windsnelheid);
            Assert.Equal(luchtdruk, metadata.Luchtdruk);
            Assert.Equal(coordinaten, metadata.Coordinaten);
            Assert.Equal(tijdstip, metadata.Tijdstip);
        }
    }
}