using Business_Logic_Layer.Mappers;
using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Models.Classes;
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
            List<CycloonDTO> cyclonen = _repository.GetCyclonen();
            List<MetadataDTO> metadata = _dataRepository.GetMetadata();

            return cyclonen
                .Where(cycloon =>
                    cycloon.Status == StatusType.Actief.ToString() &&
                    cycloon.Bassin.Replace("-", "_") == BassinType.Noord_Atlantisch.ToString())
                .Select(cycloon =>
                {
                    MetadataDTO? latest = metadata
                        .Where(metadata => metadata.CycloonId == cycloon.Id)
                        .OrderByDescending(metadata => metadata.Tijdstip)
                        .FirstOrDefault();

                    return new Cycloon(
                        cycloon.Id,
                        cycloon.Naam,
                        CycloonMapper.ParseCategorie(latest?.Categorie ?? (int)CategorieType.Tropische_Depressie),
                        CycloonMapper.ParseBassin(cycloon.Bassin),
                        CycloonMapper.ParseStatus(cycloon.Status)
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

            CycloonDTO cycloonDto = new CycloonDTO
            {
                Id = cycloon.Id,
                Naam = cycloon.Naam,
                Status = CycloonMapper.StatusToString(cycloon.Status),
                Bassin = CycloonMapper.BassinToString(cycloon.Bassin)
            };

            _repository.UpdateCycloon(cycloonDto);

            if (metadata != null)
            {
                MetadataDTO metadataDto = new MetadataDTO
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
            CycloonDTO? dto = _repository.GetById(id);

            if (dto == null)
                return null;

            return new Cycloon(
                dto.Id,
                dto.Naam,
                CycloonMapper.ParseStatus(dto.Status),
                CycloonMapper.ParseBassin(dto.Bassin)
            );
        }

        public Cycloon? GetCycloonDetails(int id)
        {
            CycloonDTO? cycloonDetails = _repository.GetById(id);

            if (cycloonDetails == null)
                return null;

            List<Metadata> metadata = _dataRepository.GetMetadata()
                .Where(metadata => metadata.CycloonId == id)
                .OrderBy(metadata => metadata.Tijdstip)
                .Select(metadata => new Metadata(metadata.Id, metadata.CycloonId, CycloonMapper.ParseCategorie(metadata.Categorie), metadata.Windsnelheid,
                    metadata.Luchtdruk, metadata.Coordinaten, metadata.Tijdstip))
                .ToList();

            CategorieType latestCategorie = metadata.LastOrDefault()?.Categorie
                ?? CategorieType.Tropische_Depressie;

            return new Cycloon(
                cycloonDetails.Id,
                cycloonDetails.Naam,
                latestCategorie,
                CycloonMapper.ParseStatus(cycloonDetails.Status),
                CycloonMapper.ParseBassin(cycloonDetails.Bassin),
                metadata
            );
        }
    }
}