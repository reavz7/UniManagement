using System;
using System.Data;
using System.Data.SqlClient;
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
            try
            {
                // First, connect the BindingSource to the DataTable before filling
                studenciBindingSource1.DataSource = this.studentManagementDataSet1.Studenci;

                // Now fill the DataSet
                this.studenciTableAdapter.Fill(this.studentManagementDataSet1.Studenci);

                // DIAGNOSTYKA: Sprawdź czy DataSet zawiera dane
                int rowCount = this.studentManagementDataSet1.Studenci.Rows.Count;
                MessageBox.Show($"Załadowano {rowCount} rekordów do DataSet.");

                // Bind the DataGridView to the BindingSource
                studentsDataGridView.DataSource = studenciBindingSource1;

                // DIAGNOSTYKA: Sprawdź czy BindingSource ma dane
                int bindingSourceCount = studenciBindingSource1.Count;
                MessageBox.Show($"BindingSource zawiera {bindingSourceCount} rekordów.");

                // Skonfiguruj BindingNavigator
                bindingNavigator1.BindingSource = studenciBindingSource1;

                // DIAGNOSTYKA: Upewnij się, że DataGridView ma widoczne kolumny
                EnsureDataGridViewColumns();

                // Force refresh the DataGridView
                studentsDataGridView.Refresh();

                // Dodanie obsługi zdarzenia SelectionChanged
                studentsDataGridView.SelectionChanged += DataGridView1_SelectionChanged;

                // Załaduj inne dane
                LoadSpecializations();
                LoadStatuses();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas ładowania danych: " + ex.Message);
            }
        }

        private void EnsureDataGridViewColumns()
        {
            if (studentsDataGridView.Columns.Count == 0)
            {
                studentsDataGridView.AutoGenerateColumns = true;

            
            }
        }

       

        private void LoadSpecializations()
        {
            try
            {
                string query = "SELECT SpecjalizacjaID, NazwaSpecjalizacji FROM Specjalizacje";
                SqlDataAdapter dataAdapter = new SqlDataAdapter(query, connectionString);
                DataSet dataSet = new DataSet();
                dataAdapter.Fill(dataSet, "Specjalizacje");
                comboBoxSpecialization1.DataSource = dataSet.Tables["Specjalizacje"];
                comboBoxSpecialization1.DisplayMember = "NazwaSpecjalizacji";
                comboBoxSpecialization1.ValueMember = "SpecjalizacjaID";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas ładowania specjalizacji: " + ex.Message);
            }
        }

        private void LoadStatuses()
        {
            comboBoxStatus.Items.Add("Oczekujący");
            comboBoxStatus.Items.Add("Zakwalifikowany");
            comboBoxStatus.Items.Add("Odrzucony");
            comboBoxStatus.SelectedIndex = 0;
        }

        private void AddStudent()
        {
            try
            {
                if (!ValidateStudentData())
                {
                    return;
                }

                // Tworzenie nowego wiersza w DataSet
                DataRow newRow = studentManagementDataSet1.Studenci.NewRow();

                // Wypełnienie wartościami z formularza
                newRow["NumerAlbumu"] = (int)numericUpDownAlbumNumber.Value;
                newRow["Imie"] = textBoxName.Text;
                newRow["Nazwisko"] = textBoxSurname.Text;
                newRow["Email"] = textBoxEmail.Text;

                if (comboBoxSpecialization1.SelectedValue != null)
                    newRow["SpecjalizacjaID"] = comboBoxSpecialization1.SelectedValue;
                else
                    newRow["SpecjalizacjaID"] = DBNull.Value;

                newRow["SredniaOcenKwalifikacyjna"] = 0;
                newRow["StatusKwalifikacji"] = comboBoxStatus.SelectedItem.ToString();

                // Dodanie wiersza do DataSet
                studentManagementDataSet1.Studenci.Rows.Add(newRow);

                // Aktualizacja bazy danych
                studenciTableAdapter.Update(studentManagementDataSet1.Studenci);

                // WAŻNE: Odśwież widok
                studentsDataGridView.Refresh();

                MessageBox.Show("Student został dodany pomyślnie.");

                // Wyczyść pola formularza
                ClearForm();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd przy dodawaniu studenta: " + ex.Message);
            }
        }

        // NOWA METODA: Walidacja danych
        private bool ValidateStudentData()
        {
            if (string.IsNullOrWhiteSpace(textBoxName.Text))
            {
                MessageBox.Show("Imię nie może być puste.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxSurname.Text))
            {
                MessageBox.Show("Nazwisko nie może być puste.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(textBoxEmail.Text))
            {
                MessageBox.Show("Email nie może być pusty.");
                return false;
            }

            return true;
        }

        private void UpdateStudent()
        {
            try
            {
                // Sprawdź czy jakiś wiersz jest zaznaczony
                if (studentsDataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Proszę wybrać studenta do aktualizacji.");
                    return;
                }

                if (!ValidateStudentData())
                {
                    return;
                }

                // Pobierz bieżący wiersz
                DataRowView currentRow = (DataRowView)studenciBindingSource1.Current;

                // Aktualizuj dane
                currentRow["NumerAlbumu"] = (int)numericUpDownAlbumNumber.Value;
                currentRow["Imie"] = textBoxName.Text;
                currentRow["Nazwisko"] = textBoxSurname.Text;
                currentRow["Email"] = textBoxEmail.Text;

                if (comboBoxSpecialization1.SelectedValue != null)
                    currentRow["SpecjalizacjaID"] = comboBoxSpecialization1.SelectedValue;

                currentRow["StatusKwalifikacji"] = comboBoxStatus.SelectedItem.ToString();

                // Zatwierdzenie zmian
                studenciBindingSource1.EndEdit();
                studenciTableAdapter.Update(studentManagementDataSet1.Studenci);

                // WAŻNE: Odśwież widok
                studentsDataGridView.Refresh();

                MessageBox.Show("Dane studenta zostały zaktualizowane.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas aktualizacji danych: " + ex.Message);
            }
        }

        private void DeleteStudent()
        {
            try
            {
                // Sprawdź czy jakiś wiersz jest zaznaczony
                if (studentsDataGridView.CurrentRow == null)
                {
                    MessageBox.Show("Proszę wybrać studenta do usunięcia.");
                    return;
                }

                // Potwierdzenie usunięcia
                if (MessageBox.Show("Czy na pewno chcesz usunąć tego studenta?", "Potwierdzenie",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // Usunięcie wiersza
                    studenciBindingSource1.RemoveCurrent();
                    studenciTableAdapter.Update(studentManagementDataSet1.Studenci);

                    // WAŻNE: Odśwież widok
                    studentsDataGridView.Refresh();

                    MessageBox.Show("Student został usunięty.");
                    ClearForm();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas usuwania studenta: " + ex.Message);
            }
        }

        private void ClearForm()
        {
            numericUpDownAlbumNumber.Value = numericUpDownAlbumNumber.Minimum;
            textBoxName.Clear();
            textBoxSurname.Clear();
            textBoxEmail.Clear();
            comboBoxSpecialization1.SelectedIndex = -1;
            comboBoxStatus.SelectedIndex = 0;
        }

        private void LoadStudentToForm()
        {
            try
            {
                if (studentsDataGridView.CurrentRow != null && studenciBindingSource1.Current != null)
                {
                    DataRowView currentRow = (DataRowView)studenciBindingSource1.Current;

                    // Bezpieczna konwersja NumerAlbumu - sprawdź, czy nie jest DBNull
                    if (currentRow["NumerAlbumu"] != DBNull.Value)
                    {
                        numericUpDownAlbumNumber.Value = Convert.ToDecimal(currentRow["NumerAlbumu"]);
                    }
                    else
                    {
                        numericUpDownAlbumNumber.Value = numericUpDownAlbumNumber.Minimum;
                    }

                    // Bezpieczne pobieranie wartości tekstowych
                    textBoxName.Text = currentRow["Imie"] != DBNull.Value ? currentRow["Imie"].ToString() : "";
                    textBoxSurname.Text = currentRow["Nazwisko"] != DBNull.Value ? currentRow["Nazwisko"].ToString() : "";
                    textBoxEmail.Text = currentRow["Email"] != DBNull.Value ? currentRow["Email"].ToString() : "";

                    // Ustawienie specjalizacji
                    if (currentRow["SpecjalizacjaID"] != DBNull.Value)
                    {
                        int specId = Convert.ToInt32(currentRow["SpecjalizacjaID"]);
                        comboBoxSpecialization1.SelectedValue = specId;
                    }
                    else
                    {
                        comboBoxSpecialization1.SelectedIndex = -1;
                    }

                    // Ustawienie statusu
                    string status = currentRow["StatusKwalifikacji"] != DBNull.Value ?
                                    currentRow["StatusKwalifikacji"].ToString() : "";

                    if (!string.IsNullOrEmpty(status) && comboBoxStatus.Items.Contains(status))
                    {
                        comboBoxStatus.SelectedItem = status;
                    }
                    else
                    {
                        comboBoxStatus.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Błąd podczas ładowania danych studenta: " + ex.Message +
                              "\nSzczegóły: " + ex.StackTrace, "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnAddStudent_Click(object sender, EventArgs e)
        {
            AddStudent();
        }

        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {
            UpdateStudent();
        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {
            DeleteStudent();
        }


        // Poprawna definicja zdarzenia SelectionChanged
        private void DataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            LoadStudentToForm();
        }

        // Pozostałe metody obsługi zdarzeń
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void sortStudents1_Load(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void button2_Click_1(object sender, EventArgs e) { }
    }
}