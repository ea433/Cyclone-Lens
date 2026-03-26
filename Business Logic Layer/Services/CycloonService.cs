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
            .Where(c => c.Status == StatusType.Actief &&
                        c.Bassin == BassinType.Noord_Atlantisch)
            .Select(c =>
            {
                var latest = metadata
                    .Where(m => m.Cycloon_Id == c.Id)
                    .OrderByDescending(m => m.Tijdstip)
                    .FirstOrDefault();

                return new ViewModel(
                    c.Naam,
                    latest?.Categorie ?? CategorieType.Tropische_Depressie,
                    c.Bassin,
                    c.Status
                );
            })
            .ToList();
    }
}