using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualArtGallery.Util
{
    static class DBConnection
    {
        private static SqlConnection connection;

        public static SqlConnection GetConnection()
        {
            // Ensure the connection is initialized
            if (connection == null)
            {
                string connectionString = PropertyUtil.GetPropertyString();

                if (connectionString != null)
                {
                   
                    try
                    {
                        // Open the connection
                        connection = new SqlConnection(connectionString);
                        
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error connecting to the database: {ex.Message}");
                        connection = null;
                    }
                }
            }

            return connection;
        }
    }
}
