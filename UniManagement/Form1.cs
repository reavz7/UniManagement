using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
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
                comboBoxStatus.Items.Add("Oczekujący");
                comboBoxStatus.Items.Add("Zakwalifikowany");
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
                    MessageBox.Show("Proszę wypełnić wszystkie wymagane pola!",
                        "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Walidacja numeru albumu
                string albumNumber = numericUpDownAlbumNumber.Text;
                if (!ValidateAlbumNumber(albumNumber))
                {
                    MessageBox.Show("Numer albumu musi składać się dokładnie z 6 cyfr!",
                        "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Walidacja adresu email
                string email = textBoxEmail.Text;
                if (!ValidateEmail(email))
                {
                    MessageBox.Show("Podany adres email jest nieprawidłowy. Musi zawierać znak @ oraz kropkę!",
                        "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Sprawdzenie czy numer albumu już istnieje
                if (AlbumNumberExists(albumNumber))
                {
                    MessageBox.Show("Student o podanym numerze albumu już istnieje w bazie danych!",
                        "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Sprawdzenie czy email już istnieje
                if (EmailExists(email))
                {
                    MessageBox.Show("Student o podanym adresie email już istnieje w bazie danych!",
                        "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tworzenie nowego wiersza
                DataRow newRow = dataSet.Tables["Students"].NewRow();

                // Ustawienie wartości dla nowego studenta
                newRow["NumerAlbumu"] = albumNumber;
                newRow["Imie"] = textBoxName.Text;
                newRow["Nazwisko"] = textBoxSurname.Text;
                newRow["Email"] = email;
                newRow["SpecjalizacjaID"] = DBNull.Value; // Null dla SpecjalizacjaID
                newRow["SredniaOcenKwalifikacyjna"] = DBNull.Value; // Null dla SredniaOcenKwalifikacyjna
                newRow["StatusKwalifikacji"] = comboBoxStatus.SelectedItem ?? DBNull.Value;

                // Dodanie wiersza do DataSet
                dataSet.Tables["Students"].Rows.Add(newRow);

                // Aktualizacja bazy danych
                dataAdapter.Update(dataSet, "Students");

                // Odświeżenie danych
                LoadData();

                MessageBox.Show("Student został dodany pomyślnie!",
                    "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Wyczyszczenie pól
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas dodawania studenta: {ex.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Edycja
        private void button3_Click_1(object sender, EventArgs e)
        {
            try
            {
                if (bindingSource.Current == null)
                {
                    MessageBox.Show("Proszę wybrać studenta do edycji!",
                        "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Sprawdzenie czy pola są wypełnione
                if (string.IsNullOrWhiteSpace(textBoxName.Text) ||
                    string.IsNullOrWhiteSpace(textBoxSurname.Text) ||
                    string.IsNullOrWhiteSpace(textBoxEmail.Text) ||
                    string.IsNullOrWhiteSpace(numericUpDownAlbumNumber.Text))
                {
                    MessageBox.Show("Proszę wypełnić wszystkie wymagane pola!",
                        "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Pobranie ID aktualnie edytowanego studenta
                DataRowView currentRowView = (DataRowView)bindingSource.Current;
                int studentId = Convert.ToInt32(currentRowView["StudentID"]);

                // Walidacja numeru albumu
                string albumNumber = numericUpDownAlbumNumber.Text;
                if (!ValidateAlbumNumber(albumNumber))
                {
                    MessageBox.Show("Numer albumu musi składać się dokładnie z 6 cyfr!",
                        "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Walidacja adresu email
                string email = textBoxEmail.Text;
                if (!ValidateEmail(email))
                {
                    MessageBox.Show("Podany adres email jest nieprawidłowy. Musi zawierać znak @ oraz kropkę!",
                        "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Sprawdzenie czy numer albumu już istnieje (z wykluczeniem aktualnego rekordu)
                if (AlbumNumberExists(albumNumber, studentId))
                {
                    MessageBox.Show("Student o podanym numerze albumu już istnieje w bazie danych!",
                        "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Sprawdzenie czy email już istnieje (z wykluczeniem aktualnego rekordu)
                if (EmailExists(email, studentId))
                {
                    MessageBox.Show("Student o podanym adresie email już istnieje w bazie danych!",
                        "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Zatwierdzenie zmian z kontrolek do BindingSource
                bindingSource.EndEdit();

                // Aktualizacja bazy danych
                dataAdapter.Update(dataSet, "Students");

                // Odświeżenie danych
                LoadData();

                MessageBox.Show("Dane studenta zostały zaktualizowane pomyślnie!",
                    "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas edycji studenta: {ex.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
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


        private bool EmailExists(string email, int? excludeStudentId = null)
        {
            try
            {
                foreach (DataRow row in dataSet.Tables["Students"].Rows)
                {
                    // Jeśli wiersz jest oznaczony do usunięcia, pomijamy go
                    if (row.RowState == DataRowState.Deleted)
                        continue;

                    // Jeśli podano ID do wykluczenia (np. przy edycji), pomijamy ten rekord
                    if (excludeStudentId.HasValue &&
                        row["StudentID"] != DBNull.Value &&
                        Convert.ToInt32(row["StudentID"]) == excludeStudentId.Value)
                        continue;

                    if (row["Email"].ToString().Equals(email, StringComparison.OrdinalIgnoreCase))
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas sprawdzania adresu email: {ex.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool AlbumNumberExists(string albumNumber, int? excludeStudentId = null)
        {
            try
            {
                foreach (DataRow row in dataSet.Tables["Students"].Rows)
                {
                    // Jeśli wiersz jest oznaczony do usunięcia, pomijamy go
                    if (row.RowState == DataRowState.Deleted)
                        continue;

                    // Jeśli podano ID do wykluczenia (np. przy edycji), pomijamy ten rekord
                    if (excludeStudentId.HasValue &&
                        row["StudentID"] != DBNull.Value &&
                        Convert.ToInt32(row["StudentID"]) == excludeStudentId.Value)
                        continue;

                    if (row["NumerAlbumu"].ToString() == albumNumber)
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas sprawdzania numeru albumu: {ex.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool ValidateAlbumNumber(string albumNumber)
        {
            // Sprawdzenie czy składa się dokładnie z 6 cyfr
            return Regex.IsMatch(albumNumber, @"^\d{6}$");
        }

        // Funkcja walidująca adres email - musi zawierać @ i kropkę
        private bool ValidateEmail(string email)
        {
            // Sprawdzenie podstawowej struktury adresu email
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

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