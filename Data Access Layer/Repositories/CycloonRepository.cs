using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Microsoft.Data.SqlClient;

namespace Data_Access_Layer.Repositories
{
    public class CycloonRepository : ICycloonRepository
    {
        private readonly string _connectionString;

        public CycloonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<CycloonDTO> GetCyclonen()
        {
            List<CycloonDTO> cyclonen = new();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "SELECT id, naam, bassin, status FROM Cycloon";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string? statusString = reader["status"]?.ToString();
                            string? bassinString = reader["bassin"]?.ToString();

                            if (string.IsNullOrEmpty(statusString) || string.IsNullOrEmpty(bassinString))
                            {
                                throw new Exception("Invalid enum value from database");
                            }

                            CycloonDTO cycloon = new()
                            {
                                Id = (int)reader["id"],
                                Naam = reader["naam"]?.ToString() ?? "",
                                Status = statusString,
                                Bassin = bassinString
                            };

                            cyclonen.Add(cycloon);
                        }
                    }
                }
            }
            catch (SqlException databaseException)
            {
                throw new Exception("Databasefout bij ophalen van cyclonen.", databaseException);
            }
            return cyclonen;
        }

        public void UpdateCycloon(CycloonDTO cycloon)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = @"UPDATE Cycloon 
                         SET naam = @naam, status = @status 
                         WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", cycloon.Id);
                        cmd.Parameters.AddWithValue("@naam", cycloon.Naam);
                        cmd.Parameters.AddWithValue("@status", cycloon.Status.ToString()); // enums

                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                            throw new Exception("Cycloon niet gevonden.");
                    }
                }
            }
            catch (SqlException databaseException)
            {
                throw new Exception("Databasefout bij updaten van cycloon.", databaseException);
            }
        }

        public CycloonDTO? GetById(int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM Cycloon WHERE id = @id";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new CycloonDTO
                                {
                                    Id = (int)reader["id"],
                                    Naam = reader["naam"]?.ToString() ?? "",
                                    Status = reader["status"]?.ToString() ?? "",
                                    Bassin = reader["bassin"]?.ToString() ?? ""
                                };
                            }
                        }
                    }
                }

                return null;
            }

            catch (SqlException databaseException)
            {
                throw new Exception("Databasefout bij ophalen van cycloon bij ID.", databaseException);
            }
        }
    }
}

