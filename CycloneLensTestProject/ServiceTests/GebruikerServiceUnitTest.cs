using Business_Logic_Layer.Services;
using CycloneLensTestProject.FakeRepositories;
using Interface_Layer.DTOs;
using Models.Enums;

namespace CycloneLensTestProject.ServiceTests
{
    public class GebruikerServiceUnitTest
    {
        private readonly FakeGebruikerRepository _gebruikerRepo;
        private readonly GebruikerService _service;

        public GebruikerServiceUnitTest()
        {
            _gebruikerRepo = new FakeGebruikerRepository();
            _service = new GebruikerService(_gebruikerRepo);
        }

        // UC01 - Happy path
        [Fact]
        public void RegistrerenGebruiker_Slaagt_MetJuisteData()
        {
            _service.RegistreerGebruiker("TestUser", "Wachtwoord1");

            Assert.Single(_gebruikerRepo._gebruikers);
        }

        // UC01 - Exception [1]: verplichte velden niet ingevuld
        [Fact]
        public void RegistrerenGebruiker_GooitExceptie_WanneerGebruikersNaamIsNull()
        {
            Assert.Throws<ArgumentException>(() =>
                _service.RegistreerGebruiker("", "Wachtwoord1"));
        }

        [Fact]
        public void RegistrerenGebruiker_GooitExceptie_WanneerWachtwoordIsNull()
        {
            Assert.Throws<ArgumentException>(() =>
                _service.RegistreerGebruiker("TestUser", ""));
        }

        // UC01 - Exception [2]: wachtwoord voldoet niet aan vereisten
        [Fact]
        public void RegistrerenGebruiker_GooitExceptie_WanneerWachtwoordTeKort()
        {
            Assert.Throws<ArgumentException>(() =>
                _service.RegistreerGebruiker("TestUser", "Ab1"));
        }

        [Fact]
        public void RegistrerenGebruiker_GooitExceptie_WanneerWachtwoordBevatGeenHoofdletter()
        {
            Assert.Throws<ArgumentException>(() =>
                _service.RegistreerGebruiker("TestUser", "wachtwoord1"));
        }

        [Fact]
        public void RegistrerenGebruiker_GooitExceptieWanneerWachtwoordBevatGeenCijfer()
        {
            Assert.Throws<ArgumentException>(() =>
                _service.RegistreerGebruiker("TestUser", "Wachtwoord"));
        }

        // UC01 - Exception [3]: gebruikersnaam al in gebruik
        [Fact]
        public void RegistrerenGebruiker_GooitExceptie_WannnerGebruikersnaamAlBestaat()
        {
            _gebruikerRepo._gebruikers.Add(new GebruikerDTO
            {
                Gebruikersnaam = "TestUser",
                WachtwoordHash = BCrypt.Net.BCrypt.HashPassword("Wachtwoord1"),
                GebruikerType = 0
            });

            Assert.Throws<ArgumentException>(() =>
                _service.RegistreerGebruiker("TestUser", "Wachtwoord1"));
        }

        // UC02 - Happy path
        [Fact]
        public void Inloggen_GeeftGebruiker_WanneerInloggegevensCorrect()
        {
            _gebruikerRepo._gebruikers.Add(new GebruikerDTO
            {
                Id = 1,
                Gebruikersnaam = "TestUser",
                WachtwoordHash = BCrypt.Net.BCrypt.HashPassword("Wachtwoord1"),
                GebruikerType = 0
            });

            var result = _service.Login("TestUser", "Wachtwoord1");

            Assert.NotNull(result);
            Assert.Equal("TestUser", result!.Naam);
            Assert.Equal(GebruikerType.Gebruiker, result.UserType);
        }

        // UC02 - Exception [1]: gegevens horen niet bij bestaande account
        [Fact]
        public void Inloggen_GeeftNull_WanneerGebruikerNietBestaat()
        {
            var result = _service.Login("TestUser", "Wachtwoord1");

            Assert.Null(result);
        }

        [Fact]
        public void Inloggen_GeeftNull_WanneerWachtwoordIncorrect()
        {
            _gebruikerRepo._gebruikers.Add(new GebruikerDTO
            {
                Id = 1,
                Gebruikersnaam = "TestUser",
                WachtwoordHash = BCrypt.Net.BCrypt.HashPassword("Wachtwoord1"),
                GebruikerType = 0
            });

            var result = _service.Login("TestUser", "VerkeerWachtwoord1");

            Assert.Null(result);
        }
    }
}