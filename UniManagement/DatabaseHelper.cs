using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace UniManagement
{
    public static class DatabaseHelper
    {
        private static string GetConnectionString()
        {
            string connString = ConfigurationManager.ConnectionStrings["UniManagement.Properties.Settings.StudentManagementConnectionString"]?.ConnectionString;

            if (string.IsNullOrEmpty(connString))
            {
                throw new ConfigurationErrorsException("Nie znaleziono connection string o nazwie 'UniManagement.Properties.Settings.StudentManagementConnectionString' w pliku App.config.");
            }
            return connString;
        }

        public static SqlConnection GetConnection()
        {
            try
            {
                return new SqlConnection(GetConnectionString());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas tworzenia SqlConnection: {ex.Message}");
                throw;
            }
        }
    }
}
