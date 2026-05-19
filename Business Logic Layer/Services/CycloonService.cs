using Models.Classes;
using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Models.Enums;

namespace Business_Logic_Layer.Services
{
    public class CycloonService
    {
        private readonly ICycloonRepository _repository;
        private readonly IMetadataRepository _dataRepository;
        private readonly ILoggingRepository _loggingRepository;

        public CycloonService(ICycloonRepository repository, IMetadataRepository datarepository, ILoggingRepository loggingrepository)
        {
            _repository = repository;
            _dataRepository = datarepository;
            _loggingRepository = loggingrepository;
        }

        public List<Cycloon> GetActiveCyclonenNATL()
        {
            var cyclonen = _repository.GetCyclonen();
            var metadata = _dataRepository.GetMetadata();

            return cyclonen
                .Where(cycloon =>
                    cycloon.Status == StatusType.Actief.ToString() &&
                    cycloon.Bassin.Replace("-", "_") == BassinType.Noord_Atlantisch.ToString())
                .Select(cycloon =>
                {
                    var latest = metadata
                        .Where(metadata => metadata.CycloonId == cycloon.Id)
                        .OrderByDescending(metadata => metadata.Tijdstip)
                        .FirstOrDefault();

                    return new Cycloon(
                        cycloon.Id,
                        cycloon.Naam,
                        (CategorieType)(latest?.Categorie ?? (int)CategorieType.Tropische_Depressie),
                        Enum.Parse<BassinType>(cycloon.Bassin.Replace("-", "_")),
                        Enum.Parse<StatusType>(cycloon.Status)
                    );
                })
                .ToList();
        }

        public void UpdateCycloon(Cycloon cycloon, Metadata metadata, Gebruiker gebruiker)
        {
            if (gebruiker == null || gebruiker.UserType != GebruikerType.Beheerder)
                throw new UnauthorizedAccessException("Geen rechten");

            if (string.IsNullOrWhiteSpace(cycloon.Naam))
                throw new ArgumentException("Naam is verplicht");

            var cycloonDto = new CycloonDTO
            {
                Id = cycloon.Id,
                Naam = cycloon.Naam,
                Status = cycloon.Status.ToString(),
                Bassin = cycloon.Bassin.ToString().Replace("_", "-")
            };

            _repository.UpdateCycloon(cycloonDto);

            if (metadata != null)
            {
                var metadataDto = new MetadataDTO
                {
                    Id = metadata.Id,
                    CycloonId = metadata.Cycloon_Id,
                    Categorie = (int)metadata.Categorie,
                    Windsnelheid = metadata.Windsnelheid,
                    Luchtdruk = metadata.Luchtdruk,
                    Coordinaten = metadata.Coordinaten,
                    Tijdstip = metadata.Tijdstip
                };

                _dataRepository.AddMetadata(metadataDto);
            }

            _loggingRepository.LogWijziging(
                cycloon.Id,
                "Cycloon bijgewerkt",
                gebruiker.Id
            );
        }

        public Cycloon? GetById(int id)
        {
            var dto = _repository.GetById(id);

            if (dto == null)
                return null;

            return new Cycloon(
                dto.Id,
                dto.Naam,
                Enum.Parse<StatusType>(dto.Status),
                Enum.Parse<BassinType>(dto.Bassin.Replace("-", "_"))
            );
        }

        public Cycloon? GetCycloonDetails(int id)
        {
            var cycloonDetails = _repository.GetById(id);

            if (cycloonDetails == null)
                return null;

            var metadata = _dataRepository.GetMetadata()
                .Where(metadata => metadata.CycloonId == id)
                .OrderBy(metadata => metadata.Tijdstip)
                .Select(metadata => new Metadata(metadata.Id, metadata.CycloonId, (CategorieType)metadata.Categorie, metadata.Windsnelheid,
                    metadata.Luchtdruk, metadata.Coordinaten, metadata.Tijdstip))
                .ToList();

            var latestCategorie = metadata.LastOrDefault()?.Categorie
                ?? CategorieType.Tropische_Depressie;

            return new Cycloon(
                cycloonDetails.Id,
                cycloonDetails.Naam,
                latestCategorie,
                Enum.Parse<StatusType>(cycloonDetails.Status),
                Enum.Parse<BassinType>(cycloonDetails.Bassin.Replace("-", "_")),
                metadata
            );
        }
    }
}