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
        [Fact]
        public void GetActiveCyclonenNATL_ReturnsOnlyActiveNorthAtlanticCyclones()
        {
            // Arrange
            var cycloonRepo = new FakeCycloonRepository
            {
                Cyclonen = new List<CycloonDTO>
        {
            new CycloonDTO
            {
                Id = 1,
                Naam = "Maria",
                Status = "Actief",
                Bassin = "Noord-Atlantisch"
            },
            new CycloonDTO
            {
                Id = 2,
                Naam = "Jose",
                Status = "Inactief",
                Bassin = "Noord-Atlantisch"
            },
            new CycloonDTO
            {
                Id = 3,
                Naam = "Lee",
                Status = "Actief",
                Bassin = "Stille-Oceaan"
            }
        }
            };

            var metadataRepo = new FakeMetadataRepository
            {
                Metadata = new List<MetadataDTO>()
            };

            var loggingRepo = new FakeLoggingRepository();

            var service = new CycloonService(cycloonRepo, metadataRepo, loggingRepo);

            // Act
            var result = service.GetActiveCyclonenNATL();

            // Assert
            Assert.Single(result);
            Assert.Equal("Maria", result[0].Naam);
        }

        [Fact]
        public void GetActiveCyclonenNATL_UsesLatestMetadata()
        {
            // Arrange
            var cycloonRepo = new FakeCycloonRepository
            {
                Cyclonen = new List<CycloonDTO>
        {
            new CycloonDTO
            {
                Id = 1,
                Naam = "Maria",
                Status = "Actief",
                Bassin = "Noord-Atlantisch"
            }
        }
            };

            var metadataRepo = new FakeMetadataRepository
            {
                Metadata = new List<MetadataDTO>
        {
            new MetadataDTO
            {
                CycloonId = 1,
                Categorie = (int)CategorieType.Categorie_1,
                Tijdstip = new DateTime(2024,1,1),
                Coordinaten = SqlGeography.Point(0, 0, 4326)
            },

            new MetadataDTO
            {
                CycloonId = 1,
                Categorie = (int)CategorieType.Categorie_5,
                Tijdstip = new DateTime(2024,2,1),
                Coordinaten = SqlGeography.Point(0, 0, 4326)
            }
        }
            };

            var service = new CycloonService(
                cycloonRepo,
                metadataRepo,
                new FakeLoggingRepository());

            // Act
            var result = service.GetActiveCyclonenNATL();

            // Assert
            Assert.Equal(CategorieType.Categorie_5, result[0].Categorie);
        }
        
        [Fact]
        public void GetById_ReturnsMappedCycloon_WhenDtoExists()
        {
            // Arrange
            var cycloonRepo = new FakeCycloonRepository
            {
                CycloonById = new CycloonDTO
                {
                    Id = 1,
                    Naam = "Maria",
                    Status = "Actief",
                    Bassin = "Noord-Atlantisch"
                }
            };

            var metadataRepo = new FakeMetadataRepository();
            var loggingRepo = new FakeLoggingRepository();

            var service = new CycloonService(cycloonRepo, metadataRepo, loggingRepo);

            // Act
            var result = service.GetById(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result!.Id);
            Assert.Equal("Maria", result.Naam);
            Assert.Equal(StatusType.Actief, result.Status);
            Assert.Equal(BassinType.Noord_Atlantisch, result.Bassin);
        }

        [Fact]
        public void GetById_ReturnsNull_WhenCycloonDoesNotExist()
        {
            // Arrange
            var cycloonRepo = new FakeCycloonRepository
            {
                CycloonById = null
            };

            var service = new CycloonService(
                cycloonRepo,
                new FakeMetadataRepository(),
                new FakeLoggingRepository());

            // Act
            var result = service.GetById(999);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetActiveCyclonenNATL_UsesDefaultCategory_WhenNoMetadataExists()
        {
            // Arrange
            var cycloonRepo = new FakeCycloonRepository
            {
                Cyclonen = new List<CycloonDTO>
        {
            new CycloonDTO
            {
                Id = 1,
                Naam = "Maria",
                Status = "Actief",
                Bassin = "Noord-Atlantisch"
            }
        }
            };

            var metadataRepo = new FakeMetadataRepository
            {
                Metadata = new List<MetadataDTO>()
            };

            var service = new CycloonService(
                cycloonRepo,
                metadataRepo,
                new FakeLoggingRepository());

            // Act
            var result = service.GetActiveCyclonenNATL();

            // Assert
            Assert.Equal(
                CategorieType.Tropische_Depressie,
                result[0].Categorie);
        }

        [Fact]
        public void UpdateCycloon_ThrowsException_WhenGebruikerIsNotAdmin()
        {
            // Arrange
            var service = new CycloonService(
                new FakeCycloonRepository(),
                new FakeMetadataRepository(),
                new FakeLoggingRepository());

            var cycloon = new Cycloon(
                1,
                "Maria",
                StatusType.Actief,
                BassinType.Noord_Atlantisch);

            var gebruiker = new Gebruiker(
                1,
                "Test",
                "wachtwoord123",
                GebruikerType.Gebruiker);

            // Act & Assert
            Assert.Throws<UnauthorizedAccessException>(() =>
                service.UpdateCycloon(cycloon, null!, gebruiker));
        }

        [Fact]
        public void UpdateCycloon_ThrowsException_WhenNaamIsEmpty()
        {
            // Arrange
            var service = new CycloonService(
                new FakeCycloonRepository(),
                new FakeMetadataRepository(),
                new FakeLoggingRepository());

            var cycloon = new Cycloon(
                1,
                "",
                StatusType.Actief,
                BassinType.Noord_Atlantisch);

            var gebruiker = new Gebruiker(
                1,
                "Admin",
                "wachtwoord123",
                GebruikerType.Beheerder);

            // Act & Assert
            Assert.Throws<ArgumentException>(() =>
            service.UpdateCycloon(cycloon, null!, gebruiker));
        }

        [Fact]
        public void GetCycloonDetails_ReturnsNull_WhenCycloonDoesNotExist()
        {
            // Arrange
            var cycloonRepo = new FakeCycloonRepository
            {
                CycloonById = null
            };

            var service = new CycloonService(
                cycloonRepo,
                new FakeMetadataRepository(),
                new FakeLoggingRepository());

            // Act
            var result = service.GetCycloonDetails(1);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public void GetCycloonDetails_ReturnsMetadata()
        {
            // Arrange
            var cycloonRepo = new FakeCycloonRepository
            {
                CycloonById = new CycloonDTO
                {
                    Id = 1,
                    Naam = "Maria",
                    Status = "Actief",
                    Bassin = "Noord-Atlantisch"
                }
            };

            var metadataRepo = new FakeMetadataRepository
            {
                Metadata = new List<MetadataDTO>
        {
            new MetadataDTO
            {
                Id = 1,
                CycloonId = 1,
                Categorie = (int)CategorieType.Categorie_3,
                Windsnelheid = 200,
                Luchtdruk = 950,
                Tijdstip = new DateTime(2024, 1, 1),
                Coordinaten = SqlGeography.Point(0,0,4326)
            }
        }
            };

            var service = new CycloonService(
                cycloonRepo,
                metadataRepo,
                new FakeLoggingRepository());

            // Act
            var result = service.GetCycloonDetails(1);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result!.Metadata);
            Assert.Equal(CategorieType.Categorie_3, result.Categorie);
        }
    }
}

