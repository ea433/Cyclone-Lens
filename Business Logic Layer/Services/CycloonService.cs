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
}