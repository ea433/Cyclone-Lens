using Interface_Layer.DTOs;
using Interface_Layer.InterfaceRepositories;
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

        public List<MetadataDTO> GetMetadata()
        {
            List<MetadataDTO> metadataList = new();

            try
            { 
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
                            string? categorieString = reader["categorie"]?.ToString();

                            if (string.IsNullOrEmpty(categorieString))
                            {
                                throw new Exception("Invalid categorie value from database");
                            }

                            MetadataDTO metadata = new()
                            {
                                Id = (int)reader["id"],
                                CycloonId = Convert.ToInt32(reader["cycloon_id"]),
                                Categorie = Convert.ToInt32(reader["categorie"]),
                                Windsnelheid = Convert.ToDouble(reader["windsnelheid"]),
                                Luchtdruk = Convert.ToDouble(reader["luchtdruk"]),
                                Coordinaten = (SqlGeography)reader["coordinaten"],
                                Tijdstip = (DateTime)reader["tijdstip"]
                            };
                            metadataList.Add(metadata);
                        }
                    }
                }
            }
            catch (SqlException databaseException)
            {
                throw new Exception("Databasefout bij ophalen van cyclonen.", databaseException);
            }

            return metadataList;
        }

        public void AddMetadata(MetadataDTO metadata)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO Metadata
                (cycloon_id, categorie, windsnelheid, luchtdruk, coordinaten, tijdstip)
                VALUES(@cid, @cat, @wind, @druk, @coord, @tijd)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@cid", metadata.CycloonId);
                        cmd.Parameters.AddWithValue("@cat", (int)metadata.Categorie); // enum dus vandaar int conversie
                        cmd.Parameters.AddWithValue("@wind", metadata.Windsnelheid);
                        cmd.Parameters.AddWithValue("@druk", metadata.Luchtdruk);
                        cmd.Parameters.AddWithValue("@coord", metadata.Coordinaten);
                        cmd.Parameters.AddWithValue("@tijd", metadata.Tijdstip);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException databaseException)
            {
                throw new Exception("Databasefout bij toevoegen van metadata.", databaseException);
            }
        }
    }
}
