using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace UniManagement
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=DESKTOP-D705V9A;Initial Catalog=StudentManagement;Integrated Security=True;Encrypt=False";
        private SqlDataAdapter dataAdapter;
        private DataSet dataSet;
        private BindingSource bindingSource;
        public Form1()
        {
            InitializeComponent();
            InitializeDataComponents();
            LoadData();
            SetupDataBindings();
        }

        private void InitializeDataComponents()
        {
            try
            {
                // Inicjalizacja komponentów danych
                dataSet = new DataSet();
                bindingSource = new BindingSource();

                // Konfiguracja ComboBox dla statusu kwalifikacji
                comboBoxStatus.Items.Add("Zakwalifikowany");
                comboBoxStatus.Items.Add("Niezakwalifikowany");
                comboBoxStatus.Items.Add("W trakcie rozpatrywania");
                comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas inicjalizacji komponentów: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadData()
        {
            try
            {
                if (dataSet.Tables.Contains("Students"))
                {
                    dataSet.Tables["Students"].Clear();
                }

                // Tworzymy nowe połączenie i adapter
                SqlConnection connection = new SqlConnection(connectionString);
                string query = "SELECT StudentID, NumerAlbumu, Imie, Nazwisko, Email, SpecjalizacjaID, SredniaOcenKwalifikacyjna, StatusKwalifikacji FROM Studenci";
                dataAdapter = new SqlDataAdapter(query, connection);

                // Tworzymy SqlCommandBuilder do generowania komend
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

                // Zapełnij DataSet danymi
                dataAdapter.Fill(dataSet, "Students");

                // Ręcznie ustawiamy komendy z tym samym połączeniem
                dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
                dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                dataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                bindingSource.DataSource = dataSet.Tables["Students"];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania danych: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ConfigureCommands()
        {
            // Konfiguracja SqlCommandBuilder dla automatycznego generowania komend
            SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);

            // Konfiguracja INSERT
            dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();

            // Konfiguracja UPDATE
            dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();

            // Konfiguracja DELETE
            dataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();
        }

        private void SetupDataBindings()
        {
            try
            {
                // Powiązanie DataGridView z BindingSource
                studentsDataGridView.DataSource = bindingSource;

                // Powiązanie kontrolek z BindingSource
                textBoxName.DataBindings.Add("Text", bindingSource, "Imie");
                textBoxSurname.DataBindings.Add("Text", bindingSource, "Nazwisko");
                textBoxEmail.DataBindings.Add("Text", bindingSource, "Email");
                numericUpDownAlbumNumber.DataBindings.Add("Text", bindingSource, "NumerAlbumu");
                comboBoxStatus.DataBindings.Add("Text", bindingSource, "StatusKwalifikacji");

                // Konfiguracja DataGridView
                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas konfiguracji powiązań danych: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            // Konfiguracja kolumn DataGridView dla lepszej czytelności
            studentsDataGridView.AutoGenerateColumns = false;

            // Czyszczenie istniejących kolumn
            studentsDataGridView.Columns.Clear();

            // Dodanie kolumn
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn();
            idColumn.DataPropertyName = "StudentID";
            idColumn.HeaderText = "ID";
            idColumn.ReadOnly = true;
            idColumn.Width = 50;
            studentsDataGridView.Columns.Add(idColumn);

            DataGridViewTextBoxColumn albumColumn = new DataGridViewTextBoxColumn();
            albumColumn.DataPropertyName = "NumerAlbumu";
            albumColumn.HeaderText = "Nr albumu";
            albumColumn.Width = 100;
            studentsDataGridView.Columns.Add(albumColumn);

            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn();
            nameColumn.DataPropertyName = "Imie";
            nameColumn.HeaderText = "Imię";
            nameColumn.Width = 120;
            studentsDataGridView.Columns.Add(nameColumn);

            DataGridViewTextBoxColumn surnameColumn = new DataGridViewTextBoxColumn();
            surnameColumn.DataPropertyName = "Nazwisko";
            surnameColumn.HeaderText = "Nazwisko";
            surnameColumn.Width = 150;
            studentsDataGridView.Columns.Add(surnameColumn);

            DataGridViewTextBoxColumn emailColumn = new DataGridViewTextBoxColumn();
            emailColumn.DataPropertyName = "Email";
            emailColumn.HeaderText = "Email";
            emailColumn.Width = 200;
            studentsDataGridView.Columns.Add(emailColumn);

            DataGridViewTextBoxColumn specColumn = new DataGridViewTextBoxColumn();
            specColumn.DataPropertyName = "SpecjalizacjaID";
            specColumn.HeaderText = "Specjalizacja ID";
            specColumn.Width = 120;
            studentsDataGridView.Columns.Add(specColumn);

            DataGridViewTextBoxColumn avgColumn = new DataGridViewTextBoxColumn();
            avgColumn.DataPropertyName = "SredniaOcenKwalifikacyjna";
            avgColumn.HeaderText = "Średnia ocen";
            avgColumn.Width = 100;
            studentsDataGridView.Columns.Add(avgColumn);

            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn();
            statusColumn.DataPropertyName = "StatusKwalifikacji";
            statusColumn.HeaderText = "Status kwalifikacji";
            statusColumn.Width = 150;
            studentsDataGridView.Columns.Add(statusColumn);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }




        //Dodawanie
        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Sprawdzenie czy pola są wypełnione
                if (string.IsNullOrWhiteSpace(textBoxName.Text) ||
                    string.IsNullOrWhiteSpace(textBoxSurname.Text) ||
                    string.IsNullOrWhiteSpace(textBoxEmail.Text) ||
                    string.IsNullOrWhiteSpace(numericUpDownAlbumNumber.Text))
                {
                    MessageBox.Show("Proszę wypełnić wszystkie wymagane pola!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tworzenie nowego wiersza
                DataRow newRow = dataSet.Tables["Students"].NewRow();

                // Ustawienie wartości dla nowego studenta
                newRow["NumerAlbumu"] = numericUpDownAlbumNumber.Text;
                newRow["Imie"] = textBoxName.Text;
                newRow["Nazwisko"] = textBoxSurname.Text;
                newRow["Email"] = textBoxEmail.Text;
                newRow["SpecjalizacjaID"] = DBNull.Value; // Null dla SpecjalizacjaID
                newRow["SredniaOcenKwalifikacyjna"] = DBNull.Value; // Null dla SredniaOcenKwalifikacyjna
                newRow["StatusKwalifikacji"] = comboBoxStatus.SelectedItem ?? DBNull.Value;

                // Dodanie wiersza do DataSet
                dataSet.Tables["Students"].Rows.Add(newRow);

                // Aktualizacja bazy danych
                dataAdapter.Update(dataSet, "Students");

                // Odświeżenie danych
                LoadData();

                MessageBox.Show("Student został dodany pomyślnie!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Wyczyszczenie pól
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas dodawania studenta: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Edycja
        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (bindingSource.Current == null)
                {
                    MessageBox.Show("Proszę wybrać studenta do edycji!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Sprawdzenie czy pola są wypełnione
                if (string.IsNullOrWhiteSpace(textBoxName.Text) ||
                    string.IsNullOrWhiteSpace(textBoxSurname.Text) ||
                    string.IsNullOrWhiteSpace(textBoxEmail.Text) ||
                    string.IsNullOrWhiteSpace(numericUpDownAlbumNumber.Text))
                {
                    MessageBox.Show("Proszę wypełnić wszystkie wymagane pola!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Aktualizacja wiersza
                DataRowView currentRow = (DataRowView)bindingSource.Current;
                currentRow["NumerAlbumu"] = numericUpDownAlbumNumber.Text;
                currentRow["Imie"] = textBoxName.Text;
                currentRow["Nazwisko"] = textBoxSurname.Text;
                currentRow["Email"] = textBoxEmail.Text;
                currentRow["StatusKwalifikacji"] = comboBoxStatus.SelectedItem ?? currentRow["StatusKwalifikacji"];

                // Aktualizacja bazy danych
                dataAdapter.Update(dataSet, "Students");

                // Odświeżenie danych
                LoadData();

                MessageBox.Show("Dane studenta zostały zaktualizowane pomyślnie!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas edycji studenta: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                if (bindingSource.Current == null)
                {
                    MessageBox.Show("Proszę wybrać studenta do usunięcia!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Potwierdzenie usunięcia
                DialogResult result = MessageBox.Show("Czy na pewno chcesz usunąć tego studenta?", "Potwierdzenie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }

                // Usunięcie wiersza
                DataRowView currentRow = (DataRowView)bindingSource.Current;
                currentRow.Delete();

                // Aktualizacja bazy danych
                dataAdapter.Update(dataSet, "Students");

                // Odświeżenie danych
                LoadData();

                MessageBox.Show("Student został usunięty pomyślnie!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Wyczyszczenie pól
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas usuwania studenta: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Dane zostały odświeżone!", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void sortStudents1_Load(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }



        private void ClearFields()
        {
            textBoxName.Clear();
            textBoxSurname.Clear();
            textBoxEmail.Clear();
            numericUpDownAlbumNumber.Value = numericUpDownAlbumNumber.Minimum;
            comboBoxStatus.SelectedIndex = -1;
        }

        
    }
}