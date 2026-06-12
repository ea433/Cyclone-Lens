using Business_Logic_Layer.Services;
using CycloneLensTestProject.FakeRepositories;
using Interface_Layer.DTOs;
using Microsoft.SqlServer.Types;
using Models.Classes;
using Models.Enums;

namespace CycloneLensTestProject.ServiceTests
{
    public class CycloonServiceUnitTest
    {
        private readonly FakeCycloonRepository _cycloonRepo;
        private readonly FakeMetadataRepository _metadataRepo;
        private readonly FakeLoggingRepository _loggingRepo;
        private readonly CycloonService _service;

        public CycloonServiceUnitTest()
        {
            _cycloonRepo = new FakeCycloonRepository();
            _metadataRepo = new FakeMetadataRepository();
            _loggingRepo = new FakeLoggingRepository();
            _service = new CycloonService(_cycloonRepo, _metadataRepo, _loggingRepo);
        }

        // UC03 - Happy path
        [Fact]
        public void GetActiveCyclonenNATL_ReturnsOnlyActiveNorthAtlanticCyclones()
        {
            _cycloonRepo.Cyclonen = new List<CycloonDTO>
            {
                new CycloonDTO { Id = 1, Naam = "Maria", Status = "Actief", Bassin = "Noord-Atlantisch" },
                new CycloonDTO { Id = 2, Naam = "Jose", Status = "Inactief", Bassin = "Noord-Atlantisch" },
                new CycloonDTO { Id = 3, Naam = "Lee", Status = "Actief", Bassin = "Stille-Oceaan" }
            };

            var result = _service.GetActiveCyclonenNATL();

            Assert.Single(result);
            Assert.Equal("Maria", result[0].Naam);
        }

        [Fact]
        public void GetActiveCyclonenNATL_UsesLatestMetadata()
        {
            _cycloonRepo.Cyclonen = new List<CycloonDTO>
            {
                new CycloonDTO { Id = 1, Naam = "Maria", Status = "Actief", Bassin = "Noord-Atlantisch" }
            };
            _metadataRepo.Metadata = new List<MetadataDTO>
            {
                new MetadataDTO { CycloonId = 1, Categorie = (int)CategorieType.Categorie_1, Tijdstip = new DateTime(2024, 1, 1), Coordinaten = SqlGeography.Point(0, 0, 4326) },
                new MetadataDTO { CycloonId = 1, Categorie = (int)CategorieType.Categorie_5, Tijdstip = new DateTime(2024, 2, 1), Coordinaten = SqlGeography.Point(0, 0, 4326) }
            };

            var result = _service.GetActiveCyclonenNATL();

            Assert.Equal(CategorieType.Categorie_5, result[0].Categorie);
        }

        [Fact]
        public void GetActiveCyclonenNATL_UsesDefaultCategory_WhenNoMetadataExists()
        {
            _cycloonRepo.Cyclonen = new List<CycloonDTO>
            {
                new CycloonDTO { Id = 1, Naam = "Maria", Status = "Actief", Bassin = "Noord-Atlantisch" }
            };
            _metadataRepo.Metadata = new List<MetadataDTO>();

            var result = _service.GetActiveCyclonenNATL();

            Assert.Equal(CategorieType.Tropische_Depressie, result[0].Categorie);
        }

        // UC04 - Happy path
        [Fact]
        public void GetCycloonDetails_ReturnsMetadata()
        {
            _cycloonRepo.CycloonById = new CycloonDTO { Id = 1, Naam = "Maria", Status = "Actief", Bassin = "Noord-Atlantisch" };
            _metadataRepo.Metadata = new List<MetadataDTO>
            {
                new MetadataDTO { Id = 1, CycloonId = 1, Categorie = (int)CategorieType.Categorie_3, Windsnelheid = 200, Luchtdruk = 950, Tijdstip = new DateTime(2024, 1, 1), Coordinaten = SqlGeography.Point(0, 0, 4326) }
            };

            var result = _service.GetCycloonDetails(1);

            Assert.NotNull(result);
            Assert.Single(result!.Metadata);
            Assert.Equal(CategorieType.Categorie_3, result.Categorie);
        }

        // UC04 - Exception [1]: cycloon bestaat niet
        [Fact]
        public void GetCycloonDetails_ReturnsNull_WhenCycloonDoesNotExist()
        {
            _cycloonRepo.CycloonById = null;

            var result = _service.GetCycloonDetails(1);

            Assert.Null(result);
        }

        // UC05 - Happy path
        [Fact]
        public void UpdateCycloon_Succeeds_WhenBeheerderAndValidData()
        {
            _cycloonRepo.CycloonById = new CycloonDTO { Id = 1, Naam = "Maria", Status = "Actief", Bassin = "Noord-Atlantisch" };

            var cycloon = new Cycloon(1, "Maria", StatusType.Actief, BassinType.Noord_Atlantisch);
            var gebruiker = new Gebruiker(1, "Admin", "Wachtwoord1", GebruikerType.Beheerder);

            var exception = Record.Exception(() => _service.UpdateCycloon(cycloon, null!, gebruiker));

            Assert.Null(exception);
        }

        // UC05 - No uitzondering but UpdateCycloon has validation so we test it
        [Fact]
        public void UpdateCycloon_ThrowsException_WhenGebruikerIsNotBeheerder()
        {
            var cycloon = new Cycloon(1, "Maria", StatusType.Actief, BassinType.Noord_Atlantisch);
            var gebruiker = new Gebruiker(1, "Test", "Wachtwoord1", GebruikerType.Gebruiker);

            Assert.Throws<UnauthorizedAccessException>(() =>
                _service.UpdateCycloon(cycloon, null!, gebruiker));
        }

        [Fact]
        public void UpdateCycloon_ThrowsException_WhenNaamIsEmpty()
        {
            var cycloon = new Cycloon(1, "", StatusType.Actief, BassinType.Noord_Atlantisch);
            var gebruiker = new Gebruiker(1, "Admin", "Wachtwoord1", GebruikerType.Beheerder);
            
            Assert.Throws<ArgumentException>(() =>
                _service.UpdateCycloon(cycloon, null!, gebruiker));
        }
    }
}