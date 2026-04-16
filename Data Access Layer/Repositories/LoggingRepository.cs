using Interface_Layer.InterfaceRepositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data_Access_Layer.Repositories
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly string _connectionString;

        public LoggingRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

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
    }
}
