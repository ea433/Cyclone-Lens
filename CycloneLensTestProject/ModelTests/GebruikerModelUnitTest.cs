using Models.Classes;
using Models.Enums;

namespace CycloneLensTestProject.ModelTests
{
    public class GebruikerTests
    {
        [Fact]
        public void Constructor_SetsPropertiesCorrectly()
        {
            // Arrange
            int id = 1;
            string naam = "Leander";
            string email = "test@mail.com";
            string wachtwoord = "password123";
            GebruikerType userType = GebruikerType.Beheerder; // adjust if needed

            // Act
            var gebruiker = new Gebruiker(id, naam, wachtwoord, userType);

            // Assert
            Assert.Equal(id, gebruiker.Id);
            Assert.Equal(naam, gebruiker.Naam);
            Assert.Equal(wachtwoord, gebruiker.Wachtwoord);
            Assert.Equal(userType, gebruiker.UserType);
        }

        [Fact]
        public void Constructor_Throws_WhenNaamIsNull()
        {
            // Act
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new Gebruiker(1, null!, "pass", GebruikerType.Beheerder));

            // Assert
            Assert.Equal("naam", ex.ParamName);
        }

        [Fact]
        public void Constructor_Throws_WhenEmailIsNull()
        {
            // Act
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new Gebruiker(1, "Leander", "pass", GebruikerType.Beheerder));

            // Assert
            Assert.Equal("email", ex.ParamName);
        }

        [Fact]
        public void Constructor_Throws_WhenWachtwoordIsNull()
        {
            // Act
            var ex = Assert.Throws<ArgumentNullException>(() =>
                new Gebruiker(1, "Leander", null!, GebruikerType.Beheerder));

            // Assert
            Assert.Equal("wachtwoord", ex.ParamName);
        }
    }
}
