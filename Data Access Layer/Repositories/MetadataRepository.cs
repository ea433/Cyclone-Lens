using CycloneLens.Models;
using Data_Access_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
using Logic.Enums;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;

namespace Data_Access_Layer.Repositories
{
    public class MetadataRepository : IMetadataRepository
    {
        private readonly string _connectionString;

        public MetadataRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Metadata> GetMetadata()
        {
            var metadataList = new List<Metadata>();

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

                        var metadata = new Metadata(
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

        public void AddMetadata(Metadata metadata)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Metadata
                (cycloon_id, categorie, windsnelheid, luchtdruk, coordinaten, tijdstip)
                VALUES(@cid, @cat, @wind, @druk, @coord, @tijd)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@cid", metadata.Cycloon_Id);
                    cmd.Parameters.AddWithValue("@cat", (int)metadata.Categorie); // enum dus vandaar int conversie
                    cmd.Parameters.AddWithValue("@wind", metadata.Windsnelheid);
                    cmd.Parameters.AddWithValue("@druk", metadata.Luchtdruk);
                    cmd.Parameters.AddWithValue("@coord", metadata.Coordinaten);
                    cmd.Parameters.AddWithValue("@tijd", metadata.Tijdstip);

                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
