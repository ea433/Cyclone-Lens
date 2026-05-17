using Models.Classes;
using Models.Enums;

namespace CycloneLensTestProject.ModelTests
{
    public class CycloonTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int id = 1;
            string naam = "Maria";
            StatusType status = StatusType.Actief;
            BassinType bassin = BassinType.Noord_Atlantisch;

            // Act
            var cycloon = new Cycloon(id, naam, status, bassin);

            // Assert
            Assert.Equal(id, cycloon.Id);
            Assert.Equal(naam, cycloon.Naam);
            Assert.Equal(status, cycloon.Status);
            Assert.Equal(bassin, cycloon.Bassin);
        }

        [Fact]
        public void Constructor_Throws_WhenNaamIsNull()
        {
            // Act
            var exception = Assert.Throws<ArgumentNullException>(() =>
                new Cycloon(1, null!, StatusType.Actief, BassinType.Noord_Atlantisch));

            // Assert
            Assert.Equal("naam", exception.ParamName);
        }

        [Fact]
        public void Metadata_IsInitialized_AsEmptyList()
        {
            // Act
            var cycloon = new Cycloon(1, "Alex", StatusType.Actief, BassinType.Noord_Atlantisch);

            // Assert
            Assert.NotNull(cycloon.Metadata);
            Assert.Empty(cycloon.Metadata);
        }
    }
}