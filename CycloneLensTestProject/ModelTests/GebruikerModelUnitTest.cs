namespace CycloneLensTestProject.ModelTests
{
    using System;
    using Xunit;
    using CycloneLens.Models;
    using Models.Enums;

    namespace CycloneLensTesting.Models
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
                UserType userType = UserType.Beheerder; // adjust if needed

                // Act
                var gebruiker = new Gebruiker(id, naam, email, wachtwoord, userType);

                // Assert
                Assert.Equal(id, gebruiker.Id);
                Assert.Equal(naam, gebruiker.Naam);
                Assert.Equal(email, gebruiker.Email);
                Assert.Equal(wachtwoord, gebruiker.Wachtwoord);
                Assert.Equal(userType, gebruiker.UserType);
            }

            [Fact]
            public void Constructor_Throws_WhenNaamIsNull()
            {
                var ex = Assert.Throws<ArgumentNullException>(() =>
                    new Gebruiker(1, null!, "mail@test.com", "pass", UserType.Beheerder));

                Assert.Equal("naam", ex.ParamName);
            }

            [Fact]
            public void Constructor_Throws_WhenEmailIsNull()
            {
                var ex = Assert.Throws<ArgumentNullException>(() =>
                    new Gebruiker(1, "Leander", null!, "pass", UserType.Beheerder));

                Assert.Equal("email", ex.ParamName);
            }

            [Fact]
            public void Constructor_Throws_WhenWachtwoordIsNull()
            {
                var ex = Assert.Throws<ArgumentNullException>(() =>
                    new Gebruiker(1, "Leander", "mail@test.com", null!, UserType.Beheerder));

                Assert.Equal("wachtwoord", ex.ParamName);
            }
        }
    }
}
