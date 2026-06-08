using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Models.Classes;
using Models.Enums;
using Business_Logic_Layer.Mappers;

namespace Business_Logic_Layer.Services
{
    public class GebruikerService
    {
        private readonly IGebruikerRepository _repository;

        public GebruikerService(IGebruikerRepository repository)
        {
            _repository = repository;
        }

        public void RegistreerGebruiker(string gebruikersnaam, string wachtwoord)
        {
            if (string.IsNullOrWhiteSpace(gebruikersnaam))
                throw new ArgumentException("Gebruikersnaam is verplicht.");

            if (string.IsNullOrWhiteSpace(wachtwoord))
                throw new ArgumentException("Wachtwoord is verplicht.");

            if (wachtwoord.Length < 8 || !wachtwoord.Any(char.IsUpper) || !wachtwoord.Any(char.IsDigit))
                throw new ArgumentException("Wachtwoord moet minimaal 8 tekens bevatten, één hoofdletter en één cijfer.");

            if (_repository.GetByGebruikersnaam(gebruikersnaam) != null)
                throw new ArgumentException("Gebruikersnaam is al in gebruik.");

            string wachtwoordHash = BCrypt.Net.BCrypt.HashPassword(wachtwoord);

            GebruikerDTO gebruikerDto = GebruikerMapper.ToDTO(gebruikersnaam, wachtwoordHash);

            _repository.RegistreerGebruiker(gebruikerDto);
        }

        public Gebruiker? Login(string gebruikersnaam, string wachtwoord)
        {
            GebruikerDTO? dto = _repository.GetByGebruikersnaam(gebruikersnaam);
            {
                if (dto == null || !BCrypt.Net.BCrypt.Verify(wachtwoord, dto.WachtwoordHash))
                    return null;

                return GebruikerMapper.ToDomain(dto);

            }
        }
    }
}