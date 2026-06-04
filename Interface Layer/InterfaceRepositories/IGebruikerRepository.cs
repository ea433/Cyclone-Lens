using Interface_Layer.DTOs;

namespace Interface_Layer.InterfaceRepositories
{
    public interface IGebruikerRepository
    {
        void RegistreerGebruiker(GebruikerDTO gebruiker);
        GebruikerDTO? GetByGebruikersnaam(string gebruikersnaam);
    }
}
