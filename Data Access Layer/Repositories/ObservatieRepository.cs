using Interface_Layer.DTOs;   
using Interface_Layer.InterfaceRepositories;
using Microsoft.Data.SqlClient;
using Microsoft.SqlServer.Types;
using Models.Classes;

namespace Data_Access_Layer.Repositories
{
    public class ObservatieDAL : IObservatieRepository
    {
        private readonly string _connectionString;

        public ObservatieDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertObservatie(ObservatieDTO observatie)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(_connectionString))
                {
                    string query = @"INSERT INTO Observatie 
                (gebruiker_id, cycloon_id, coordinaten, omschrijving, afbeelding, tijdstip)
                VALUES (@gebruikerId, @cycloonId, @coordinaten, @omschrijving, @afbeelding, @tijdstip)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@gebruikerId", observatie.GebruikerId);
                        cmd.Parameters.AddWithValue("@cycloonId", observatie.CycloonId);
                        cmd.Parameters.AddWithValue("@omschrijving", observatie.Omschrijving);
                        cmd.Parameters.AddWithValue("@afbeelding", (object?)observatie.AfbeeldingPad ?? DBNull.Value);
                        cmd.Parameters.AddWithValue("@tijdstip", observatie.Tijdstip);

                        SqlParameter geoParam = new SqlParameter("@coordinaten", observatie.Coordinaten);
                        geoParam.UdtTypeName = "geography";
                        cmd.Parameters.Add(geoParam);

                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException databaseException)
            {
                throw new Exception("Databasefout bij ophalen van cyclonen.", databaseException);
            }
        }

        public List<Observatie> GetAllObservaties()
        {
            List<Observatie> observaties = new();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                string query = "SELECT * FROM Observatie";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        observaties.Add(new Observatie(
                            (int)reader["id"],
                            (int)reader["gebruiker_id"],
                            (int)reader["cycloon_id"],
                            reader["omschrijving"].ToString() ?? "",
                            reader["afbeelding"]?.ToString(),
                            (SqlGeography)reader["coordinaten"],
                            (DateTime)reader["tijdstip"]
                        ));
                    }
                }
            }

            return observaties;
        }
    }
}
