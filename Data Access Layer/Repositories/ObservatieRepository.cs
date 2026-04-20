using Microsoft.SqlServer.Types;
using Models.Classes;
using Microsoft.Data.SqlClient;
using Interface_Layer.InterfaceRepositories;

namespace Data_Access_Layer.Repositories
{
    public class ObservatieDAL : IObservatieRepository
    {
        private readonly string _connectionString;

        public ObservatieDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        public void InsertObservatie(Observatie observatie)
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
    }
}
