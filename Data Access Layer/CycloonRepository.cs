using CycloneLens.Interfaces;
using CycloneLens.Models;
using Logic.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;
using Models.Classes;

namespace CycloneLens.DAL
{
    public class CycloonRepository : ICycloonRepository
    {
        private readonly string _connectionString;

        public CycloonRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Cycloon> GetCyclonen()
        {
            var cyclonen = new List<Cycloon>();

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

                        var cycloon = new Cycloon(
                            (int)reader["id"],
                            naam,
                            Enum.Parse<StatusType>(statusString),
                            Enum.Parse<BassinType>(normalizedBassin)
                        );

                        cyclonen.Add(cycloon);
                    }
                }
            }

            return cyclonen;
        }

        public List<CycloonData> GetMetadata()
        {
            var metadataList = new List<CycloonData>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"SELECT id, cycloon_id, categorie, windsnelheid, luchtdruk,
                                 tijdstip, coordinaten
                                 FROM Metadata";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var categorieString = reader["categorie"]?.ToString();

                        if (string.IsNullOrEmpty(categorieString))
                        {
                            throw new Exception("Invalid categorie value from database");
                        }

                        var metadata = new CycloonData(
                        (int)reader["id"],
                        Convert.ToInt32(reader["cycloon_id"]),
                        (CategorieType)(int)reader["categorie"],
                        Convert.ToDouble(reader["windsnelheid"]),
                        Convert.ToDouble(reader["luchtdruk"]),
                        (SqlGeography)reader["coordinaten"],
                        (DateTime)reader["tijdstip"]
                        );

                        metadataList.Add(metadata);
                    }
                }
            }

            return metadataList;
        }

        // fr-05 
        public void UpdateCycloon(Cycloon cycloon)
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
        public void AddMetadata(CycloonData metadata)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Metadata 
        (cycloon_id, categorie, windsnelheid, luchtdruk, longitude, latitude, tijdstip)
        VALUES (@cid, @cat, @wind, @druk, @lon, @lat, @tijd)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cid", metadata.Cycloon_Id);
                    cmd.Parameters.AddWithValue("@cat", (int)metadata.Categorie); // enums
                    cmd.Parameters.AddWithValue("@wind", metadata.Windsnelheid);
                    cmd.Parameters.AddWithValue("@druk", metadata.Luchtdruk);
                    cmd.Parameters.AddWithValue("@lon", metadata.Coordinaten);
                    cmd.Parameters.AddWithValue("@tijd", metadata.Tijdstip);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // fr-05 logging
        public void LogWijziging(int cycloonId, string actie, int gebruikerId)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO CycloonLog 
        (Cycloon_id, actie, Gebruiker_id, tijdstip)
        VALUES (@cid, @actie, @gid, @tijd)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cid", cycloonId);
                    cmd.Parameters.AddWithValue("@actie", actie);
                    cmd.Parameters.AddWithValue("@gid", gebruikerId);
                    cmd.Parameters.AddWithValue("@tijd", DateTime.UtcNow);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Cycloon? GetById(int id)
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
                            return new Cycloon(
                                (int)reader["id"],
                                reader["naam"]?.ToString() ?? "",
                                Enum.Parse<StatusType>(reader["status"]?.ToString() ?? "Actief"),
                                Enum.Parse<BassinType>((reader["bassin"] as string ?? "Noord_Atlantisch").Replace("-", "_"))
                            );
                        }
                    }
                }
            }

            return null;
        }
    }
}

