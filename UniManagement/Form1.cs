using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace UniManagement
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=.;Initial Catalog=StudentManagement;Integrated Security=True;Encrypt=False";
        private SqlDataAdapter dataAdapter;
        private DataSet dataSet;
        private BindingSource bindingSource;

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

        private SqlDataAdapter specializationsComboBoxAdapter;
        private DataTable specializationsDataTable;
        private SqlDataAdapter studentPreferencesDataAdapter;
        private int currentSelectedStudentID = -1;


        private SqlDataAdapter studentsForGradesComboBoxAdapter;
        private DataTable studentsForGradesDataTable;
        private SqlDataAdapter studentSpecificGradesAdapter;
        private DataTable studentGradesDataTable;
        private BindingSource studentGradesBindingSource;
        private SqlCommandBuilder studentGradesCommandBuilder;
        private SqlDataAdapter subjectsAdapter;
        private DataTable subjectsDataTable;
        private bool tabPage2GradesInitialized = false;
        private DataTable averageGradesDataTable;
        private SqlDataAdapter averageGradesAdapter;


        public Form1()
        {
            InitializeComponent();
            InitializeDataComponents();
            InitializeAssignmentTabComponents();
            LoadData(); // Ładuje "Studenci"
            SetupDataBindings();
            InitializeGradesRelatedComponents();
            SetupGradesTabEventHandlers();
            ConfigureSpecializationsTab();
            InitializeAllDataGridViews();
        }

        private void InitializeDataComponents()
        {
            try
            {
                dataSet = new DataSet();
                bindingSource = new BindingSource();
                if (this.Controls.Find("comboBoxStatus", true).FirstOrDefault() is ComboBox cbStatus)
                {
                    cbStatus.Items.Add("Oczekujący");
                    cbStatus.Items.Add("Zakwalifikowany");
                    cbStatus.Items.Add("W trakcie rozpatrywania");
                    cbStatus.DropDownStyle = ComboBoxStyle.DropDownList;
                }
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
                if (dataSet.Tables.Contains("Studenci"))
                {
                    dataSet.Tables["Studenci"].Clear();
                }

                string query = "SELECT StudentID, NumerAlbumu, Imie, Nazwisko, Email, SpecjalizacjaID, SredniaOcenKwalifikacyjna, StatusKwalifikacji FROM Studenci";
                dataAdapter = new SqlDataAdapter(query, connectionString);

                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdapter);
                dataAdapter.InsertCommand = commandBuilder.GetInsertCommand();
                dataAdapter.UpdateCommand = commandBuilder.GetUpdateCommand();
                dataAdapter.DeleteCommand = commandBuilder.GetDeleteCommand();

                dataAdapter.Fill(dataSet, "Studenci");

                if (dataSet.Tables["Studenci"].PrimaryKey.Length == 0 && dataSet.Tables["Studenci"].Columns.Contains("StudentID"))
                {
                    dataSet.Tables["Studenci"].PrimaryKey = new DataColumn[] { dataSet.Tables["Studenci"].Columns["StudentID"] };
                }

                bindingSource.DataSource = dataSet;
                bindingSource.DataMember = "Studenci";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania danych studentów: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


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
                if (string.IsNullOrWhiteSpace(textBoxName.Text) ||
                    string.IsNullOrWhiteSpace(textBoxSurname.Text) ||
                    string.IsNullOrWhiteSpace(textBoxEmail.Text) ||
                    string.IsNullOrWhiteSpace(numericUpDownAlbumNumber.Text))
                {
                    MessageBox.Show("Proszę wypełnić wszystkie wymagane pola!", "Uwaga",
                                  MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string albumNumber = numericUpDownAlbumNumber.Text;
                if (!ValidateAlbumNumber(albumNumber))
                {
                    MessageBox.Show("Numer albumu musi składać się dokładnie z 6 cyfr!",
                                  "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string email = textBoxEmail.Text;
                if (!ValidateEmail(email))
                {
                    MessageBox.Show("Podany adres email jest nieprawidłowy. Musi zawierać znak @ oraz kropkę!",
                                  "Błąd walidacji", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DataRow newRow = dataSet.Tables["Studenci"].NewRow();

                // Since StudentID is likely an auto-increment field in the database,
                // we need to either provide a temporary value or set it to auto-increment in the DataSet

                // OPTION 1: Set StudentID to a temporary negative value
                // The database will ignore this and use its auto-increment value
                newRow["StudentID"] = -1;  // Temporary value that will be replaced by DB auto-increment

                newRow["NumerAlbumu"] = albumNumber;
                newRow["Imie"] = textBoxName.Text;
                newRow["Nazwisko"] = textBoxSurname.Text;
                newRow["Email"] = email;
                newRow["SpecjalizacjaID"] = DBNull.Value;
                newRow["SredniaOcenKwalifikacyjna"] = DBNull.Value;
                newRow["StatusKwalifikacji"] = comboBoxStatus.SelectedItem?.ToString() ?? "Oczekujący";

                dataSet.Tables["Studenci"].Rows.Add(newRow);
                dataAdapter.Update(dataSet, "Studenci");
                LoadData();

                MessageBox.Show("Student został dodany pomyślnie!", "Sukces",
                              MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas dodawania studenta: {ex.Message}", "Błąd",
                              MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


                bindingSource.EndEdit();
                dataAdapter.Update(dataSet, "Studenci"); // Poprawka nazwy tabeli
                LoadData();
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
                currentRow.Delete();
                dataAdapter.Update(dataSet, "Studenci"); // Poprawka nazwy tabeli
                LoadData();
                MessageBox.Show("Student został usunięty pomyślnie!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ClearFields();
            }
            catch (SqlException sqlEx) when (sqlEx.Number == 547)
            {
                MessageBox.Show($"Nie można usunąć studenta, ponieważ istnieją powiązane z nim dane (np. preferencje).\nNajpierw usuń powiązane dane.\n\nSzczegóły: {sqlEx.Message}", "Błąd usuwania", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LoadData();
            }
            catch (Exception ex)
            { MessageBox.Show($"Błąd podczas usuwania studenta: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            LoadData();
            MessageBox.Show("Dane zostały odświeżone!", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool EmailExists(string email, int? excludeStudentId = null)
        {
            // Poprawka nazwy tabeli
            DataRow[] foundRows = dataSet.Tables["Studenci"].Select($"Email = '{email.Replace("'", "''")}'" + (excludeStudentId.HasValue ? $" AND StudentID <> {excludeStudentId.Value}" : ""));
            return foundRows.Length > 0;
        }

        private bool AlbumNumberExists(string albumNumber, int? excludeStudentId = null)
        {
            // Poprawka nazwy tabeli
            DataRow[] foundRows = dataSet.Tables["Studenci"].Select($"NumerAlbumu = '{albumNumber}'" + (excludeStudentId.HasValue ? $" AND StudentID <> {excludeStudentId.Value}" : ""));
            return foundRows.Length > 0;
        }

        private bool ValidateAlbumNumber(string albumNumber) => Regex.IsMatch(albumNumber, @"^\d{6}$");
        private bool ValidateEmail(string email) => Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");

        private void ClearFields()
        {
            textBoxName.Clear();
            textBoxSurname.Clear();
            textBoxEmail.Clear();
            if (numericUpDownAlbumNumber.Minimum <= 0 && numericUpDownAlbumNumber.Maximum >= 0)
                numericUpDownAlbumNumber.Value = 0;
            else if (numericUpDownAlbumNumber.Minimum > numericUpDownAlbumNumber.Maximum)
                numericUpDownAlbumNumber.Value = numericUpDownAlbumNumber.Minimum;
            else
                numericUpDownAlbumNumber.Value = numericUpDownAlbumNumber.Minimum;


            comboBoxStatus.SelectedIndex = -1;
        }


        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void sortStudents1_Load(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void dgvGroupsPreview_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtAssignmentLog_TextChanged(object sender, EventArgs e) { }

        private void InitializeAssignmentTabComponents()
        {
            try
            {
                assignmentDataSet = new DataSet("AssignmentData");
                bsUnassignedStudents = new BindingSource();
                bsGroupsPreview = new BindingSource();

                dgvUnassignedStudents.AutoGenerateColumns = false;
                dgvUnassignedStudents.Columns.Clear();
                dgvUnassignedStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "StudentID", HeaderText = "ID", Width = 50, ReadOnly = true });
                dgvUnassignedStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Imie", HeaderText = "Imię", Width = 100, ReadOnly = true });
                dgvUnassignedStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "Nazwisko", HeaderText = "Nazwisko", Width = 120, ReadOnly = true });
                dgvUnassignedStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "SredniaOcenKwalifikacyjna", HeaderText = "Średnia", Width = 70, ReadOnly = true });
                dgvUnassignedStudents.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = tempAssignmentStatusColName, HeaderText = "Status Przypisania", Width = 150, ReadOnly = true });
                dgvUnassignedStudents.DataSource = bsUnassignedStudents;

                dgvGroupsPreview.AutoGenerateColumns = false;
                dgvGroupsPreview.Columns.Clear();
                dgvGroupsPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "NazwaSpecjalizacji", HeaderText = "Specjalizacja", Width = 200, ReadOnly = true });
                dgvGroupsPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "LimitMiejsc", HeaderText = "Limit", Width = 60, ReadOnly = true });
                dgvGroupsPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "AktualnieZajeteMiejsca", HeaderText = "Zajęte", Width = 60, ReadOnly = true });
                dgvGroupsPreview.Columns.Add(new DataGridViewTextBoxColumn { DataPropertyName = "WolneMiejsca", HeaderText = "Wolne", Width = 60, ReadOnly = true });
                dgvGroupsPreview.DataSource = bsGroupsPreview;

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
            if (this.Controls.Find("txtAssignmentLog", true).FirstOrDefault() is TextBox logBox)
            {
                logBox.AppendText($"{DateTime.Now:G}: {message}{Environment.NewLine}");
            }
            else if (tcAssignmentResults != null && tpAssignmentLog != null && txtAssignmentLog != null)
            {
                txtAssignmentLog.AppendText($"{DateTime.Now:G}: {message}{Environment.NewLine}");
            }

        }


        private void btnLoadDataForAssignment_Click(object sender, EventArgs e)
        {
            try
            {
                AppendToLog("Rozpoczęto ładowanie danych do przypisania...");
                if (assignmentDataSet == null) assignmentDataSet = new DataSet("AssignmentData");
                assignmentDataSet.Clear();

                using (SqlConnection connStdToAssign = new SqlConnection(connectionString))
                {
                    string studentsQuery = @"
                        SELECT StudentID, NumerAlbumu, Imie, Nazwisko, SredniaOcenKwalifikacyjna, StatusKwalifikacji
                        FROM Studenci
                        WHERE (SpecjalizacjaID IS NULL OR StatusKwalifikacji = 'Oczekujący' OR StatusKwalifikacji = 'W trakcie rozpatrywania')
                              AND SredniaOcenKwalifikacyjna IS NOT NULL
                        ORDER BY SredniaOcenKwalifikacyjna DESC, Nazwisko ASC, Imie ASC;";
                    studentsToAssignAdapter = new SqlDataAdapter(studentsQuery, connStdToAssign);
                    studentsToAssignAdapter.Fill(assignmentDataSet, studentsToAssignTableName);
                }
                AppendToLog($"Załadowano {assignmentDataSet.Tables[studentsToAssignTableName].Rows.Count} studentów do przetworzenia.");

                DataTable dtStudentsToAssign = assignmentDataSet.Tables[studentsToAssignTableName];
                if (dtStudentsToAssign.PrimaryKey.Length == 0 && dtStudentsToAssign.Columns.Contains("StudentID"))
                {
                    dtStudentsToAssign.PrimaryKey = new DataColumn[] { dtStudentsToAssign.Columns["StudentID"] };
                }
                if (!dtStudentsToAssign.Columns.Contains(tempAssignedSpecIdColName))
                {
                    dtStudentsToAssign.Columns.Add(tempAssignedSpecIdColName, typeof(int))
                        .AllowDBNull = true;
                }
                if (!dtStudentsToAssign.Columns.Contains(tempAssignmentStatusColName))
                {
                    dtStudentsToAssign.Columns.Add(tempAssignmentStatusColName, typeof(string));
                }
                foreach (DataRow row in dtStudentsToAssign.Rows)
                {
                    row[tempAssignmentStatusColName] = "Oczekujący na przypisanie";
                }


                string specsQuery = "SELECT SpecjalizacjaID, NazwaSpecjalizacji, LimitMiejsc, MinimalnaSredniaOgólna, AktualnieZajeteMiejsca FROM Specjalizacje ORDER BY NazwaSpecjalizacji;";

                specializationsAdapter = new SqlDataAdapter();
                specializationsAdapter.SelectCommand = new SqlCommand(specsQuery, new SqlConnection(connectionString));

                SqlCommandBuilder specBuilder = new SqlCommandBuilder(specializationsAdapter);
                specializationsAdapter.UpdateCommand = specBuilder.GetUpdateCommand();

                specializationsAdapter.Fill(assignmentDataSet, specializationsTableName);
                AppendToLog($"Załadowano {assignmentDataSet.Tables[specializationsTableName].Rows.Count} specjalizacji.");

                DataTable dtSpecializations = assignmentDataSet.Tables[specializationsTableName];
                if (dtSpecializations.PrimaryKey.Length == 0 && dtSpecializations.Columns.Contains("SpecjalizacjaID"))
                {
                    dtSpecializations.PrimaryKey = new DataColumn[] { dtSpecializations.Columns["SpecjalizacjaID"] };
                }
                if (!dtSpecializations.Columns.Contains("WolneMiejsca"))
                {
                    DataColumn wolneMiejscaCol = new DataColumn("WolneMiejsca", typeof(int))
                    {
                        Expression = "LimitMiejsc - AktualnieZajeteMiejsca"
                    };
                    dtSpecializations.Columns.Add(wolneMiejscaCol);
                }

                using (SqlConnection connPrefs = new SqlConnection(connectionString))
                {
                    string prefsQuery = "SELECT PreferencjaID, StudentID, SpecjalizacjaID, Priorytet FROM PreferencjeStudentow ORDER BY StudentID, Priorytet;";
                    studentPreferencesAdapter = new SqlDataAdapter(prefsQuery, connPrefs);
                    studentPreferencesAdapter.Fill(assignmentDataSet, studentPreferencesTableName);
                }
                AppendToLog($"Załadowano {assignmentDataSet.Tables[studentPreferencesTableName].Rows.Count} preferencji studentów.");

                bsUnassignedStudents.DataSource = assignmentDataSet;
                bsUnassignedStudents.DataMember = studentsToAssignTableName;
                bsUnassignedStudents.Filter = $"{tempAssignedSpecIdColName} IS NULL OR {tempAssignmentStatusColName} = 'Oczekujący na przypisanie'";

                bsGroupsPreview.DataSource = assignmentDataSet;
                bsGroupsPreview.DataMember = specializationsTableName;

                UpdateAssignmentCountsAndViews();
                AppendToLog("Dane załadowane pomyślnie.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas ładowania danych do przypisania: {ex.Message}\n{ex.StackTrace}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

            DataRow[] studentsToConsider = dtStudents.Select($"{tempAssignedSpecIdColName} IS NULL", "SredniaOcenKwalifikacyjna DESC, Nazwisko ASC, Imie ASC");

            foreach (DataRow studentRow in studentsToConsider)
            {
                int studentId = Convert.ToInt32(studentRow["StudentID"]);
                decimal studentAvgGrade = Convert.ToDecimal(studentRow["SredniaOcenKwalifikacyjna"]);

                DataRow[] studentPrefs = dtPreferences.Select($"StudentID = {studentId} AND Priorytet = {priority}");
                if (studentPrefs.Length == 0) continue;

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
                ProcessAssignmentRound(2, "Krok 2 (Prio 2)");
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
                DataTable dtMainStudents = dataSet.Tables["Studenci"]; // Poprawka nazwy tabeli

                if (dtMainStudents == null) // Dodatkowe sprawdzenie dla bezpieczeństwa
                {
                    AppendToLog("BŁĄD KRYTYCZNY: Główna tabela studentów ('Studenci') nie została znaleziona w DataSet.");
                    MessageBox.Show("Błąd krytyczny: brak tabeli studentów do aktualizacji.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


                foreach (DataRow assignedStudentRow in dtStudentsToAssign.Rows)
                {
                    if (assignedStudentRow.RowState != DataRowState.Deleted && assignedStudentRow[tempAssignedSpecIdColName] != DBNull.Value)
                    {
                        int studentId = Convert.ToInt32(assignedStudentRow["StudentID"]);
                        int specializationId = Convert.ToInt32(assignedStudentRow[tempAssignedSpecIdColName]);
                        string finalStatus = "Zakwalifikowany";
                        string tempStatus = assignedStudentRow[tempAssignmentStatusColName]?.ToString() ?? "";
                        if (tempStatus.Contains("Krok 1")) finalStatus = "Zakwalifikowany (Krok 1)";
                        else if (tempStatus.Contains("Krok 2")) finalStatus = "Zakwalifikowany (Krok 2)";

                        DataRow mainStudentRow = dtMainStudents.Rows.Find(studentId); // To powinno teraz działać
                        if (mainStudentRow != null)
                        {
                            mainStudentRow["SpecjalizacjaID"] = specializationId;
                            mainStudentRow["StatusKwalifikacji"] = finalStatus;
                            AppendToLog($"Przygotowano aktualizację dla studenta ID: {studentId} - Spec: {specializationId}, Status: {finalStatus}");
                        }
                        else
                        {
                            AppendToLog($"OSTRZEŻENIE: Nie znaleziono studenta o ID {studentId} w głównym zestawie danych. Pomijanie.");
                        }
                    }
                }

                if (dataSet.HasChanges(DataRowState.Modified | DataRowState.Added | DataRowState.Deleted))
                {
                    if (dataAdapter.UpdateCommand == null || dataAdapter.UpdateCommand.Connection == null || string.IsNullOrEmpty(dataAdapter.UpdateCommand.Connection.ConnectionString))
                    {
                        AppendToLog("BŁĄD KRYTYCZNY: Główny dataAdapter nie ma prawidłowego UpdateCommand lub połączenia.");
                        MessageBox.Show("Błąd konfiguracji głównego adaptera danych. Nie można zapisać zmian studentów.", "Błąd krytyczny", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (assignmentDataSet.HasChanges()) assignmentDataSet.RejectChanges();
                        return;
                    }
                    int actualStudentsUpdatedInDb = dataAdapter.Update(dataSet, "Studenci"); // Poprawka nazwy tabeli
                    AppendToLog($"Zaktualizowano {actualStudentsUpdatedInDb} wierszy studentów w bazie danych.");
                    dataSet.AcceptChanges();
                }
                else
                {
                    AppendToLog("Brak zmian w danych studentów do zapisania w bazie.");
                }

                DataTable specializationsChanges = assignmentDataSet.Tables[specializationsTableName].GetChanges(DataRowState.Modified);
                if (specializationsChanges != null && specializationsChanges.Rows.Count > 0)
                {
                    if (specializationsAdapter != null && specializationsAdapter.UpdateCommand != null && specializationsAdapter.UpdateCommand.Connection != null && !string.IsNullOrEmpty(specializationsAdapter.UpdateCommand.Connection.ConnectionString))
                    {
                        int updatedSpecRows = specializationsAdapter.Update(specializationsChanges);
                        AppendToLog($"Zaktualizowano {updatedSpecRows} specjalizacji (zajęte miejsca) w bazie danych.");
                        assignmentDataSet.Tables[specializationsTableName].AcceptChanges();
                        specializationsUpdatedCount = updatedSpecRows;
                    }
                    else
                    {
                        AppendToLog("BŁĄD KRYTYCZNY: Adapter specjalizacji lub jego UpdateCommand/Connection nie jest poprawnie skonfigurowany.");
                        MessageBox.Show("Błąd konfiguracji adaptera specjalizacji. Nie można zapisać zmian.", "Błąd krytyczny", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (dataSet.HasChanges()) dataSet.RejectChanges();
                        return;
                    }
                }
                else
                {
                    AppendToLog("Brak zmian w danych specjalizacji do zapisania.");
                }

                MessageBox.Show($"Zakończono proces przypisywania.\nZaktualizowano studentów w bazie: (liczba zaktualizowanych wierszy zostanie podana przez dataAdapter.Update)\nZaktualizowano specjalizacje w bazie: {specializationsUpdatedCount}.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                AppendToLog("Zatwierdzanie zakończone pomyślnie.");

                LoadData();
                btnLoadDataForAssignment_Click(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zatwierdzania przypisań: {ex.Message}\n{ex.StackTrace}", "Błąd krytyczny", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendToLog($"KRYTYCZNY BŁĄD podczas zatwierdzania: {ex.Message}");
                if (dataSet.HasChanges()) dataSet.RejectChanges();
                if (assignmentDataSet.HasChanges()) assignmentDataSet.RejectChanges();
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
                bsUnassignedStudents.Filter = "";
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

            bsUnassignedStudents.Filter = $"{tempAssignedSpecIdColName} IS NULL AND ({tempAssignmentStatusColName} = 'Oczekujący na przypisanie' OR {tempAssignmentStatusColName} IS NULL)";


            dgvUnassignedStudents.Refresh();
            dgvGroupsPreview.Refresh();

            AppendToLog("Zaktualizowano liczniki i widoki.");
        }

        // Page 3 - KONIEC

        // Page 2

        private void InitializeGradesRelatedComponents()
        {
            try
            {
                studentsForGradesDataTable = new DataTable("StudentsForGradesCombo");
                studentGradesDataTable = new DataTable("StudentGradesDetails");
                studentGradesBindingSource = new BindingSource { DataSource = studentGradesDataTable };
                dgvStudentGrades.DataSource = studentGradesBindingSource;
                subjectsDataTable = new DataTable("Subjects");
                averageGradesDataTable = new DataTable("AverageGrades");
                dgvAverageGrades.DataSource = averageGradesDataTable;

                ConfigureDgvStudentGrades();
                ConfigureDgvAverageGrades();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas inicjalizacji komponentów dla zakładki ocen: {ex.Message}", "Błąd inicjalizacji", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void InitializeAllDataGridViews()
        {
            InitializeSpecializationsStudentsDataGridView();
        }

        private void SetupGradesTabEventHandlers()
        {
            tabControl1.SelectedIndexChanged += TabControl1_SelectedIndexChanged_GradesInit;
            cmbStudentSelectorGrades.SelectedIndexChanged += CmbStudentSelectorGrades_SelectedIndexChanged;
            btnAddGrade.Click += BtnAddGrade_Click;
            btnDeleteGrade.Click += BtnDeleteGrade_Click;
            btnSaveGrades.Click += BtnSaveGrades_Click;
            dgvStudentGrades.CellValidating += DgvStudentGrades_CellValidating;
            btnCalculateAverageGrades.Click += BtnCalculateAverageGrades_Click;
        }

        private void TabControl1_SelectedIndexChanged_GradesInit(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab == tabPage2 && !tabPage2GradesInitialized)
            {
                LoadStudentsToGradesComboBox();
                LoadSubjects();
                tabPage2GradesInitialized = true;
            }
        }

        private void LoadStudentsToGradesComboBox()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT StudentID, Imie, Nazwisko, NumerAlbumu FROM Studenci ORDER BY Nazwisko, Imie";
                    studentsForGradesComboBoxAdapter = new SqlDataAdapter(query, conn);
                    studentsForGradesDataTable.Clear();
                    studentsForGradesComboBoxAdapter.Fill(studentsForGradesDataTable);
                }

                if (!studentsForGradesDataTable.Columns.Contains("DisplayMember"))
                {
                    studentsForGradesDataTable.Columns.Add("DisplayMember", typeof(string), "Imie + ' ' + Nazwisko + ' (' + NumerAlbumu + ')'");
                }

                cmbStudentSelectorGrades.DataSource = studentsForGradesDataTable;
                cmbStudentSelectorGrades.DisplayMember = "DisplayMember";
                cmbStudentSelectorGrades.ValueMember = "StudentID";
                cmbStudentSelectorGrades.SelectedIndex = -1;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd ładowania listy studentów: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadSubjects()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    string query = "SELECT PrzedmiotID, NazwaPrzedmiotu FROM Przedmioty ORDER BY NazwaPrzedmiotu";
                    subjectsAdapter = new SqlDataAdapter(query, conn);
                    subjectsDataTable.Clear();
                    subjectsAdapter.Fill(subjectsDataTable);

                    if (subjectsDataTable.PrimaryKey.Length == 0 && subjectsDataTable.Columns.Contains("PrzedmiotID"))
                    {
                        subjectsDataTable.PrimaryKey = new DataColumn[] { subjectsDataTable.Columns["PrzedmiotID"] };
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd ładowania listy przedmiotów: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbStudentSelectorGrades_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStudentSelectorGrades.SelectedValue != null && cmbStudentSelectorGrades.SelectedValue is int)
            {
                int selectedStudentId = (int)cmbStudentSelectorGrades.SelectedValue;
                LoadStudentGrades(selectedStudentId);
            }
            else
            {
                studentGradesDataTable.Clear();
                studentSpecificGradesAdapter = null;
            }
        }

        private void LoadStudentGrades(int studentId)
        {
            try
            {
                string displayQuery = @"
                    SELECT OS.OcenaID, OS.StudentID, OS.PrzedmiotID, P.NazwaPrzedmiotu, OS.WartoscOceny
                    FROM OcenyStudentow OS
                    INNER JOIN Przedmioty P ON OS.PrzedmiotID = P.PrzedmiotID
                    WHERE OS.StudentID = @StudentID_Display";

                studentGradesDataTable.Clear();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    using (SqlDataAdapter displayAdapter = new SqlDataAdapter(displayQuery, conn))
                    {
                        displayAdapter.SelectCommand.Parameters.AddWithValue("@StudentID_Display", studentId);
                        displayAdapter.Fill(studentGradesDataTable);
                    }
                }

                // Poprawka dla Błędu 1: Konfiguracja AutoIncrement dla OcenaID
                if (studentGradesDataTable.Columns.Contains("OcenaID"))
                {
                    DataColumn ocenaIdCol = studentGradesDataTable.Columns["OcenaID"];
                    if (!ocenaIdCol.AutoIncrement)
                    {
                        ocenaIdCol.AutoIncrement = true;
                        ocenaIdCol.AutoIncrementSeed = -1;
                        ocenaIdCol.AutoIncrementStep = -1;
                        // Upewnij się, że AllowDBNull jest false jeśli to klucz główny, AutoIncrement to załatwia
                        // ocenaIdCol.AllowDBNull = false; // Usuń, jeśli AutoIncrement i PrimaryKey to ustawią
                    }
                    if (studentGradesDataTable.PrimaryKey.Length == 0 ||
                        (studentGradesDataTable.PrimaryKey.Length > 0 && studentGradesDataTable.PrimaryKey[0] != ocenaIdCol))
                    {
                        studentGradesDataTable.PrimaryKey = new DataColumn[] { ocenaIdCol };
                    }
                }


                // Poprawka dla Błędu 2: Konfiguracja adaptera do ZAPISU (studentSpecificGradesAdapter)
                string crudQuery = "SELECT OcenaID, StudentID, PrzedmiotID, WartoscOceny FROM OcenyStudentow WHERE StudentID = @StudentID_CRUD";
                studentSpecificGradesAdapter = new SqlDataAdapter();

                // Utwórz SelectCommand z NOWYM obiektem SqlConnection
                SqlCommand selectCmdForBuilder = new SqlCommand(crudQuery, new SqlConnection(connectionString));
                selectCmdForBuilder.Parameters.AddWithValue("@StudentID_CRUD", studentId);
                studentSpecificGradesAdapter.SelectCommand = selectCmdForBuilder;

                studentGradesCommandBuilder = new SqlCommandBuilder(studentSpecificGradesAdapter);
                studentSpecificGradesAdapter.InsertCommand = studentGradesCommandBuilder.GetInsertCommand();
                studentSpecificGradesAdapter.UpdateCommand = studentGradesCommandBuilder.GetUpdateCommand();
                studentSpecificGradesAdapter.DeleteCommand = studentGradesCommandBuilder.GetDeleteCommand();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd ładowania ocen studenta: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                studentGradesDataTable.Clear();
            }
            finally
            {
                studentGradesBindingSource.ResetBindings(false);
            }
        }

        private void ConfigureDgvStudentGrades()
        {
            dgvStudentGrades.AutoGenerateColumns = false;
            dgvStudentGrades.Columns.Clear();

            dgvStudentGrades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colNazwaPrzedmiotu",
                HeaderText = "Nazwa Przedmiotu",
                DataPropertyName = "NazwaPrzedmiotu",
                ReadOnly = true,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            });
            dgvStudentGrades.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "colWartoscOceny",
                HeaderText = "Ocena",
                DataPropertyName = "WartoscOceny",
                ReadOnly = false,
                DefaultCellStyle = new DataGridViewCellStyle { Format = "N1" }
            });
            string[] hiddenCols = { "OcenaID", "StudentID", "PrzedmiotID" };
            foreach (var colName in hiddenCols)
            {
                dgvStudentGrades.Columns.Add(new DataGridViewTextBoxColumn { Name = "col" + colName, DataPropertyName = colName, Visible = false });
            }
            dgvStudentGrades.AllowUserToAddRows = false;
            dgvStudentGrades.AllowUserToDeleteRows = false;
        }

        private void DgvStudentGrades_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (dgvStudentGrades.Columns[e.ColumnIndex].Name == "colWartoscOceny")
            {
                dgvStudentGrades.Rows[e.RowIndex].ErrorText = "";
                if (e.FormattedValue == null || string.IsNullOrWhiteSpace(e.FormattedValue.ToString()))
                {
                    dgvStudentGrades.Rows[e.RowIndex].ErrorText = "Wartość oceny nie może być pusta.";
                    e.Cancel = true; return;
                }
                if (decimal.TryParse(e.FormattedValue.ToString().Replace(",", "."),
                    System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out decimal gradeValue))
                {
                    decimal[] validGrades = { 2.0m, 2.5m, 3.0m, 3.5m, 4.0m, 4.5m, 5.0m };
                    if (!validGrades.Contains(gradeValue))
                    {
                        dgvStudentGrades.Rows[e.RowIndex].ErrorText = "Nieprawidłowa ocena. Dopuszczalne: 2.0, 2.5,..., 5.0.";
                        e.Cancel = true;
                    }
                }
                else
                {
                    dgvStudentGrades.Rows[e.RowIndex].ErrorText = "Wartość oceny musi być liczbą.";
                    e.Cancel = true;
                }
            }
        }
        private void InitializeSpecializationComponents()
        {
            try
            {
                // Inicjalizacja adaptera dla specjalizacji do ComboBox-ów
                specializationsComboBoxAdapter = new SqlDataAdapter(
                    "SELECT SpecjalizacjaID, NazwaSpecjalizacji FROM Specjalizacje ORDER BY NazwaSpecjalizacji",
                    connectionString);

                specializationsDataTable = new DataTable("SpecjalizacjeComboBox");
                specializationsComboBoxAdapter.Fill(specializationsDataTable);

                // Konfiguracja ComboBox-ów na specjalizacje
                comboBoxSpecialization1.DisplayMember = "NazwaSpecjalizacji";
                comboBoxSpecialization1.ValueMember = "SpecjalizacjaID";
                comboBoxSpecialization1.DataSource = specializationsDataTable.Copy();

                comboBoxSpecialization2.DisplayMember = "NazwaSpecjalizacji";
                comboBoxSpecialization2.ValueMember = "SpecjalizacjaID";
                comboBoxSpecialization2.DataSource = specializationsDataTable.Copy();

                comboBoxSpecialization3.DisplayMember = "NazwaSpecjalizacji";
                comboBoxSpecialization3.ValueMember = "SpecjalizacjaID";
                comboBoxSpecialization3.DataSource = specializationsDataTable.Copy();

                // Adapter dla danych preferencji studentów
                studentPreferencesDataAdapter = new SqlDataAdapter(
                    "SELECT PreferencjaID, StudentID, SpecjalizacjaID, Priorytet FROM PreferencjeStudentow",
                    connectionString);

                SqlCommandBuilder preferencesCommandBuilder = new SqlCommandBuilder(studentPreferencesDataAdapter);
                studentPreferencesDataAdapter.InsertCommand = preferencesCommandBuilder.GetInsertCommand();
                studentPreferencesDataAdapter.UpdateCommand = preferencesCommandBuilder.GetUpdateCommand();
                studentPreferencesDataAdapter.DeleteCommand = preferencesCommandBuilder.GetDeleteCommand();

                // Dodaj obsługę zdarzenia dla DataGridView studentów
                studentsDataGridView.SelectionChanged += StudentsDataGridView_SelectionChanged;

                AppendToLog("Moduł specjalizacji zainicjalizowany pomyślnie.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd inicjalizacji komponentów specjalizacji: {ex.Message}", "Błąd",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendToLog($"BŁĄD inicjalizacji specjalizacji: {ex.Message}");
            }
        }
        private void ConfigureSpecializationsTab()
        {
            InitializeSpecializationComponents();
            LoadStudentPreferences();
        }

        private void StudentsDataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if (studentsDataGridView.CurrentRow != null)
            {
                DataRowView selectedRow = (DataRowView)studentsDataGridView.CurrentRow.DataBoundItem;
                if (selectedRow != null)
                {
                    currentSelectedStudentID = Convert.ToInt32(selectedRow["StudentID"]);
                    LoadStudentPreferences();
                }
            }
        }

        private void LoadStudentPreferences()
        {
            try
            {
                if (currentSelectedStudentID <= 0)
                {
                    ClearPreferencesComboBoxes();
                    return;
                }

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Pobierz preferencje studenta
                    string query = "SELECT SpecjalizacjaID, Priorytet FROM PreferencjeStudentow " +
                                   "WHERE StudentID = @StudentID ORDER BY Priorytet";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@StudentID", currentSelectedStudentID);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        Dictionary<int, int> preferences = new Dictionary<int, int>();

                        while (reader.Read())
                        {
                            int specID = reader.GetInt32(0);
                            int priority = reader.GetInt32(1);
                            preferences[priority] = specID;
                        }

                        ClearPreferencesComboBoxes();

                        // Ustaw preferencje w odpowiednich ComboBox-ach
                        if (preferences.ContainsKey(1))
                            comboBoxSpecialization1.SelectedValue = preferences[1];

                        if (preferences.ContainsKey(2))
                            comboBoxSpecialization2.SelectedValue = preferences[2];

                        if (preferences.ContainsKey(3))
                            comboBoxSpecialization3.SelectedValue = preferences[3];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas wczytywania preferencji studenta: {ex.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ClearPreferencesComboBoxes()
        {
            comboBoxSpecialization1.SelectedIndex = -1;
            comboBoxSpecialization2.SelectedIndex = -1;
            comboBoxSpecialization3.SelectedIndex = -1;
        }

   

        private void InitializeSpecializationsStudentsDataGridView()
        {
            try
            {
                dataGridViewSpecializations.AutoGenerateColumns = false;
                dataGridViewSpecializations.Columns.Clear();

                DataGridViewTextBoxColumn idColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "StudentID",
                    HeaderText = "ID",
                    ReadOnly = true,
                    Width = 50
                };
                dataGridViewSpecializations.Columns.Add(idColumn);

                DataGridViewTextBoxColumn albumColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "NumerAlbumu",
                    HeaderText = "Nr albumu",
                    Width = 100
                };
                dataGridViewSpecializations.Columns.Add(albumColumn);

                DataGridViewTextBoxColumn nameColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Imie",
                    HeaderText = "Imię",
                    Width = 120
                };
                dataGridViewSpecializations.Columns.Add(nameColumn);

                DataGridViewTextBoxColumn surnameColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "Nazwisko",
                    HeaderText = "Nazwisko",
                    Width = 150
                };
                dataGridViewSpecializations.Columns.Add(surnameColumn);

                DataGridViewTextBoxColumn avgColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "SredniaOcenKwalifikacyjna",
                    HeaderText = "Średnia ocen",
                    Width = 100
                };
                dataGridViewSpecializations.Columns.Add(avgColumn);

                DataGridViewTextBoxColumn statusColumn = new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "StatusKwalifikacji",
                    HeaderText = "Status kwalifikacji",
                    Width = 150
                };
                dataGridViewSpecializations.Columns.Add(statusColumn);

                dataGridViewSpecializations.DataSource = bindingSource;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd inicjalizacji tabeli studentów na zakładce specjalizacji: {ex.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void BtnAddGrade_Click(object sender, EventArgs e)
        {
            if (cmbStudentSelectorGrades.SelectedValue == null || !(cmbStudentSelectorGrades.SelectedValue is int currentStudentId))
            {
                MessageBox.Show("Proszę wybrać studenta.", "Brak studenta", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (subjectsDataTable == null || subjectsDataTable.Rows.Count == 0)
            {
                MessageBox.Show("Brak załadowanych przedmiotów. Nie można dodać oceny.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var addGradeForm = new AddGradeForm(subjectsDataTable.Copy()))
            {
                if (addGradeForm.ShowDialog(this) == DialogResult.OK)
                {
                    int subjectId = addGradeForm.SelectedSubjectId;
                    decimal gradeValue = addGradeForm.SelectedGradeValue;

                    bool gradeExists = studentGradesDataTable.AsEnumerable()
                        .Any(row => row.RowState != DataRowState.Deleted &&
                                    row.Field<int>("StudentID") == currentStudentId &&
                                    row.Field<int>("PrzedmiotID") == subjectId);
                    if (gradeExists)
                    {
                        MessageBox.Show("Student ma już ocenę z wybranego przedmiotu.", "Ocena już istnieje", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    try
                    {
                        DataRow newRow = studentGradesDataTable.NewRow();
                        // OcenaID zostanie przypisana przez AutoIncrement (-1, -2 itd.) lokalnie.
                        // Baza danych nada właściwe ID podczas zapisu.
                        newRow["StudentID"] = currentStudentId;
                        newRow["PrzedmiotID"] = subjectId;
                        newRow["WartoscOceny"] = gradeValue;
                        DataRow subjectInfoRow = subjectsDataTable.Rows.Find(subjectId);
                        newRow["NazwaPrzedmiotu"] = subjectInfoRow?["NazwaPrzedmiotu"]?.ToString() ?? "Brak nazwy";

                        studentGradesDataTable.Rows.Add(newRow);
                        MessageBox.Show("Ocena dodana lokalnie. Kliknij 'Zapisz Zmiany'.", "Dodano", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Błąd dodawania oceny lokalnie: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void BtnDeleteGrade_Click(object sender, EventArgs e)
        {
            if (dgvStudentGrades.CurrentRow == null || dgvStudentGrades.CurrentRow.IsNewRow)
            {
                MessageBox.Show("Proszę zaznaczyć ocenę do usunięcia.", "Brak zaznaczenia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Czy na pewno chcesz usunąć wybraną ocenę?", "Potwierdzenie", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ((DataRowView)dgvStudentGrades.CurrentRow.DataBoundItem).Row.Delete();
                    MessageBox.Show("Ocena oznaczona do usunięcia. Kliknij 'Zapisz Zmiany'.", "Usunięto", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Błąd oznaczania oceny do usunięcia: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnSaveGrades_Click(object sender, EventArgs e)
        {
            if (cmbStudentSelectorGrades.SelectedValue == null || !(cmbStudentSelectorGrades.SelectedValue is int currentStudentId))
            {
                MessageBox.Show("Żaden student nie jest wybrany.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
            }
            if (studentSpecificGradesAdapter == null || studentSpecificGradesAdapter.SelectCommand == null) // Dodatkowe sprawdzenie
            {
                MessageBox.Show("Adapter zapisu ocen nie jest gotowy. Proszę ponownie wybrać studenta.", "Błąd krytyczny", MessageBoxButtons.OK, MessageBoxIcon.Error); return;
            }
            try
            {
                this.Validate();
                studentGradesBindingSource.EndEdit();

                DataTable changes = studentGradesDataTable.GetChanges();
                if (changes == null || changes.Rows.Count == 0)
                {
                    MessageBox.Show("Brak zmian do zapisania.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information); return;
                }

                int rowsAffected = studentSpecificGradesAdapter.Update(changes);
                studentGradesDataTable.AcceptChanges();

                MessageBox.Show($"Zapisano {rowsAffected} zmian w ocenach.", "Zapisano", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadStudentGrades(currentStudentId);
            }
            catch (DBConcurrencyException dbcex)
            {
                MessageBox.Show($"Konflikt współbieżności: {dbcex.Message}", "Konflikt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                studentGradesDataTable.RejectChanges();
                LoadStudentGrades(currentStudentId);
            }
            catch (SqlException sqlEx)
            {
                MessageBox.Show($"Błąd SQL ({sqlEx.Number}): {sqlEx.Message}\n{sqlEx.StackTrace}", "Błąd SQL", MessageBoxButtons.OK, MessageBoxIcon.Error);
                studentGradesDataTable.RejectChanges();
                // Możesz chcieć ponownie załadować oceny, aby przywrócić stan z bazy danych
                LoadStudentGrades(currentStudentId);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ogólny błąd zapisu ocen: {ex.Message}\n{ex.StackTrace}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                studentGradesDataTable.RejectChanges();
                // Możesz chcieć ponownie załadować oceny
                LoadStudentGrades(currentStudentId);
            }
        }


        private void ConfigureDgvAverageGrades()
        {
            dgvAverageGrades.AutoGenerateColumns = false;
            dgvAverageGrades.Columns.Clear();
            string[] headers = { "Numer Albumu", "Imię", "Nazwisko", "Obliczona Średnia" };
            string[] props = { "NumerAlbumu", "Imie", "Nazwisko", "ObliczonaSrednia" };
            for (int i = 0; i < headers.Length; i++)
            {
                var col = new DataGridViewTextBoxColumn { HeaderText = headers[i], DataPropertyName = props[i], Name = "colAvg" + props[i] };
                if (props[i] == "ObliczonaSrednia") col.DefaultCellStyle.Format = "N2";
                col.AutoSizeMode = (props[i] == "Nazwisko" || props[i] == "Imie") ? DataGridViewAutoSizeColumnMode.Fill : DataGridViewAutoSizeColumnMode.AllCells;
                dgvAverageGrades.Columns.Add(col);
            }
            dgvAverageGrades.ReadOnly = true;
            dgvAverageGrades.AllowUserToAddRows = false;
        }

        private void BtnCalculateAverageGrades_Click(object sender, EventArgs e) => CalculateAndDisplayAverages();

        private void CalculateAndDisplayAverages()
        {
            try
            {
                string query = @"
                    SELECT S.StudentID, S.NumerAlbumu, S.Imie, S.Nazwisko, AVG(CAST(OS.WartoscOceny AS DECIMAL(4,2))) AS ObliczonaSrednia
                    FROM Studenci S JOIN OcenyStudentow OS ON S.StudentID = OS.StudentID
                    GROUP BY S.StudentID, S.NumerAlbumu, S.Imie, S.Nazwisko
                    HAVING COUNT(OS.OcenaID) > 0 ORDER BY S.Nazwisko, S.Imie;";
                averageGradesAdapter = new SqlDataAdapter(query, connectionString);
                averageGradesDataTable.Clear();
                averageGradesAdapter.Fill(averageGradesDataTable);

                if (averageGradesDataTable.Rows.Count > 0 &&
                    MessageBox.Show("Czy zaktualizować 'Średnią Ocen Kwalifikacyjną' w tabeli Studenci?",
                                    "Aktualizacja średnich", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    UpdateStudentAveragesInDatabase();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd obliczania średnich: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void UpdateStudentAveragesInDatabase()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlTransaction trans = conn.BeginTransaction())
                {
                    int updatedCount = 0;
                    try
                    {
                        foreach (DataRow row in averageGradesDataTable.Rows)
                        {
                            using (SqlCommand cmd = new SqlCommand("UPDATE Studenci SET SredniaOcenKwalifikacyjna = @Srednia WHERE StudentID = @StudentID", conn, trans))
                            {
                                cmd.Parameters.AddWithValue("@Srednia", row["ObliczonaSrednia"]);
                                cmd.Parameters.AddWithValue("@StudentID", row["StudentID"]);
                                cmd.ExecuteNonQuery();
                                updatedCount++;
                            }
                        }
                        trans.Commit();
                        MessageBox.Show($"Zaktualizowano średnie dla {updatedCount} studentów.", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadData();
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback();
                        MessageBox.Show($"Błąd aktualizacji średnich w bazie: {ex.Message}", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnCalculateAverageGrades_Click_1(object sender, EventArgs e)
        {
            LoadData();
        }

        private void buttonAsignSpecializations_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentSelectedStudentID <= 0)
                {
                    MessageBox.Show("Proszę wybrać studenta z listy.", "Brak zaznaczenia",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (comboBoxSpecialization1.SelectedIndex == -1 &&
                    comboBoxSpecialization2.SelectedIndex == -1 &&
                    comboBoxSpecialization3.SelectedIndex == -1)
                {
                    MessageBox.Show("Proszę wybrać przynajmniej jedną specjalizację.",
                        "Brak wyboru", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Sprawdź duplikaty w wyborach
                HashSet<int> selectedSpecIds = new HashSet<int>();

                if (comboBoxSpecialization1.SelectedIndex != -1)
                {
                    int specId = (int)comboBoxSpecialization1.SelectedValue;
                    selectedSpecIds.Add(specId);
                }

                if (comboBoxSpecialization2.SelectedIndex != -1)
                {
                    int specId = (int)comboBoxSpecialization2.SelectedValue;
                    if (selectedSpecIds.Contains(specId))
                    {
                        MessageBox.Show("Nie można wybrać tej samej specjalizacji więcej niż raz.",
                            "Duplikat wyboru", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    selectedSpecIds.Add(specId);
                }

                if (comboBoxSpecialization3.SelectedIndex != -1)
                {
                    int specId = (int)comboBoxSpecialization3.SelectedValue;
                    if (selectedSpecIds.Contains(specId))
                    {
                        MessageBox.Show("Nie można wybrać tej samej specjalizacji więcej niż raz.",
                            "Duplikat wyboru", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    selectedSpecIds.Add(specId);
                }

                // Usuń istniejące preferencje
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string deleteQuery = "DELETE FROM PreferencjeStudentow WHERE StudentID = @StudentID";
                    SqlCommand deleteCmd = new SqlCommand(deleteQuery, conn);
                    deleteCmd.Parameters.AddWithValue("@StudentID", currentSelectedStudentID);
                    deleteCmd.ExecuteNonQuery();

                    // Dodaj nowe preferencje
                    DataTable preferencesTable = new DataTable();
                    preferencesTable.Columns.Add("StudentID", typeof(int));
                    preferencesTable.Columns.Add("SpecjalizacjaID", typeof(int));
                    preferencesTable.Columns.Add("Priorytet", typeof(int));

                    if (comboBoxSpecialization1.SelectedIndex != -1)
                    {
                        DataRow row = preferencesTable.NewRow();
                        row["StudentID"] = currentSelectedStudentID;
                        row["SpecjalizacjaID"] = comboBoxSpecialization1.SelectedValue;
                        row["Priorytet"] = 1;
                        preferencesTable.Rows.Add(row);
                    }

                    if (comboBoxSpecialization2.SelectedIndex != -1)
                    {
                        DataRow row = preferencesTable.NewRow();
                        row["StudentID"] = currentSelectedStudentID;
                        row["SpecjalizacjaID"] = comboBoxSpecialization2.SelectedValue;
                        row["Priorytet"] = 2;
                        preferencesTable.Rows.Add(row);
                    }

                    if (comboBoxSpecialization3.SelectedIndex != -1)
                    {
                        DataRow row = preferencesTable.NewRow();
                        row["StudentID"] = currentSelectedStudentID;
                        row["SpecjalizacjaID"] = comboBoxSpecialization3.SelectedValue;
                        row["Priorytet"] = 3;
                        preferencesTable.Rows.Add(row);
                    }

                    // Dodaj nowe preferencje do bazy danych
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                    {
                        bulkCopy.DestinationTableName = "PreferencjeStudentow";
                        bulkCopy.ColumnMappings.Add("StudentID", "StudentID");
                        bulkCopy.ColumnMappings.Add("SpecjalizacjaID", "SpecjalizacjaID");
                        bulkCopy.ColumnMappings.Add("Priorytet", "Priorytet");
                        bulkCopy.WriteToServer(preferencesTable);
                    }

                    // Ustaw status studenta na "W trakcie rozpatrywania"
                    string updateStudentQuery = "UPDATE Studenci SET StatusKwalifikacji = 'W trakcie rozpatrywania' " +
                                              "WHERE StudentID = @StudentID";
                    SqlCommand updateCmd = new SqlCommand(updateStudentQuery, conn);
                    updateCmd.Parameters.AddWithValue("@StudentID", currentSelectedStudentID);
                    updateCmd.ExecuteNonQuery();

                    // Odśwież dane
                    LoadData();
                    MessageBox.Show("Preferencje specjalizacji zostały pomyślnie zapisane.",
                        "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    AppendToLog($"Zaktualizowano preferencje specjalizacji dla studenta ID: {currentSelectedStudentID}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Błąd podczas zapisywania preferencji specjalizacji: {ex.Message}",
                    "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Error);
                AppendToLog($"BŁĄD zapisywania preferencji: {ex.Message}");
            }
        }
    }

    // Formatka z przedmiotami i ocenami w Page 2
    public class AddGradeForm : Form
    {
        public int SelectedSubjectId { get; private set; }
        public decimal SelectedGradeValue { get; private set; }

        private ComboBox cmbSubjects;
        private NumericUpDown nudGradeValue;
        private Button btnOk;
        private Button btnCancel;
        private DataTable localSubjectsTable;

        public AddGradeForm(DataTable subjects)
        {
            this.localSubjectsTable = subjects;
            InitializeFormComponents();
            LoadSubjectsIntoComboBox();
        }

        private void InitializeFormComponents()
        {
            this.Text = "Dodaj Ocenę";
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.ClientSize = new Size(350, 150);
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            Label lblSubject = new Label { Text = "Przedmiot:", Location = new Point(10, 20), AutoSize = true };
            cmbSubjects = new ComboBox { Location = new Point(100, 17), Width = 230, DropDownStyle = ComboBoxStyle.DropDownList };

            Label lblGrade = new Label { Text = "Ocena:", Location = new Point(10, 55), AutoSize = true };
            nudGradeValue = new NumericUpDown
            {
                Location = new Point(100, 52),
                Width = 100,
                Minimum = 2.0m,
                Maximum = 5.0m,
                Increment = 0.5m,
                DecimalPlaces = 1,
                Value = 3.0m
            };

            btnOk = new Button { Text = "OK", Location = new Point(170, 100), DialogResult = DialogResult.OK };
            btnCancel = new Button { Text = "Anuluj", Location = new Point(250, 100), DialogResult = DialogResult.Cancel };

            btnOk.Click += (s, e) =>
            {
                if (cmbSubjects.SelectedValue != null)
                {
                    SelectedSubjectId = (int)cmbSubjects.SelectedValue;
                    SelectedGradeValue = nudGradeValue.Value;
                }
                else
                {
                    MessageBox.Show("Proszę wybrać przedmiot.", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.DialogResult = DialogResult.None;
                }
            };

            this.Controls.AddRange(new Control[] { lblSubject, cmbSubjects, lblGrade, nudGradeValue, btnOk, btnCancel });
            this.AcceptButton = btnOk;
            this.CancelButton = btnCancel;
        }

        private void LoadSubjectsIntoComboBox()
        {
            if (localSubjectsTable != null && localSubjectsTable.Rows.Count > 0)
            {
                cmbSubjects.DataSource = localSubjectsTable;
                cmbSubjects.DisplayMember = "NazwaPrzedmiotu";
                cmbSubjects.ValueMember = "PrzedmiotID";
                if (localSubjectsTable.Rows.Count > 0) cmbSubjects.SelectedIndex = 0;
            }
            else
            {
                cmbSubjects.Enabled = false;
            }
        }

        // Page 2 - KONIEC
    }
}