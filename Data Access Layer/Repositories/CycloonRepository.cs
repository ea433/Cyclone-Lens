using Data_Access_Layer.DTOs;
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
            var cyclonen = new List<CycloonDTO>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT id, naam, bassin, status FROM Cycloon";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var naam = reader["naam"]?.ToString() ?? "";

                        var statusString = reader["status"]?.ToString();
                        var bassinString = reader["bassin"]?.ToString();

                        if (string.IsNullOrEmpty(statusString) || string.IsNullOrEmpty(bassinString))
                        {
                            throw new Exception("Invalid enum value from database");
                        }

                        var normalizedBassin = bassinString.Replace("-", "_");

                        var cycloon = new CycloonDTO
                        {
                            Id = (int)reader["id"],
                            Naam = reader["naam"]?.ToString() ?? "",
                            Status = reader["status"]?.ToString() ?? "",
                            Bassin = reader["bassin"]?.ToString() ?? ""
                        };

                        cyclonen.Add(cycloon);
                    }
                }
            }

            return cyclonen;
        }

        // fr-05 
        public void UpdateCycloon(CycloonDTO cycloon)
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

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public CycloonDTO? GetById(int id)
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
    }
}

