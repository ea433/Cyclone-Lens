using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;

namespace CycloneLensTestProject.FakeRepositories
{
    public class FakeGebruikerRepository : IGebruikerRepository
    {
        public List<GebruikerDTO> _gebruikers = new();

        public void RegistreerGebruiker(GebruikerDTO gebruiker)
        {
            _gebruikers.Add(gebruiker);
        }

        public GebruikerDTO? GetByGebruikersnaam(string gebruikersnaam)
        {
            return _gebruikers.FirstOrDefault(g => g.Gebruikersnaam == gebruikersnaam);
        }
    }
}
