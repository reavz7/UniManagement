using System.Data.SqlClient;
using System.Data;

namespace UniManagement.Data
{
    public class StudentRepository
    {
        private string connectionString = Properties.Settings.Default.StudentManagementConnectionString;

        private SqlDataAdapter adapter;
        private DataSet dataSet;

        public DataTable GetAllStudents()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Studenci";
                adapter = new SqlDataAdapter(query, connection);
                dataSet = new DataSet();

                adapter.Fill(dataSet, "Studenci");
                return dataSet.Tables["Studenci"];
            }
        }

        public void SaveChanges()
        {
            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);
            adapter.Update(dataSet, "Studenci");
        }
    }
}
