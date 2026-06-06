using Interface_Layer.API_DTOs;
using Interface_Layer.API_InterfaceRepositories;
using Microsoft.Data.SqlClient;

namespace Data_Access_Layer.API_Repositories
{
    public class NhcStormRepository : INhcStormRepository
    {
        private readonly string _connectionString;

        public NhcStormRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void Insert(NhcStormDTO storm)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = @"INSERT INTO NhcStorm 
                                    (nhc_id, naam, categorie, bassin, windsnelheid, luchtdruk, latitude, longitude, tijdstip)
                                    VALUES 
                                    (@nhcId, @naam, @categorie, @bassin, @windsnelheid, @luchtdruk, @latitude, @longitude, @tijdstip)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@nhcId", storm.NhcId);
                        cmd.Parameters.AddWithValue("@naam", storm.Naam);
                        cmd.Parameters.AddWithValue("@categorie", storm.Categorie);
                        cmd.Parameters.AddWithValue("@bassin", storm.Bassin);
                        cmd.Parameters.AddWithValue("@windsnelheid", storm.Windsnelheid);
                        cmd.Parameters.AddWithValue("@luchtdruk", storm.Luchtdruk);
                        cmd.Parameters.AddWithValue("@latitude", storm.Latitude);
                        cmd.Parameters.AddWithValue("@longitude", storm.Longitude);
                        cmd.Parameters.AddWithValue("@tijdstip", storm.Tijdstip);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException databaseException)
            {
                throw new Exception("Databasefout bij inserteren van NHC storm.", databaseException);
            }
        }

        public List<NhcStormDTO> GetAll()
        {
            List<NhcStormDTO> storms = new();

            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM NhcStorm";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            storms.Add(new NhcStormDTO
                            {
                                Id = (int)reader["id"],
                                NhcId = reader["nhc_id"]?.ToString() ?? "",
                                Naam = reader["naam"]?.ToString() ?? "",
                                Categorie = reader["categorie"]?.ToString() ?? "",
                                Bassin = reader["bassin"]?.ToString() ?? "",
                                Windsnelheid = (int)reader["windsnelheid"],
                                Luchtdruk = (int)reader["luchtdruk"],
                                Latitude = (double)reader["latitude"],
                                Longitude = (double)reader["longitude"],
                                Tijdstip = (DateTime)reader["tijdstip"]
                            });
                        }
                    }
                }
            }
            catch (SqlException databaseException)
            {
                throw new Exception("Databasefout bij ophalen van NHC storms.", databaseException);
            }

            return storms;
        }
    }
}