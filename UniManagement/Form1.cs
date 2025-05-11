using System;
using System.Collections.Generic; // Dodane dla List
using System.Data;
using System.Data.SqlClient;
using System.Drawing; // Dodane dla Kolorów w logu (opcjonalnie)
using System.Linq; // Dodane dla operacji LINQ
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace UniManagement
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=FirstOne;Initial Catalog=UniManagement;Integrated Security=True;Encrypt=False";
        private SqlDataAdapter dataAdapter;
        private DataSet dataSet;
        private BindingSource bindingSource;

        // Pola dla nowej funkcjonalności przypisywania
        private DataSet assignmentDataSet;
        private SqlDataAdapter studentsToAssignAdapter;
        private SqlDataAdapter specializationsAdapter;
        private SqlDataAdapter studentPreferencesAdapter;

        private BindingSource bsUnassignedStudents;
        private BindingSource bsGroupsPreview;

        private const string studentsToAssignTableName = "StudentsToAssign";
        private const string specializationsTableName = "Specializations";
        private const string studentPreferencesTableName = "StudentPreferences";
        private const string tempAssignedSpecIdColName = "AssignedSpecializationID_Temp";
        private const string tempAssignmentStatusColName = "AssignmentStatus_Temp";


        public Form1()
        {
            InitializeComponent();
            InitializeDataComponents();
            LoadData();
            SetupDataBindings();
            InitializeAssignmentTabComponents(); // <-- NOWE WYWOŁANIE
        }

        private void InitializeDataComponents()
        {
            try
            {
                dataSet = new DataSet();
                bindingSource = new BindingSource();
                comboBoxStatus.Items.Add("Oczekujący");
                comboBoxStatus.Items.Add("Zakwalifikowany");
                comboBoxStatus.Items.Add("W trakcie rozpatrywania");
                comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas inicjalizacji komponentów danych: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                SqlConnection connection = new SqlConnection(connectionString);
                string query = "SELECT StudentID, NumerAlbumu, Imie, Nazwisko, Email, SpecjalizacjaID, SredniaOcenKwalifikacyjna, StatusKwalifikacji FROM Studenci";
                dataAdapter = new SqlDataAdapter(query, connection);
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
                dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                dataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();
                dataAdapter.Fill(dataSet, "Students");
                bindingSource.DataSource = dataSet.Tables["Students"];
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania danych: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Metoda ConfigureCommands jest wywoływana w LoadData przez SqlCommandBuilder, więc osobne wywołanie nie jest konieczne.
        // Jeśli jednak chcesz ją zachować dla przejrzystości lub innych celów, możesz to zrobić.
        // private void ConfigureCommands()
        // {
        //     SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
        //     dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
        //     dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
        //     dataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();
        // }

        private void SetupDataBindings()
        {
            try
            {
                studentsDataGridView.DataSource = bindingSource;
                textBoxName.DataBindings.Add("Text", bindingSource, "Imie");
                textBoxSurname.DataBindings.Add("Text", bindingSource, "Nazwisko");
                textBoxEmail.DataBindings.Add("Text", bindingSource, "Email");
                numericUpDownAlbumNumber.DataBindings.Add("Text", bindingSource, "NumerAlbumu");
                comboBoxStatus.DataBindings.Add("Text", bindingSource, "StatusKwalifikacji", true, DataSourceUpdateMode.OnPropertyChanged, string.Empty);

                ConfigureDataGridView();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas konfiguracji powiązań danych: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigureDataGridView()
        {
            studentsDataGridView.AutoGenerateColumns = false;
            studentsDataGridView.Columns.Clear();

            // ... (reszta konfiguracji kolumn jak w oryginalnym kodzie) ...
            DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn { DataPropertyName = "StudentID", HeaderText = "ID", ReadOnly = true, Width = 50 };
            studentsDataGridView.Columns.Add(idColumn);
            DataGridViewTextBoxColumn albumColumn = new DataGridViewTextBoxColumn { DataPropertyName = "NumerAlbumu", HeaderText = "Nr albumu", Width = 100 };
            studentsDataGridView.Columns.Add(albumColumn);
            DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn { DataPropertyName = "Imie", HeaderText = "Imię", Width = 120 };
            studentsDataGridView.Columns.Add(nameColumn);
            DataGridViewTextBoxColumn surnameColumn = new DataGridViewTextBoxColumn { DataPropertyName = "Nazwisko", HeaderText = "Nazwisko", Width = 150 };
            studentsDataGridView.Columns.Add(surnameColumn);
            DataGridViewTextBoxColumn emailColumn = new DataGridViewTextBoxColumn { DataPropertyName = "Email", HeaderText = "Email", Width = 200 };
            studentsDataGridView.Columns.Add(emailColumn);
            DataGridViewTextBoxColumn specColumn = new DataGridViewTextBoxColumn { DataPropertyName = "SpecjalizacjaID", HeaderText = "Specjalizacja ID", Width = 120 };
            studentsDataGridView.Columns.Add(specColumn);
            DataGridViewTextBoxColumn avgColumn = new DataGridViewTextBoxColumn { DataPropertyName = "SredniaOcenKwalifikacyjna", HeaderText = "Średnia ocen", Width = 100 };
            studentsDataGridView.Columns.Add(avgColumn);
            DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn { DataPropertyName = "StatusKwalifikacji", HeaderText = "Status kwalifikacji", Width = 150 };
            studentsDataGridView.Columns.Add(statusColumn);
        }

        private void Form1_Load(object sender, EventArgs e) { }

        private void button2_Click_1(object sender, EventArgs e) // Dodawanie
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textBoxName.Text) || string.IsNullOrWhiteSpace(textBoxSurname.Text) || string.IsNullOrWhiteSpace(textBoxEmail.Text) || string.IsNullOrWhiteSpace(numericUpDownAlbumNumber.Text))
                { MessageBox.Show("Proszę wypełnić wszystkie wymagane pola!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                string albumNumber = numericUpDownAlbumNumber.Text;
                if (!ValidateAlbumNumber(albumNumber))
                { MessageBox.Show("Numer albumu musi składać się dokładnie z 6 cyfr!", "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                string email = textBoxEmail.Text;
                if (!ValidateEmail(email))
                { MessageBox.Show("Podany adres email jest nieprawidłowy. Musi zawierać znak @ oraz kropkę!", "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (AlbumNumberExists(albumNumber))
                { MessageBox.Show("Student o podanym numerze albumu już istnieje w bazie danych!", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (EmailExists(email))
                { MessageBox.Show("Student o podanym adresie email już istnieje w bazie danych!", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                DataRow newRow = dataSet.Tables["Students"].NewRow();
                newRow["NumerAlbumu"] = albumNumber;
                newRow["Imie"] = textBoxName.Text;
                newRow["Nazwisko"] = textBoxSurname.Text;
                newRow["Email"] = email;
                newRow["SpecjalizacjaID"] = DBNull.Value;
                newRow["SredniaOcenKwalifikacyjna"] = DBNull.Value; // Można dodać pole do wprowadzania średniej przy dodawaniu lub zostawić NULL
                newRow["StatusKwalifikacji"] = comboBoxStatus.SelectedItem?.ToString() ?? "Oczekujący";
                dataSet.Tables["Students"].Rows.Add(newRow);
                dataAdapter.Update(dataSet, "Students");
                LoadData(); // Odświeżenie danych, w tym ID z bazy
                MessageBox.Show("Student został dodany pomyślnie!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
            catch (Exception ex)
            { MessageBox.Show($"Błąd podczas dodawania studenta: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button3_Click_1(object sender, EventArgs e) // Edycja
        {
            try
            {
                if (bindingSource.Current == null)
                { MessageBox.Show("Proszę wybrać studenta do edycji!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (string.IsNullOrWhiteSpace(textBoxName.Text) || string.IsNullOrWhiteSpace(textBoxSurname.Text) || string.IsNullOrWhiteSpace(textBoxEmail.Text) || string.IsNullOrWhiteSpace(numericUpDownAlbumNumber.Text))
                { MessageBox.Show("Proszę wypełnić wszystkie wymagane pola!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                DataRowView currentRowView = (DataRowView)bindingSource.Current;
                int studentId = Convert.ToInt32(currentRowView["StudentID"]);
                string albumNumber = numericUpDownAlbumNumber.Text;
                if (!ValidateAlbumNumber(albumNumber))
                { MessageBox.Show("Numer albumu musi składać się dokładnie z 6 cyfr!", "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                string email = textBoxEmail.Text;
                if (!ValidateEmail(email))
                { MessageBox.Show("Podany adres email jest nieprawidłowy. Musi zawierać znak @ oraz kropkę!", "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (AlbumNumberExists(albumNumber, studentId))
                { MessageBox.Show("Student o podanym numerze albumu już istnieje w bazie danych!", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                if (EmailExists(email, studentId))
                { MessageBox.Show("Student o podanym adresie email już istnieje w bazie danych!", "Duplikat", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

                bindingSource.EndEdit(); // Zapisuje zmiany z kontrolek do DataRowView
                // SredniaOcenKwalifikacyjna i SpecjalizacjaID nie są edytowalne na tym formularzu, więc nie ma potrzeby ich aktualizować
                dataAdapter.Update(dataSet, "Students");
                LoadData(); // Ważne, aby odświeżyć ID, jeśli byłoby dodawanie i od razu edycja
                MessageBox.Show("Dane studenta zostały zaktualizowane pomyślnie!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            { MessageBox.Show($"Błąd podczas edycji studenta: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button4_Click(object sender, EventArgs e) // Usuwanie
        {
            try
            {
                if (bindingSource.Current == null)
                { MessageBox.Show("Proszę wybrać studenta do usunięcia!", "Uwaga", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
                DialogResult result = MessageBox.Show("Czy na pewno chcesz usunąć tego studenta?", "Potwierdzenie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No) return;

                DataRowView currentRow = (DataRowView)bindingSource.Current;
                // Sprawdzenie, czy student ma powiązane preferencje i czy należy je usunąć (CASCADE w bazie to załatwi)
                // lub wyświetlić ostrzeżenie. Na razie zakładamy, że baza obsługuje kaskadowe usuwanie.
                currentRow.Delete();
                dataAdapter.Update(dataSet, "Students");
                LoadData();
                MessageBox.Show("Student został usunięty pomyślnie!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547) // Foreign key constraint violation
            {
                MessageBox.Show($"Nie można usunąć studenta, ponieważ istnieją powiązane z nim dane (np. preferencje).\nNajpierw usuń powiązane dane.\n\nSzczegóły: {sqlEx.Message}", "Błąd usuwania", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData(); // Przywróć stan, bo Delete() mogło zostać wycofane
            }
            catch (Exception ex)
            { MessageBox.Show($"Błąd podczas usuwania studenta: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button1_Click_1(object sender, EventArgs e) // Odśwież dane (z pierwszej zakładki)
        {
            LoadData();
            MessageBox.Show("Dane zostały odświeżone!", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool EmailExists(string email, int? excludeStudentId = null)
        {
            DataRow[] foundRows = dataSet.Tables["Students"].Select($"Email = '{email.Replace("'", "''")}'" + (excludeStudentId.HasValue ? $" AND StudentID <> {excludeStudentId.Value}" : ""));
            return foundRows.Length > 0;
        }

        private bool AlbumNumberExists(string albumNumber, int? excludeStudentId = null)
        {
            DataRow[] foundRows = dataSet.Tables["Students"].Select($"NumerAlbumu = '{albumNumber}'" + (excludeStudentId.HasValue ? $" AND StudentID <> {excludeStudentId.Value}" : ""));
            return foundRows.Length > 0;
        }

        private bool ValidateAlbumNumber(string albumNumber) => Regex.IsMatch(albumNumber, @"^\d{6}$");
        private bool ValidateEmail(string email) => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        private void ClearFields()
        {
            textBoxName.Clear();
            textBoxSurname.Clear();
            textBoxEmail.Clear();
            if (numericUpDownAlbumNumber.Minimum <= 0 && numericUpDownAlbumNumber.Maximum >= 0) // Zakładając, że 0 jest ok jako domyślne
                numericUpDownAlbumNumber.Value = 0; // Lub numericUpDownAlbumNumber.Text = ""; lub specyficzna wartość minimalna
            else if (numericUpDownAlbumNumber.Minimum > numericUpDownAlbumNumber.Maximum)
                numericUpDownAlbumNumber.Value = numericUpDownAlbumNumber.Minimum; // jeśli min > max to problem z kontrolką
            else
                numericUpDownAlbumNumber.Value = numericUpDownAlbumNumber.Minimum;


            comboBoxStatus.SelectedIndex = -1;
        }

        // Puste event handlery - można je usunąć, jeśli nie są już nigdzie podpięte w Designer.cs
        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { } // Jeśli to duplikat, należy go usunąć
        private void button2_Click(object sender, EventArgs e) { } // Jeśli to duplikat, należy go usunąć
        private void button3_Click(object sender, EventArgs e) { } // Jeśli to duplikat, należy go usunąć
        private void sortStudents1_Load(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { } // Dla studentsDataGridView? Jeśli tak, zachowaj.
        private void dgvGroupsPreview_CellContentClick(object sender, DataGridViewCellEventArgs e) { } // Pusty, dodałem później
        private void txtAssignmentLog_TextChanged(object sender, EventArgs e) { } // Pusty, może być potrzebny, jeśli chcemy auto-scroll


        // ############################################################################
        // NOWA FUNKCJONALNOŚĆ - PRZYPISYWANIE STUDENTÓW DO SPECJALIZACJI
        // ############################################################################

        private void InitializeAssignmentTabComponents()
        {
            try
            {
                assignmentDataSet = new DataSet("AssignmentData");
                bsUnassignedStudents = new BindingSource();
                bsGroupsPreview = new BindingSource();

                // Konfiguracja dgvUnassignedStudents
                dgvUnassignedStudents.AutoGenerateColumns = false;
                dgvUnassignedStudents.Columns.Clear();
                dgvUnassignedStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentID", HeaderText = "ID", Width = 50, ReadOnly = true });
                dgvUnassignedStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Imie", HeaderText = "Imię", Width = 100, ReadOnly = true });
                dgvUnassignedStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nazwisko", HeaderText = "Nazwisko", Width = 120, ReadOnly = true });
                dgvUnassignedStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SredniaOcenKwalifikacyjna", HeaderText = "Średnia", Width = 70, ReadOnly = true });
                dgvUnassignedStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = tempAssignmentStatusColName, HeaderText = "Status Przypisania", Width = 150, ReadOnly = true });
                dgvUnassignedStudents.DataSource = bsUnassignedStudents;

                // Konfiguracja dgvGroupsPreview
                dgvGroupsPreview.AutoGenerateColumns = false;
                dgvGroupsPreview.Columns.Clear();
                dgvGroupsPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NazwaSpecjalizacji", HeaderText = "Specjalizacja", Width = 200, ReadOnly = true });
                dgvGroupsPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LimitMiejsc", HeaderText = "Limit", Width = 60, ReadOnly = true });
                dgvGroupsPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "AktualnieZajeteMiejsca", HeaderText = "Zajęte", Width = 60, ReadOnly = true });
                dgvGroupsPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WolneMiejsca", HeaderText = "Wolne", Width = 60, ReadOnly = true }); // Ta kolumna zostanie dodana jako Expression
                dgvGroupsPreview.DataSource = bsGroupsPreview;

                // Ustawienie początkowych wartości liczników
                lblStudentsToProcessCount.Text = "0";
                lblStudentsAssignedStep1Count.Text = "0";
                lblStudentsAssignedStep2Count.Text = "0";
                lblStudentsStillUnassignedCount.Text = "0";

                AppendToLog("Moduł przypisywania zainicjalizowany.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd inicjalizacji komponentów zakładki przypisywania: {ex.Message}", "Błąd krytyczny", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendToLog($"KRYTYCZNY BŁĄD inicjalizacji: {ex.Message}");
            }
        }

        private void AppendToLog(string message)
        {
            // Upewnij się, że kontrolka txtAssignmentLog istnieje na formularzu
            if (this.Controls.Find("txtAssignmentLog", true).FirstOrDefault() is TextBox logBox)
            {
                logBox.AppendText($"{DateTime.Now:G}: {message}{Environment.NewLine}");
            }
            else if (tcAssignmentResults != null && tpAssignmentLog != null && txtAssignmentLog != null) // Sprawdzenie czy kontrolki są zainicjalizowane
            {
                txtAssignmentLog.AppendText($"{DateTime.Now:G}: {message}{Environment.NewLine}");
            }

        }

        private void btnLoadDataForAssignment_Click(object sender, EventArgs e)
        {
            try
            {
                AppendToLog("Rozpoczęto ładowanie danych do przypisania...");
                assignmentDataSet.Clear(); // Czyści wszystkie tabele i ich definicje

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 1. Wczytaj studentów do przypisania
                    string studentsQuery = @"
                        SELECT StudentID, NumerAlbumu, Imie, Nazwisko, SredniaOcenKwalifikacyjna, StatusKwalifikacji 
                        FROM Studenci 
                        WHERE (SpecjalizacjaID IS NULL OR StatusKwalifikacji = 'Oczekujący' OR StatusKwalifikacji = 'W trakcie rozpatrywania')
                              AND SredniaOcenKwalifikacyjna IS NOT NULL
                        ORDER BY SredniaOcenKwalifikacyjna DESC, Nazwisko, Imie;";
                    studentsToAssignAdapter = new SqlDataAdapter(studentsQuery, conn);
                    studentsToAssignAdapter.Fill(assignmentDataSet, studentsToAssignTableName);
                    AppendToLog($"Załadowano {assignmentDataSet.Tables[studentsToAssignTableName].Rows.Count} studentów do przetworzenia.");

                    // Dodaj tymczasowe kolumny do śledzenia przypisań
                    DataTable dtStudentsToAssign = assignmentDataSet.Tables[studentsToAssignTableName];
                    if (!dtStudentsToAssign.Columns.Contains(tempAssignedSpecIdColName))
                    {
                        dtStudentsToAssign.Columns.Add(tempAssignedSpecIdColName, typeof(int));
                        dtStudentsToAssign.Columns[tempAssignedSpecIdColName].AllowDBNull = true;
                    }
                    if (!dtStudentsToAssign.Columns.Contains(tempAssignmentStatusColName))
                    {
                        dtStudentsToAssign.Columns.Add(tempAssignmentStatusColName, typeof(string));
                    }
                    foreach (DataRow row in dtStudentsToAssign.Rows)
                    {
                        row[tempAssignmentStatusColName] = "Oczekujący na przypisanie";
                    }


                    // 2. Wczytaj specjalizacje
                    string specsQuery = "SELECT SpecjalizacjaID, NazwaSpecjalizacji, LimitMiejsc, MinimalnaSredniaOgólna, AktualnieZajeteMiejsca FROM Specjalizacje ORDER BY NazwaSpecjalizacji;";
                    specializationsAdapter = new SqlDataAdapter(specsQuery, conn);
                    SqlCommandBuilder specBuilder = new SqlCommandBuilder(specializationsAdapter); // Do generowania UpdateCommand
                    specializationsAdapter.UpdateCommand = specBuilder.GetUpdateCommand();
                    specializationsAdapter.Fill(assignmentDataSet, specializationsTableName);
                    AppendToLog($"Załadowano {assignmentDataSet.Tables[specializationsTableName].Rows.Count} specjalizacji.");

                    DataTable dtSpecializations = assignmentDataSet.Tables[specializationsTableName];
                    if (!dtSpecializations.Columns.Contains("WolneMiejsca"))
                    {
                        DataColumn wolneMiejscaCol = new DataColumn("WolneMiejsca", typeof(int));
                        wolneMiejscaCol.Expression = "LimitMiejsc - AktualnieZajeteMiejsca";
                        dtSpecializations.Columns.Add(wolneMiejscaCol);
                    }


                    // 3. Wczytaj preferencje studentów
                    string prefsQuery = "SELECT PreferencjaID, StudentID, SpecjalizacjaID, Priorytet FROM PreferencjeStudentow ORDER BY StudentID, Priorytet;";
                    studentPreferencesAdapter = new SqlDataAdapter(prefsQuery, conn);
                    studentPreferencesAdapter.Fill(assignmentDataSet, studentPreferencesTableName);
                    AppendToLog($"Załadowano {assignmentDataSet.Tables[studentPreferencesTableName].Rows.Count} preferencji studentów.");
                }

                bsUnassignedStudents.DataSource = assignmentDataSet;
                bsUnassignedStudents.DataMember = studentsToAssignTableName;
                // Filtr początkowy, aby pokazać wszystkich ładowanych, status będzie się zmieniał
                bsUnassignedStudents.Filter = $"{tempAssignmentStatusColName} = 'Oczekujący na przypisanie'";


                bsGroupsPreview.DataSource = assignmentDataSet;
                bsGroupsPreview.DataMember = specializationsTableName;

                UpdateAssignmentCountsAndViews();
                AppendToLog("Dane załadowane pomyślnie.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas ładowania danych do przypisania: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendToLog($"BŁĄD ładowania danych: {ex.Message}");
            }
        }

        private void ProcessAssignmentRound(int priority, string statusMessagePrefix)
        {
            if (assignmentDataSet == null || !assignmentDataSet.Tables.Contains(studentsToAssignTableName) ||
                !assignmentDataSet.Tables.Contains(specializationsTableName) || !assignmentDataSet.Tables.Contains(studentPreferencesTableName))
            {
                AppendToLog("Błąd: Dane nie zostały załadowane. Kliknij 'Załaduj/Odśwież dane'.");
                MessageBox.Show("Dane nie zostały załadowane. Proszę najpierw załadować dane.", "Brak danych", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DataTable dtStudents = assignmentDataSet.Tables[studentsToAssignTableName];
            DataTable dtSpecializations = assignmentDataSet.Tables[specializationsTableName];
            DataTable dtPreferences = assignmentDataSet.Tables[studentPreferencesTableName];
            int studentsAssignedThisRound = 0;

            AppendToLog($"Rozpoczęto {statusMessagePrefix} (priorytet {priority})...");

            // Sortowanie studentów wg średniej malejąco, następnie wg nazwiska i imienia
            DataRow[] studentsToConsider = dtStudents.Select($"{tempAssignedSpecIdColName} IS NULL", "SredniaOcenKwalifikacyjna DESC, Nazwisko ASC, Imie ASC");

            foreach (DataRow studentRow in studentsToConsider)
            {
                int studentId = Convert.ToInt32(studentRow["StudentID"]);
                decimal studentAvgGrade = Convert.ToDecimal(studentRow["SredniaOcenKwalifikacyjna"]);

                DataRow[] studentPrefs = dtPreferences.Select($"StudentID = {studentId} AND Priorytet = {priority}");
                if (studentPrefs.Length == 0) continue; // Student nie ma preferencji o tym priorytecie

                int specializationId = Convert.ToInt32(studentPrefs[0]["SpecjalizacjaID"]);
                DataRow[] specRows = dtSpecializations.Select($"SpecjalizacjaID = {specializationId}");
                if (specRows.Length == 0)
                {
                    AppendToLog($"Ostrzeżenie: Student {studentRow["Imie"]} {studentRow["Nazwisko"]} (ID: {studentId}) ma preferencję dla nieistniejącej specjalizacji ID: {specializationId}.");
                    continue;
                }
                DataRow specializationRow = specRows[0];

                decimal minGradeForSpec = specializationRow["MinimalnaSredniaOgólna"] != DBNull.Value ? Convert.ToDecimal(specializationRow["MinimalnaSredniaOgólna"]) : 0;
                int limitMiejsc = Convert.ToInt32(specializationRow["LimitMiejsc"]);
                int aktualnieZajete = Convert.ToInt32(specializationRow["AktualnieZajeteMiejsca"]);

                if (studentAvgGrade >= minGradeForSpec)
                {
                    if (aktualnieZajete < limitMiejsc)
                    {
                        studentRow[tempAssignedSpecIdColName] = specializationId;
                        studentRow[tempAssignmentStatusColName] = $"{statusMessagePrefix}: {specializationRow["NazwaSpecjalizacji"]}";
                        specializationRow["AktualnieZajeteMiejsca"] = aktualnieZajete + 1;
                        studentsAssignedThisRound++;
                        AppendToLog($"Student {studentRow["Imie"]} {studentRow["Nazwisko"]} (Śr: {studentAvgGrade:F2}) przypisany do '{specializationRow["NazwaSpecjalizacji"]}' ({statusMessagePrefix}).");
                    }
                    else
                    {
                        AppendToLog($"Student {studentRow["Imie"]} {studentRow["Nazwisko"]} (Śr: {studentAvgGrade:F2}) nie przypisany do '{specializationRow["NazwaSpecjalizacji"]}' - brak miejsc (Priorytet {priority}).");
                    }
                }
                else
                {
                    AppendToLog($"Student {studentRow["Imie"]} {studentRow["Nazwisko"]} (Śr: {studentAvgGrade:F2}) nie spełnia progu średniej ({minGradeForSpec:F2}) dla '{specializationRow["NazwaSpecjalizacji"]}' (Priorytet {priority}).");
                }
            }
            AppendToLog($"Zakończono {statusMessagePrefix}. Przypisano studentów: {studentsAssignedThisRound}.");
            UpdateAssignmentCountsAndViews();
        }


        private void btnAssignStep1_Click(object sender, EventArgs e)
        {
            try
            {
                ProcessAssignmentRound(1, "Krok 1");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas kroku 1 przypisywania: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendToLog($"BŁĄD w Kroku 1: {ex.Message}");
            }
        }

        private void btnAssignStep2Redistribute_Click(object sender, EventArgs e)
        {
            try
            {
                // Najpierw priorytet 2
                ProcessAssignmentRound(2, "Krok 2 (Prio 2)");
                // Następnie priorytet 3 dla tych, co pozostali
                ProcessAssignmentRound(3, "Krok 2 (Prio 3)");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas kroku 2 redystrybucji: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendToLog($"BŁĄD w Kroku 2: {ex.Message}");
            }
        }

        private void btnConfirmAssignments_Click(object sender, EventArgs e)
        {
            if (assignmentDataSet == null || !assignmentDataSet.Tables.Contains(studentsToAssignTableName) || !assignmentDataSet.Tables.Contains(specializationsTableName))
            {
                MessageBox.Show("Brak danych do przetworzenia lub dane nie zostały załadowane.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult confirmResult = MessageBox.Show("Czy na pewno chcesz zatwierdzić i zapisać wszystkie przypisania do bazy danych?\nTej operacji nie można cofnąć bez interwencji w bazie.", "Potwierdzenie zapisu", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmResult == DialogResult.No)
            {
                AppendToLog("Zapis anulowany przez użytkownika.");
                return;
            }

            int studentsUpdatedCount = 0;
            int specializationsUpdatedCount = 0;

            try
            {
                AppendToLog("Rozpoczęto zatwierdzanie przypisań...");
                DataTable dtStudentsToAssign = assignmentDataSet.Tables[studentsToAssignTableName];
                DataTable dtMainStudents = dataSet.Tables["Students"]; // Główny DataSet

                foreach (DataRow assignedStudentRow in dtStudentsToAssign.Rows)
                {
                    if (assignedStudentRow[tempAssignedSpecIdColName] != DBNull.Value)
                    {
                        int studentId = Convert.ToInt32(assignedStudentRow["StudentID"]);
                        int specializationId = Convert.ToInt32(assignedStudentRow[tempAssignedSpecIdColName]);
                        string finalStatus = "Zakwalifikowany"; // Domyślny status po przypisaniu

                        // Ustalanie statusu na podstawie tego, w którym kroku przypisano
                        string tempStatus = assignedStudentRow[tempAssignmentStatusColName]?.ToString() ?? "";
                        if (tempStatus.Contains("Krok 1")) finalStatus = "Zakwalifikowany (Krok 1)";
                        else if (tempStatus.Contains("Krok 2")) finalStatus = "Zakwalifikowany (Krok 2)";


                        DataRow mainStudentRow = dtMainStudents.Rows.Find(studentId); // Zakładając, że StudentID jest kluczem głównym
                        if (mainStudentRow != null)
                        {
                            mainStudentRow["SpecjalizacjaID"] = specializationId;
                            mainStudentRow["StatusKwalifikacji"] = finalStatus;
                            studentsUpdatedCount++;
                            AppendToLog($"Aktualizacja studenta ID: {studentId} - Spec: {specializationId}, Status: {finalStatus}");
                        }
                        else
                        {
                            AppendToLog($"OSTRZEŻENIE: Nie znaleziono studenta o ID {studentId} w głównym zestawie danych. Pomijanie.");
                        }
                    }
                }

                // Zapisz zmiany w tabeli Studenci (używając głównego dataAdapter)
                if (studentsUpdatedCount > 0)
                {
                    dataAdapter.Update(dataSet, "Students"); // Użyj istniejącego adaptera
                    AppendToLog($"Zaktualizowano {studentsUpdatedCount} studentów w bazie danych.");
                }
                else
                {
                    AppendToLog("Brak studentów do zaktualizowania w bazie danych.");
                }

                // Zapisz zmiany w tabeli Specjalizacje (używając specializationsAdapter)
                // Upewnij się, że specializationsAdapter ma skonfigurowany UpdateCommand
                // Zrobiono to w btnLoadDataForAssignment_Click poprzez SqlCommandBuilder
                if (assignmentDataSet.Tables[specializationsTableName].GetChanges(DataRowState.Modified) != null)
                {
                    specializationsAdapter.Update(assignmentDataSet, specializationsTableName);
                    specializationsUpdatedCount = assignmentDataSet.Tables[specializationsTableName].GetChanges(DataRowState.Modified)?.Rows.Count ?? 0; // Liczba może być niedokładna po Update
                    AppendToLog($"Zaktualizowano informacje o zajętych miejscach dla specjalizacji w bazie danych.");
                    // Trzeba by policzyć faktycznie zmodyfikowane wiersze przed Update lub po przez ponowne pobranie
                }
                else
                {
                    AppendToLog("Brak zmian w danych specjalizacji do zapisania.");
                }


                MessageBox.Show($"Zakończono proces przypisywania.\nZaktualizowano studentów: {studentsUpdatedCount}\nZaktualizowano specjalizacje (zajęte miejsca).", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AppendToLog("Zatwierdzanie zakończone pomyślnie.");

                // Odśwież dane na obu zakładkach
                LoadData(); // Odświeża dane na pierwszej zakładce
                btnLoadDataForAssignment_Click(null, EventArgs.Empty); // Resetuje i odświeża dane na zakładce przypisywania

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zatwierdzania przypisań: {ex.Message}", "Błąd krytyczny", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendToLog($"KRYTYCZNY BŁĄD podczas zatwierdzania: {ex.Message}");
            }
        }

        private void UpdateAssignmentCountsAndViews()
        {
            if (assignmentDataSet == null || !assignmentDataSet.Tables.Contains(studentsToAssignTableName))
            {
                lblStudentsToProcessCount.Text = "0";
                lblStudentsAssignedStep1Count.Text = "0";
                lblStudentsAssignedStep2Count.Text = "0";
                lblStudentsStillUnassignedCount.Text = "0";
                bsUnassignedStudents.Filter = ""; // Wyczyść filtr, jeśli nie ma danych
                return;
            }

            DataTable dtStudents = assignmentDataSet.Tables[studentsToAssignTableName];

            int totalToProcess = dtStudents.Rows.Count;
            int assignedStep1 = dtStudents.AsEnumerable().Count(row => row.Field<string>(tempAssignmentStatusColName)?.StartsWith("Krok 1") ?? false);
            int assignedStep2 = dtStudents.AsEnumerable().Count(row => row.Field<string>(tempAssignmentStatusColName)?.StartsWith("Krok 2") ?? false);
            int stillUnassigned = dtStudents.AsEnumerable().Count(row => row.Field<object>(tempAssignedSpecIdColName) == null || row.Field<object>(tempAssignedSpecIdColName) == DBNull.Value);

            lblStudentsToProcessCount.Text = totalToProcess.ToString();
            lblStudentsAssignedStep1Count.Text = assignedStep1.ToString();
            lblStudentsAssignedStep2Count.Text = assignedStep2.ToString();
            lblStudentsStillUnassignedCount.Text = stillUnassigned.ToString();

            // Odśwież widok nieprzypisanych studentów
            // Filtr może być bardziej złożony, np. pokazywać tylko tych z tempAssignedSpecIdColName IS NULL
            bsUnassignedStudents.Filter = $"{tempAssignedSpecIdColName} IS NULL AND ({tempAssignmentStatusColName} = 'Oczekujący na przypisanie' OR {tempAssignmentStatusColName} IS NULL)";

            // Odświeżenie DataGridViews, jeśli nie odświeżają się automatycznie
            // bsUnassignedStudents.ResetBindings(false); // Może być potrzebne
            // bsGroupsPreview.ResetBindings(false); // Może być potrzebne

            dgvUnassignedStudents.Refresh();
            dgvGroupsPreview.Refresh(); // Dla kolumny Expression

            AppendToLog("Zaktualizowano liczniki i widoki.");
        }

    }
}