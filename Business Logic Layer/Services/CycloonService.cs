using CycloneLens.Models;
using Logic.Enums;

namespace CycloneLens.Services
{ 
    class CycloonService
    {
        public List<ViewModel> GetActiveCyclonenNATL(List<Cycloon> Cyclonen, List<Metadata> MetadataList)
        {
            return Cyclonen
                .Where(cycloon => cycloon.Status == StatusType.Actief
                        && cycloon.Bassin == BassinType.Noord_Atlantisch)
                .Select(cycloon =>
                {
                    var latestMetadata = MetadataList
                        .Where(metadata => metadata.Cycloon_Id == cycloon.Id)
                        .OrderByDescending(metadata => metadata.Tijdstip)
                        .FirstOrDefault();

                    return new ViewModel(
                        cycloon.Naam,
                        latestMetadata?.Categorie ?? CategorieType.Tropische_Depressie,
                        cycloon.Bassin,
                        cycloon.Status
                    );
                })
                .ToList();
        }
    }
}

