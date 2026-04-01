using CycloneLens.Interfaces;
using CycloneLens.Models;
using Logic.Enums;

public class CycloonService
{
    private readonly ICycloonRepository _repository;

    public CycloonService(ICycloonRepository repository)
    {
        _repository = repository;
    }

    public List<ViewModel> GetActiveCyclonenNATL()
    {
        var cyclonen = _repository.GetCyclonen();
        var metadata = _repository.GetMetadata();

        return cyclonen
            .Where(cycloon => cycloon.Status == StatusType.Actief &&
                        cycloon.Bassin == BassinType.Noord_Atlantisch)
            .Select(cycloon =>
            {
                var latest = metadata
                    .Where(metadata => metadata.Cycloon_Id == cycloon.Id)
                    .OrderByDescending(metadata => metadata.Tijdstip)
                    .FirstOrDefault();

                return new ViewModel(
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
    public void UpdateCycloon(Cycloon cycloon, Metadata metadata, Gebruiker gebruiker)
    {
        if (gebruiker == null || !gebruiker.BeheerRechten)
            throw new Exception("Geen rechten");

        if (string.IsNullOrWhiteSpace(cycloon.Naam))
            throw new Exception("Naam is verplicht");

        _repository.UpdateCycloon(cycloon);

        if (metadata != null)
        {
            _repository.AddMetadata(metadata);
        }

        _repository.LogWijziging(
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