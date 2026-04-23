using CycloneLens.Models;
using Data_Access_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Logic.Enums;
using Models.Classes;
using Models.Enums;

public class CycloonService
{
    private readonly ICycloonRepository _repository;
    private readonly IMetadataRepository _dataRepository;
    private readonly ILoggingRepository _loggingRepository;

    public CycloonService(
        ICycloonRepository repository,
        IMetadataRepository datarepository,
        ILoggingRepository loggingrepository)
    {
        _repository = repository;
        _dataRepository = datarepository;
        _loggingRepository = loggingrepository;
    }

    public List<CycloonOverzicht> GetActiveCyclonenNATL()
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

                return new CycloonOverzicht(
                    cycloon.Id,
                    cycloon.Naam,
                    (CategorieType)(latest?.Categorie ?? (int)CategorieType.Tropische_Depressie),
                    Enum.Parse<BassinType>(cycloon.Bassin.Replace("-", "_")),
                    Enum.Parse<StatusType>(cycloon.Status)
                );
            })
            .ToList();
    }

    // fr-05 + logging
    public void UpdateCycloon(Cycloon cycloon, Metadata? metadata, Gebruiker gebruiker)
    {
        if (gebruiker == null || gebruiker.UserType != UserType.Beheerder)
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

    public (Cycloon cycloon, List<MetadataDTO> traject)? GetCycloonDetails(int id)
    {
        var cycloonDto = _repository.GetById(id);
        if (cycloonDto == null)
            return null;

        var cycloon = new Cycloon(
            cycloonDto.Id,
            cycloonDto.Naam,
            Enum.Parse<StatusType>(cycloonDto.Status),
            Enum.Parse<BassinType>(cycloonDto.Bassin.Replace("-", "_"))
        );

        var traject = _dataRepository.GetMetadata()
            .Where(m => m.CycloonId == id)
            .OrderBy(m => m.Tijdstip)
            .ToList();

        return (cycloon, traject);
    }
}