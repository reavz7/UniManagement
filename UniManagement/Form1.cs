using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniManagement
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=DESKTOP-D705V9A;Initial Catalog=StudentManagement;Integrated Security=True;Encrypt=False";
        public Form1()
        {
            InitializeComponent();  
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'studentManagementDataSet.Studenci' . Możesz go przenieść lub usunąć.
            this.studenciTableAdapter1.Fill(this.studentManagementDataSet.Studenci);
            // TODO: Ten wiersz kodu wczytuje dane do tabeli 'studentManagementDataSet1.Studenci' . Możesz go przenieść lub usunąć.
            this.studenciTableAdapter.Fill(this.studentManagementDataSet1.Studenci);
            LoadSpecializations();
        }
        private void LoadSpecializations()
        {
            try
            {
                // Połączenie z bazą danych
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Zapytanie SQL
                    string query = "SELECT SpecjalizacjaID, NazwaSpecjalizacji FROM Specjalizacje";

                    // Tworzenie obiektu SqlDataAdapter
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connection);

                    // Tworzenie DataSet i załadowanie danych
                    DataSet dataSet = new DataSet();
                    dataAdapter.Fill(dataSet, "Specjalizacje");

                    // Ustawianie źródła danych dla ComboBox
                    DataTable specTable = dataSet.Tables["Specjalizacje"];

                    // Ustawienie BindingSource dla ComboBox
                    BindingSource bindingSource = new BindingSource();
                    bindingSource.DataSource = specTable;

                    // Wypełnianie ComboBox'ów danymi
                    comboBoxSpecialization1.DataSource = bindingSource;
                    comboBoxSpecialization1.DisplayMember = "NazwaSpecjalizacji";
                    comboBoxSpecialization1.ValueMember = "SpecjalizacjaID";

                    comboBoxSpecialization2.DataSource = bindingSource;
                    comboBoxSpecialization2.DisplayMember = "NazwaSpecjalizacji";
                    comboBoxSpecialization2.ValueMember = "SpecjalizacjaID";

                    comboBoxSpecialization3.DataSource = bindingSource;
                    comboBoxSpecialization3.DisplayMember = "NazwaSpecjalizacji";
                    comboBoxSpecialization3.ValueMember = "SpecjalizacjaID";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas ładowania specjalizacji: " + ex.Message);
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
        }

        private void button3_Click(object sender, EventArgs e)
        {
        }

        private void sortStudents1_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void BtnAddStudent_Click(object sender, EventArgs e)
        {

        }
    }
}
