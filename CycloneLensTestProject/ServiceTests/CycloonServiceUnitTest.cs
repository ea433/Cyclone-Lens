using CycloneLensTestProject.FakeRepositories;
using Interface_Layer.DTOs;
using Models.Enums;
using Microsoft.SqlServer.Types;
using Business_Logic_Layer.Services;

// onafgemaakt 

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
    }
}

