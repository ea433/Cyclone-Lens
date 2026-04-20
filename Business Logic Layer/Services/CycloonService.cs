using CycloneLens.Models;
using Interface_Layer.InterfaceRepositories;
using Logic.Enums;
using Models.Classes;

public class CycloonService
{
    private readonly ICycloonRepository _repository;
    private readonly ICycloonDataRepository _dataRepository;
    private readonly ILoggingRepository _loggingRepository;

    public CycloonService(ICycloonRepository repository, ICycloonDataRepository datarepository,
        ILoggingRepository loggingrepository)
    {
        _repository = repository;
        _dataRepository = datarepository;
        _loggingRepository = loggingrepository;
    }

    public List<CycloonOverzichtNATL> GetActiveCyclonenNATL()
    {
        var cyclonen = _repository.GetCyclonen();
        var metadata = _dataRepository.GetMetadata();

        return cyclonen
            .Where(cycloon => cycloon.Status == StatusType.Actief &&
                        cycloon.Bassin == BassinType.Noord_Atlantisch)
            .Select(cycloon =>
            {
                var latest = metadata
                    .Where(metadata => metadata.Cycloon_Id == cycloon.Id)
                    .OrderByDescending(metadata => metadata.Tijdstip)
                    .FirstOrDefault();

                return new CycloonOverzichtNATL(
                cycloon.Id,
                cycloon.Naam,
                latest?.Categorie ?? CategorieType.Tropische_Depressie,
                cycloon.Bassin,
                cycloon.Status
                );
            })
            .ToList();
    }

    // fr-05 + logging
    public void UpdateCycloon(Cycloon cycloon, CycloonData? metadata, Gebruiker gebruiker)
    {
        if (gebruiker == null || !gebruiker.BeheerRechten)
            throw new UnauthorizedAccessException("Geen rechten");

        if (string.IsNullOrWhiteSpace(cycloon.Naam))
            throw new ArgumentException("Naam is verplicht");

        _repository.UpdateCycloon(cycloon);

        if (metadata != null)
        {
            _dataRepository.AddMetadata(metadata);
        }

        _loggingRepository.LogWijziging(
            cycloon.Id,
            "Cycloon bijgewerkt",
            gebruiker.Id
        );
    }

    public Cycloon? GetById(int id)
    {
        return _repository.GetById(id);
    }
}