using Interface_Layer.DTOs;
using Logic.Enums;
using CycloneLensTestProject.FakeRepositories;

// onafgemaakt 

namespace CycloneLensTestProject.ServiceTests
{
    public class CycloonServiceUnitTest
    {
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

