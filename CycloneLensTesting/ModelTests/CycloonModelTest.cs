using System;
using Xunit;
using CycloneLens.Models;
using Logic.Enums;

namespace CycloneLens.Tests.Models
{
    public class CycloonModelTest
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int id = 1;
            string naam = "Orkaan Alex";
            StatusType status = StatusType.Actief;
            BassinType bassin = BassinType.Atlantisch;

            // Act
            var cycloon = new Cycloon(id, naam, status, bassin);

            // Assert
            Assert.Equal(id, cycloon.Id);
            Assert.Equal(naam, cycloon.Naam);
            Assert.Equal(status, cycloon.Status);
            Assert.Equal(bassin, cycloon.Bassin);
        }

        [Fact]
        public void Constructor_ThrowsArgumentNullException_WhenNaamIsNull()
        {
            // Arrange
            string naam = null;

            // Act & Assert
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new Cycloon(1, naam, StatusType.Actief, BassinType.Atlantisch));

            Assert.Equal("naam", ex.ParamName);
        }

        [Fact]
        public void Constructor_InitializesMetadataAsEmptyList()
        {
            // Act
            var cycloon = new Cycloon(1, "Orkaan Alex", StatusType.Actief, BassinType.Atlantisch);

            // Assert
            Assert.NotNull(cycloon.Metadata);
            Assert.Empty(cycloon.Metadata);
        }
    }
}