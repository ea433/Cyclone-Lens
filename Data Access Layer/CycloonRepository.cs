using CycloneLens.Interfaces;
using CycloneLens.Models;
using Logic.Enums;
using Microsoft.Data.SqlClient;

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

        public List<Metadata> GetMetadata()
        {
            var metadataList = new List<Metadata>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"SELECT id, cycloon_id, categorie, windsnelheid, luchtdruk,
                                 longitude, latitude, tijdstip 
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

                        var metadata = new Metadata(
                        (int)reader["id"],
                        Convert.ToInt32(reader["cycloon_id"]),
                        (CategorieType)(int)reader["categorie"],
                        Convert.ToDouble(reader["windsnelheid"]),
                        Convert.ToDouble(reader["luchtdruk"]),
                        Convert.ToDouble(reader["longitude"]),
                        Convert.ToDouble(reader["latitude"]),
                        (DateTime)reader["tijdstip"]
                        );

                        metadataList.Add(metadata);
                    }
                }
            }

            return metadataList;
        }
    }
}