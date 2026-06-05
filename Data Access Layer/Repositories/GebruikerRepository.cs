using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Microsoft.Data.SqlClient;

namespace Data_Access_Layer.Repositories
{
    public class GebruikerRepository : IGebruikerRepository
    {
        private readonly string _connectionString;

        public GebruikerRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void RegistreerGebruiker(GebruikerDTO gebruiker)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO Gebruiker (gebruikersnaam, wachtwoord_hash, GebruikerType) 
                                    VALUES (@gebruikersnaam, @wachtwoordHash, @userType)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@gebruikersnaam", gebruiker.Gebruikersnaam);
                        cmd.Parameters.AddWithValue("@wachtwoordHash", gebruiker.WachtwoordHash);
                        cmd.Parameters.AddWithValue("@userType", gebruiker.GebruikerType);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException databaseException)
            {
                throw new Exception("Databasefout bij registreren van gebruiker.", databaseException);
            }
        }

        public GebruikerDTO? GetByGebruikersnaam(string gebruikersnaam)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM Gebruiker WHERE gebruikersnaam = @gebruikersnaam";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@gebruikersnaam", gebruikersnaam);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new GebruikerDTO
                                {
                                    Id = (int)reader["id"],
                                    Gebruikersnaam = reader["gebruikersnaam"]?.ToString() ?? "",
                                    WachtwoordHash = reader["wachtwoord_hash"]?.ToString() ?? "",
                                    GebruikerType = (int)reader["GebruikerType"]
                                };
                            }
                        }
                    }
                }

                return null;
            }
            catch (SqlException databaseException)
            {
                throw new Exception("Databasefout bij ophalen van gebruiker.", databaseException);
            }
        }
    }
}
