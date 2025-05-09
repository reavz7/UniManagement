namespace UniManagement
{
    partial class Form1
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.studentManagementDataSet1 = new UniManagement.StudentManagementDataSet1();
            this.studenciBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.studenciTableAdapter = new UniManagement.StudentManagementDataSet1TableAdapters.StudenciTableAdapter();
            this.tableAdapterManager = new UniManagement.StudentManagementDataSet1TableAdapters.TableAdapterManager();
            this.studentsDataGridView = new System.Windows.Forms.DataGridView();
            this.studentIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.numerAlbumuDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.imieDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nazwiskoDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.emailDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.specjalizacjaIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.sredniaOcenKwalifikacyjnaDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.statusKwalifikacjiDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.studenciBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.studentManagementDataSet = new UniManagement.StudentManagementDataSet();
            this.studenciTableAdapter1 = new UniManagement.StudentManagementDataSetTableAdapters.StudenciTableAdapter();
            this.tableAdapterManager1 = new UniManagement.StudentManagementDataSetTableAdapters.TableAdapterManager();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.bindingNavigator1 = new System.Windows.Forms.BindingNavigator(this.components);
            this.BtnAddStudent = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.btnDeleteStudent = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnUpdateStudent = new System.Windows.Forms.ToolStripButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.comboBoxStatus = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxEmail = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.numericUpDownAlbumNumber = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxSurname = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBoxSpecialization1 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxSpecialization2 = new System.Windows.Forms.ComboBox();
            this.comboBoxSpecialization3 = new System.Windows.Forms.ComboBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tcAssignmentResults = new System.Windows.Forms.TabControl();
            this.tpUnassignedStudents = new System.Windows.Forms.TabPage();
            this.dgvUnassignedStudents = new System.Windows.Forms.DataGridView();
            this.tpAssignmentLog = new System.Windows.Forms.TabPage();
            this.txtAssignmentLog = new System.Windows.Forms.TextBox();
            this.tpGroupsPreview = new System.Windows.Forms.TabPage();
            this.dgvGroupsPreview = new System.Windows.Forms.DataGridView();
            this.pnlAssignmentControls = new System.Windows.Forms.Panel();
            this.lblStudentsStillUnassignedCount = new System.Windows.Forms.Label();
            this.lblInfoStillUnassigned = new System.Windows.Forms.Label();
            this.lblStudentsAssignedStep2Count = new System.Windows.Forms.Label();
            this.lblInfoAssignedStep2 = new System.Windows.Forms.Label();
            this.lblStudentsAssignedStep1Count = new System.Windows.Forms.Label();
            this.lblInfoAssignedStep1 = new System.Windows.Forms.Label();
            this.lblStudentsToProcessCount = new System.Windows.Forms.Label();
            this.lblInfoStudentsToProcess = new System.Windows.Forms.Label();
            this.btnLoadDataForAssignment = new System.Windows.Forms.Button();
            this.btnAssignStep1 = new System.Windows.Forms.Button();
            this.btnAssignStep2Redistribute = new System.Windows.Forms.Button();
            this.btnConfirmAssignments = new System.Windows.Forms.Button();
            this.przedmiotyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.przedmiotyTableAdapter = new UniManagement.StudentManagementDataSetTableAdapters.PrzedmiotyTableAdapter();
            this.ocenyStudentowBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ocenyStudentowTableAdapter = new UniManagement.StudentManagementDataSet1TableAdapters.OcenyStudentowTableAdapter();
            this.specjalizacjeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.specjalizacjeTableAdapter = new UniManagement.StudentManagementDataSet1TableAdapters.SpecjalizacjeTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.studentManagementDataSet1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studenciBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentsDataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studenciBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentManagementDataSet)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).BeginInit();
            this.bindingNavigator1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlbumNumber)).BeginInit();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tcAssignmentResults.SuspendLayout();
            this.tpUnassignedStudents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnassignedStudents)).BeginInit();
            this.tpAssignmentLog.SuspendLayout();
            this.tpGroupsPreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupsPreview)).BeginInit();
            this.pnlAssignmentControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.przedmiotyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ocenyStudentowBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.specjalizacjeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // studentManagementDataSet1
            // 
            this.studentManagementDataSet1.DataSetName = "StudentManagementDataSet1";
            this.studentManagementDataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // studenciBindingSource
            // 
            this.studenciBindingSource.DataMember = "Studenci";
            this.studenciBindingSource.DataSource = this.studentManagementDataSet1;
            // 
            // studenciTableAdapter
            // 
            this.studenciTableAdapter.ClearBeforeFill = true;
            // 
            // tableAdapterManager
            // 
            this.tableAdapterManager.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager.OcenyStudentowTableAdapter = null;
            this.tableAdapterManager.PreferencjeStudentowTableAdapter = null;
            this.tableAdapterManager.PrzedmiotyTableAdapter = null;
            this.tableAdapterManager.SpecjalizacjeTableAdapter = null;
            this.tableAdapterManager.StudenciTableAdapter = this.studenciTableAdapter;
            this.tableAdapterManager.UpdateOrder = UniManagement.StudentManagementDataSet1TableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // studentsDataGridView
            // 
            this.studentsDataGridView.AutoGenerateColumns = false;
            this.studentsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.studentsDataGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.studentIDDataGridViewTextBoxColumn,
            this.numerAlbumuDataGridViewTextBoxColumn,
            this.imieDataGridViewTextBoxColumn,
            this.nazwiskoDataGridViewTextBoxColumn,
            this.emailDataGridViewTextBoxColumn,
            this.specjalizacjaIDDataGridViewTextBoxColumn,
            this.sredniaOcenKwalifikacyjnaDataGridViewTextBoxColumn,
            this.statusKwalifikacjiDataGridViewTextBoxColumn});
            this.studentsDataGridView.DataSource = this.studenciBindingSource1;
            this.studentsDataGridView.Location = new System.Drawing.Point(647, 55);
            this.studentsDataGridView.Margin = new System.Windows.Forms.Padding(4);
            this.studentsDataGridView.Name = "studentsDataGridView";
            this.studentsDataGridView.RowHeadersWidth = 51;
            this.studentsDataGridView.Size = new System.Drawing.Size(743, 620);
            this.studentsDataGridView.TabIndex = 1;
            // 
            // studentIDDataGridViewTextBoxColumn
            // 
            this.studentIDDataGridViewTextBoxColumn.DataPropertyName = "StudentID";
            this.studentIDDataGridViewTextBoxColumn.HeaderText = "StudentID";
            this.studentIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.studentIDDataGridViewTextBoxColumn.Name = "studentIDDataGridViewTextBoxColumn";
            this.studentIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.studentIDDataGridViewTextBoxColumn.Width = 125;
            // 
            // numerAlbumuDataGridViewTextBoxColumn
            // 
            this.numerAlbumuDataGridViewTextBoxColumn.DataPropertyName = "NumerAlbumu";
            this.numerAlbumuDataGridViewTextBoxColumn.HeaderText = "NumerAlbumu";
            this.numerAlbumuDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.numerAlbumuDataGridViewTextBoxColumn.Name = "numerAlbumuDataGridViewTextBoxColumn";
            this.numerAlbumuDataGridViewTextBoxColumn.Width = 125;
            // 
            // imieDataGridViewTextBoxColumn
            // 
            this.imieDataGridViewTextBoxColumn.DataPropertyName = "Imie";
            this.imieDataGridViewTextBoxColumn.HeaderText = "Imie";
            this.imieDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.imieDataGridViewTextBoxColumn.Name = "imieDataGridViewTextBoxColumn";
            this.imieDataGridViewTextBoxColumn.Width = 125;
            // 
            // nazwiskoDataGridViewTextBoxColumn
            // 
            this.nazwiskoDataGridViewTextBoxColumn.DataPropertyName = "Nazwisko";
            this.nazwiskoDataGridViewTextBoxColumn.HeaderText = "Nazwisko";
            this.nazwiskoDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.nazwiskoDataGridViewTextBoxColumn.Name = "nazwiskoDataGridViewTextBoxColumn";
            this.nazwiskoDataGridViewTextBoxColumn.Width = 125;
            // 
            // emailDataGridViewTextBoxColumn
            // 
            this.emailDataGridViewTextBoxColumn.DataPropertyName = "Email";
            this.emailDataGridViewTextBoxColumn.HeaderText = "Email";
            this.emailDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.emailDataGridViewTextBoxColumn.Name = "emailDataGridViewTextBoxColumn";
            this.emailDataGridViewTextBoxColumn.Width = 125;
            // 
            // specjalizacjaIDDataGridViewTextBoxColumn
            // 
            this.specjalizacjaIDDataGridViewTextBoxColumn.DataPropertyName = "SpecjalizacjaID";
            this.specjalizacjaIDDataGridViewTextBoxColumn.HeaderText = "SpecjalizacjaID";
            this.specjalizacjaIDDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.specjalizacjaIDDataGridViewTextBoxColumn.Name = "specjalizacjaIDDataGridViewTextBoxColumn";
            this.specjalizacjaIDDataGridViewTextBoxColumn.Width = 125;
            // 
            // sredniaOcenKwalifikacyjnaDataGridViewTextBoxColumn
            // 
            this.sredniaOcenKwalifikacyjnaDataGridViewTextBoxColumn.DataPropertyName = "SredniaOcenKwalifikacyjna";
            this.sredniaOcenKwalifikacyjnaDataGridViewTextBoxColumn.HeaderText = "SredniaOcenKwalifikacyjna";
            this.sredniaOcenKwalifikacyjnaDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.sredniaOcenKwalifikacyjnaDataGridViewTextBoxColumn.Name = "sredniaOcenKwalifikacyjnaDataGridViewTextBoxColumn";
            this.sredniaOcenKwalifikacyjnaDataGridViewTextBoxColumn.Width = 125;
            // 
            // statusKwalifikacjiDataGridViewTextBoxColumn
            // 
            this.statusKwalifikacjiDataGridViewTextBoxColumn.DataPropertyName = "StatusKwalifikacji";
            this.statusKwalifikacjiDataGridViewTextBoxColumn.HeaderText = "StatusKwalifikacji";
            this.statusKwalifikacjiDataGridViewTextBoxColumn.MinimumWidth = 6;
            this.statusKwalifikacjiDataGridViewTextBoxColumn.Name = "statusKwalifikacjiDataGridViewTextBoxColumn";
            this.statusKwalifikacjiDataGridViewTextBoxColumn.Width = 125;
            // 
            // studenciBindingSource1
            // 
            this.studenciBindingSource1.DataMember = "Studenci";
            this.studenciBindingSource1.DataSource = this.studentManagementDataSet;
            // 
            // studentManagementDataSet
            // 
            this.studentManagementDataSet.DataSetName = "StudentManagementDataSet";
            this.studentManagementDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // studenciTableAdapter1
            // 
            this.studenciTableAdapter1.ClearBeforeFill = true;
            // 
            // tableAdapterManager1
            // 
            this.tableAdapterManager1.BackupDataSetBeforeUpdate = false;
            this.tableAdapterManager1.OcenyStudentowTableAdapter = null;
            this.tableAdapterManager1.PreferencjeStudentowTableAdapter = null;
            this.tableAdapterManager1.PrzedmiotyTableAdapter = null;
            this.tableAdapterManager1.SpecjalizacjeTableAdapter = null;
            this.tableAdapterManager1.StudenciTableAdapter = this.studenciTableAdapter1;
            this.tableAdapterManager1.UpdateOrder = UniManagement.StudentManagementDataSetTableAdapters.TableAdapterManager.UpdateOrderOption.InsertUpdateDelete;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(16, 0);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1420, 719);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.bindingNavigator1);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.button4);
            this.tabPage1.Controls.Add(this.button3);
            this.tabPage1.Controls.Add(this.button2);
            this.tabPage1.Controls.Add(this.studentsDataGridView);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage1.Size = new System.Drawing.Size(1412, 690);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Zarządzanie studentami";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // bindingNavigator1
            // 
            this.bindingNavigator1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.bindingNavigator1.AddNewItem = this.BtnAddStudent;
            this.bindingNavigator1.BindingSource = this.studenciBindingSource;
            this.bindingNavigator1.CountItem = this.bindingNavigatorCountItem;
            this.bindingNavigator1.DeleteItem = this.btnDeleteStudent;
            this.bindingNavigator1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.bindingNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.bindingNavigatorSeparator2,
            this.BtnAddStudent,
            this.btnDeleteStudent,
            this.btnUpdateStudent});
            this.bindingNavigator1.Location = new System.Drawing.Point(4, 4);
            this.bindingNavigator1.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bindingNavigator1.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bindingNavigator1.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bindingNavigator1.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bindingNavigator1.Name = "bindingNavigator1";
            this.bindingNavigator1.PositionItem = this.bindingNavigatorPositionItem;
            this.bindingNavigator1.Size = new System.Drawing.Size(1404, 27);
            this.bindingNavigator1.TabIndex = 13;
            this.bindingNavigator1.Text = "bindingNavigator1";
            // 
            // BtnAddStudent
            // 
            this.BtnAddStudent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnAddStudent.Image = ((System.Drawing.Image)(resources.GetObject("BtnAddStudent.Image")));
            this.BtnAddStudent.Name = "BtnAddStudent";
            this.BtnAddStudent.RightToLeftAutoMirrorImage = true;
            this.BtnAddStudent.Size = new System.Drawing.Size(29, 24);
            this.BtnAddStudent.Text = "Dodaj nowy";
            this.BtnAddStudent.Click += new System.EventHandler(this.BtnAddStudent_Click);
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(38, 24);
            this.bindingNavigatorCountItem.Text = "z {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Suma elementów";
            // 
            // btnDeleteStudent
            // 
            this.btnDeleteStudent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnDeleteStudent.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteStudent.Image")));
            this.btnDeleteStudent.Name = "btnDeleteStudent";
            this.btnDeleteStudent.RightToLeftAutoMirrorImage = true;
            this.btnDeleteStudent.Size = new System.Drawing.Size(29, 24);
            this.btnDeleteStudent.Text = "Usuń";
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveFirstItem.Text = "Przenieś pierwszy";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMovePreviousItem.Text = "Przenieś poprzedni";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Pozycja";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(65, 27);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Bieżąca pozycja";
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 27);
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveNextItem.Text = "Przenieś następny";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(29, 24);
            this.bindingNavigatorMoveLastItem.Text = "Przenieś ostatni";
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 27);
            // 
            // btnUpdateStudent
            // 
            this.btnUpdateStudent.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.btnUpdateStudent.Image = ((System.Drawing.Image)(resources.GetObject("btnUpdateStudent.Image")));
            this.btnUpdateStudent.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUpdateStudent.Name = "btnUpdateStudent";
            this.btnUpdateStudent.Size = new System.Drawing.Size(29, 24);
            this.btnUpdateStudent.Text = "toolStripButton1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.comboBoxStatus);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBoxEmail);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBoxName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.numericUpDownAlbumNumber);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.textBoxSurname);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.comboBoxSpecialization1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.comboBoxSpecialization2);
            this.groupBox1.Controls.Add(this.comboBoxSpecialization3);
            this.groupBox1.Location = new System.Drawing.Point(81, 55);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(455, 436);
            this.groupBox1.TabIndex = 12;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Dane studenta";
            // 
            // comboBoxStatus
            // 
            this.comboBoxStatus.FormattingEnabled = true;
            this.comboBoxStatus.Location = new System.Drawing.Point(237, 220);
            this.comboBoxStatus.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxStatus.Name = "comboBoxStatus";
            this.comboBoxStatus.Size = new System.Drawing.Size(160, 24);
            this.comboBoxStatus.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(31, 220);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 16);
            this.label8.TabIndex = 14;
            this.label8.Text = "Status";
            // 
            // textBoxEmail
            // 
            this.textBoxEmail.Location = new System.Drawing.Point(239, 135);
            this.textBoxEmail.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxEmail.Name = "textBoxEmail";
            this.textBoxEmail.Size = new System.Drawing.Size(159, 22);
            this.textBoxEmail.TabIndex = 12;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(31, 135);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 16);
            this.label7.TabIndex = 13;
            this.label7.Text = "Email";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(31, 42);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 16);
            this.label1.TabIndex = 11;
            this.label1.Text = "Imie";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 396);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 16);
            this.label6.TabIndex = 11;
            this.label6.Text = "Trzeci wybór";
            // 
            // textBoxName
            // 
            this.textBoxName.Location = new System.Drawing.Point(240, 42);
            this.textBoxName.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(159, 22);
            this.textBoxName.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(29, 345);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(78, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "Drugi wybór";
            // 
            // numericUpDownAlbumNumber
            // 
            this.numericUpDownAlbumNumber.Location = new System.Drawing.Point(239, 180);
            this.numericUpDownAlbumNumber.Margin = new System.Windows.Forms.Padding(4);
            this.numericUpDownAlbumNumber.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.numericUpDownAlbumNumber.Name = "numericUpDownAlbumNumber";
            this.numericUpDownAlbumNumber.Size = new System.Drawing.Size(160, 22);
            this.numericUpDownAlbumNumber.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 288);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Pierwszy wybór";
            // 
            // textBoxSurname
            // 
            this.textBoxSurname.Location = new System.Drawing.Point(240, 89);
            this.textBoxSurname.Margin = new System.Windows.Forms.Padding(4);
            this.textBoxSurname.Name = "textBoxSurname";
            this.textBoxSurname.Size = new System.Drawing.Size(159, 22);
            this.textBoxSurname.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(29, 180);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 16);
            this.label3.TabIndex = 11;
            this.label3.Text = "Nr. albumu";
            // 
            // comboBoxSpecialization1
            // 
            this.comboBoxSpecialization1.FormattingEnabled = true;
            this.comboBoxSpecialization1.Location = new System.Drawing.Point(237, 288);
            this.comboBoxSpecialization1.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxSpecialization1.Name = "comboBoxSpecialization1";
            this.comboBoxSpecialization1.Size = new System.Drawing.Size(160, 24);
            this.comboBoxSpecialization1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 89);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 11;
            this.label2.Text = "Nazwisko";
            // 
            // comboBoxSpecialization2
            // 
            this.comboBoxSpecialization2.FormattingEnabled = true;
            this.comboBoxSpecialization2.Location = new System.Drawing.Point(239, 345);
            this.comboBoxSpecialization2.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxSpecialization2.Name = "comboBoxSpecialization2";
            this.comboBoxSpecialization2.Size = new System.Drawing.Size(160, 24);
            this.comboBoxSpecialization2.TabIndex = 6;
            // 
            // comboBoxSpecialization3
            // 
            this.comboBoxSpecialization3.FormattingEnabled = true;
            this.comboBoxSpecialization3.Location = new System.Drawing.Point(239, 396);
            this.comboBoxSpecialization3.Margin = new System.Windows.Forms.Padding(4);
            this.comboBoxSpecialization3.Name = "comboBoxSpecialization3";
            this.comboBoxSpecialization3.Size = new System.Drawing.Size(160, 24);
            this.comboBoxSpecialization3.TabIndex = 7;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(421, 539);
            this.button4.Margin = new System.Windows.Forms.Padding(4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(189, 44);
            this.button4.TabIndex = 10;
            this.button4.Text = "Usuń studenta";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(224, 539);
            this.button3.Margin = new System.Windows.Forms.Padding(4);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(189, 44);
            this.button3.TabIndex = 9;
            this.button3.Text = "Edytuj studenta";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(27, 539);
            this.button2.Margin = new System.Windows.Forms.Padding(4);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(189, 44);
            this.button2.TabIndex = 8;
            this.button2.Text = "Dodaj studenta";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage2.Size = new System.Drawing.Size(1412, 690);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Oceny studentów";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(148, 47);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(873, 418);
            this.button1.TabIndex = 0;
            this.button1.Text = "PIZDA!!!";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.tcAssignmentResults);
            this.tabPage3.Controls.Add(this.pnlAssignmentControls);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Margin = new System.Windows.Forms.Padding(4);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(4);
            this.tabPage3.Size = new System.Drawing.Size(1412, 690);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Automatyczny Przydział do Grup";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // tcAssignmentResults
            // 
            this.tcAssignmentResults.Controls.Add(this.tpUnassignedStudents);
            this.tcAssignmentResults.Controls.Add(this.tpAssignmentLog);
            this.tcAssignmentResults.Controls.Add(this.tpGroupsPreview);
            this.tcAssignmentResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcAssignmentResults.Location = new System.Drawing.Point(4, 124);
            this.tcAssignmentResults.Name = "tcAssignmentResults";
            this.tcAssignmentResults.SelectedIndex = 0;
            this.tcAssignmentResults.Size = new System.Drawing.Size(1404, 562);
            this.tcAssignmentResults.TabIndex = 1;
            // 
            // tpUnassignedStudents
            // 
            this.tpUnassignedStudents.Controls.Add(this.dgvUnassignedStudents);
            this.tpUnassignedStudents.Location = new System.Drawing.Point(4, 25);
            this.tpUnassignedStudents.Name = "tpUnassignedStudents";
            this.tpUnassignedStudents.Padding = new System.Windows.Forms.Padding(3);
            this.tpUnassignedStudents.Size = new System.Drawing.Size(1396, 533);
            this.tpUnassignedStudents.TabIndex = 0;
            this.tpUnassignedStudents.Text = "Nieprzypisani Studenci";
            this.tpUnassignedStudents.UseVisualStyleBackColor = true;
            // 
            // dgvUnassignedStudents
            // 
            this.dgvUnassignedStudents.AllowUserToAddRows = false;
            this.dgvUnassignedStudents.AllowUserToDeleteRows = false;
            this.dgvUnassignedStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvUnassignedStudents.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvUnassignedStudents.Location = new System.Drawing.Point(3, 3);
            this.dgvUnassignedStudents.Name = "dgvUnassignedStudents";
            this.dgvUnassignedStudents.ReadOnly = true;
            this.dgvUnassignedStudents.RowHeadersWidth = 51;
            this.dgvUnassignedStudents.RowTemplate.Height = 24;
            this.dgvUnassignedStudents.Size = new System.Drawing.Size(1390, 527);
            this.dgvUnassignedStudents.TabIndex = 0;
            // 
            // tpAssignmentLog
            // 
            this.tpAssignmentLog.Controls.Add(this.txtAssignmentLog);
            this.tpAssignmentLog.Location = new System.Drawing.Point(4, 25);
            this.tpAssignmentLog.Name = "tpAssignmentLog";
            this.tpAssignmentLog.Padding = new System.Windows.Forms.Padding(3);
            this.tpAssignmentLog.Size = new System.Drawing.Size(1396, 533);
            this.tpAssignmentLog.TabIndex = 1;
            this.tpAssignmentLog.Text = "Log Procesu";
            this.tpAssignmentLog.UseVisualStyleBackColor = true;
            // 
            // txtAssignmentLog
            // 
            this.txtAssignmentLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtAssignmentLog.Location = new System.Drawing.Point(3, 3);
            this.txtAssignmentLog.Multiline = true;
            this.txtAssignmentLog.Name = "txtAssignmentLog";
            this.txtAssignmentLog.ReadOnly = true;
            this.txtAssignmentLog.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtAssignmentLog.Size = new System.Drawing.Size(1390, 527);
            this.txtAssignmentLog.TabIndex = 0;
            // 
            // tpGroupsPreview
            // 
            this.tpGroupsPreview.Controls.Add(this.dgvGroupsPreview);
            this.tpGroupsPreview.Location = new System.Drawing.Point(4, 25);
            this.tpGroupsPreview.Name = "tpGroupsPreview";
            this.tpGroupsPreview.Size = new System.Drawing.Size(1396, 533);
            this.tpGroupsPreview.TabIndex = 2;
            this.tpGroupsPreview.Text = "Podgląd Grup";
            this.tpGroupsPreview.UseVisualStyleBackColor = true;
            // 
            // dgvGroupsPreview
            // 
            this.dgvGroupsPreview.AllowUserToAddRows = false;
            this.dgvGroupsPreview.AllowUserToDeleteRows = false;
            this.dgvGroupsPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvGroupsPreview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvGroupsPreview.Location = new System.Drawing.Point(0, 0);
            this.dgvGroupsPreview.Name = "dgvGroupsPreview";
            this.dgvGroupsPreview.ReadOnly = true;
            this.dgvGroupsPreview.RowHeadersWidth = 51;
            this.dgvGroupsPreview.RowTemplate.Height = 24;
            this.dgvGroupsPreview.Size = new System.Drawing.Size(1396, 533);
            this.dgvGroupsPreview.TabIndex = 0;
            // 
            // pnlAssignmentControls
            // 
            this.pnlAssignmentControls.Controls.Add(this.lblStudentsStillUnassignedCount);
            this.pnlAssignmentControls.Controls.Add(this.lblInfoStillUnassigned);
            this.pnlAssignmentControls.Controls.Add(this.lblStudentsAssignedStep2Count);
            this.pnlAssignmentControls.Controls.Add(this.lblInfoAssignedStep2);
            this.pnlAssignmentControls.Controls.Add(this.lblStudentsAssignedStep1Count);
            this.pnlAssignmentControls.Controls.Add(this.lblInfoAssignedStep1);
            this.pnlAssignmentControls.Controls.Add(this.lblStudentsToProcessCount);
            this.pnlAssignmentControls.Controls.Add(this.lblInfoStudentsToProcess);
            this.pnlAssignmentControls.Controls.Add(this.btnLoadDataForAssignment);
            this.pnlAssignmentControls.Controls.Add(this.btnAssignStep1);
            this.pnlAssignmentControls.Controls.Add(this.btnAssignStep2Redistribute);
            this.pnlAssignmentControls.Controls.Add(this.btnConfirmAssignments);
            this.pnlAssignmentControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlAssignmentControls.Location = new System.Drawing.Point(4, 4);
            this.pnlAssignmentControls.Name = "pnlAssignmentControls";
            this.pnlAssignmentControls.Size = new System.Drawing.Size(1404, 120);
            this.pnlAssignmentControls.TabIndex = 0;
            // 
            // lblStudentsStillUnassignedCount
            // 
            this.lblStudentsStillUnassignedCount.AutoSize = true;
            this.lblStudentsStillUnassignedCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStudentsStillUnassignedCount.Location = new System.Drawing.Point(360, 85);
            this.lblStudentsStillUnassignedCount.Name = "lblStudentsStillUnassignedCount";
            this.lblStudentsStillUnassignedCount.Size = new System.Drawing.Size(15, 16);
            this.lblStudentsStillUnassignedCount.TabIndex = 11;
            this.lblStudentsStillUnassignedCount.Text = "0";
            // 
            // lblInfoStillUnassigned
            // 
            this.lblInfoStillUnassigned.AutoSize = true;
            this.lblInfoStillUnassigned.Location = new System.Drawing.Point(230, 85);
            this.lblInfoStillUnassigned.Name = "lblInfoStillUnassigned";
            this.lblInfoStillUnassigned.Size = new System.Drawing.Size(129, 16);
            this.lblInfoStillUnassigned.TabIndex = 10;
            this.lblInfoStillUnassigned.Text = "Nadal nieprzypisani:";
            // 
            // lblStudentsAssignedStep2Count
            // 
            this.lblStudentsAssignedStep2Count.AutoSize = true;
            this.lblStudentsAssignedStep2Count.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStudentsAssignedStep2Count.Location = new System.Drawing.Point(140, 85);
            this.lblStudentsAssignedStep2Count.Name = "lblStudentsAssignedStep2Count";
            this.lblStudentsAssignedStep2Count.Size = new System.Drawing.Size(15, 16);
            this.lblStudentsAssignedStep2Count.TabIndex = 9;
            this.lblStudentsAssignedStep2Count.Text = "0";
            // 
            // lblInfoAssignedStep2
            // 
            this.lblInfoAssignedStep2.AutoSize = true;
            this.lblInfoAssignedStep2.Location = new System.Drawing.Point(15, 85);
            this.lblInfoAssignedStep2.Name = "lblInfoAssignedStep2";
            this.lblInfoAssignedStep2.Size = new System.Drawing.Size(120, 16);
            this.lblInfoAssignedStep2.TabIndex = 8;
            this.lblInfoAssignedStep2.Text = "Przypisani (Krok 2):";
            // 
            // lblStudentsAssignedStep1Count
            // 
            this.lblStudentsAssignedStep1Count.AutoSize = true;
            this.lblStudentsAssignedStep1Count.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStudentsAssignedStep1Count.Location = new System.Drawing.Point(355, 60);
            this.lblStudentsAssignedStep1Count.Name = "lblStudentsAssignedStep1Count";
            this.lblStudentsAssignedStep1Count.Size = new System.Drawing.Size(15, 16);
            this.lblStudentsAssignedStep1Count.TabIndex = 7;
            this.lblStudentsAssignedStep1Count.Text = "0";
            // 
            // lblInfoAssignedStep1
            // 
            this.lblInfoAssignedStep1.AutoSize = true;
            this.lblInfoAssignedStep1.Location = new System.Drawing.Point(230, 60);
            this.lblInfoAssignedStep1.Name = "lblInfoAssignedStep1";
            this.lblInfoAssignedStep1.Size = new System.Drawing.Size(120, 16);
            this.lblInfoAssignedStep1.TabIndex = 6;
            this.lblInfoAssignedStep1.Text = "Przypisani (Krok 1):";
            // 
            // lblStudentsToProcessCount
            // 
            this.lblStudentsToProcessCount.AutoSize = true;
            this.lblStudentsToProcessCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.lblStudentsToProcessCount.Location = new System.Drawing.Point(175, 60);
            this.lblStudentsToProcessCount.Name = "lblStudentsToProcessCount";
            this.lblStudentsToProcessCount.Size = new System.Drawing.Size(15, 16);
            this.lblStudentsToProcessCount.TabIndex = 5;
            this.lblStudentsToProcessCount.Text = "0";
            // 
            // lblInfoStudentsToProcess
            // 
            this.lblInfoStudentsToProcess.AutoSize = true;
            this.lblInfoStudentsToProcess.Location = new System.Drawing.Point(15, 60);
            this.lblInfoStudentsToProcess.Name = "lblInfoStudentsToProcess";
            this.lblInfoStudentsToProcess.Size = new System.Drawing.Size(176, 16);
            this.lblInfoStudentsToProcess.TabIndex = 4;
            this.lblInfoStudentsToProcess.Text = "Studentów do przetworzenia:";
            // 
            // btnLoadDataForAssignment
            // 
            this.btnLoadDataForAssignment.Location = new System.Drawing.Point(15, 15);
            this.btnLoadDataForAssignment.Name = "btnLoadDataForAssignment";
            this.btnLoadDataForAssignment.Size = new System.Drawing.Size(180, 30);
            this.btnLoadDataForAssignment.TabIndex = 0;
            this.btnLoadDataForAssignment.Text = "Załaduj/Odśwież Dane";
            this.btnLoadDataForAssignment.UseVisualStyleBackColor = true;
            // 
            // btnAssignStep1
            // 
            this.btnAssignStep1.Location = new System.Drawing.Point(215, 15);
            this.btnAssignStep1.Name = "btnAssignStep1";
            this.btnAssignStep1.Size = new System.Drawing.Size(231, 30);
            this.btnAssignStep1.TabIndex = 1;
            this.btnAssignStep1.Text = "Krok 1: Pierwsza Preferencja";
            this.btnAssignStep1.UseVisualStyleBackColor = true;
            // 
            // btnAssignStep2Redistribute
            // 
            this.btnAssignStep2Redistribute.Location = new System.Drawing.Point(466, 15);
            this.btnAssignStep2Redistribute.Name = "btnAssignStep2Redistribute";
            this.btnAssignStep2Redistribute.Size = new System.Drawing.Size(203, 30);
            this.btnAssignStep2Redistribute.TabIndex = 2;
            this.btnAssignStep2Redistribute.Text = "Krok 2: Redystrybucja";
            this.btnAssignStep2Redistribute.UseVisualStyleBackColor = true;
            // 
            // btnConfirmAssignments
            // 
            this.btnConfirmAssignments.Location = new System.Drawing.Point(684, 15);
            this.btnConfirmAssignments.Name = "btnConfirmAssignments";
            this.btnConfirmAssignments.Size = new System.Drawing.Size(200, 30);
            this.btnConfirmAssignments.TabIndex = 3;
            this.btnConfirmAssignments.Text = "Zatwierdź i Zapisz Przydziały";
            this.btnConfirmAssignments.UseVisualStyleBackColor = true;
            // 
            // przedmiotyBindingSource
            // 
            this.przedmiotyBindingSource.DataMember = "Przedmioty";
            this.przedmiotyBindingSource.DataSource = this.studentManagementDataSet;
            // 
            // przedmiotyTableAdapter
            // 
            this.przedmiotyTableAdapter.ClearBeforeFill = true;
            // 
            // ocenyStudentowBindingSource
            // 
            this.ocenyStudentowBindingSource.DataMember = "OcenyStudentow";
            this.ocenyStudentowBindingSource.DataSource = this.studentManagementDataSet1;
            // 
            // ocenyStudentowTableAdapter
            // 
            this.ocenyStudentowTableAdapter.ClearBeforeFill = true;
            // 
            // specjalizacjeBindingSource
            // 
            this.specjalizacjeBindingSource.DataMember = "Specjalizacje";
            this.specjalizacjeBindingSource.DataSource = this.studentManagementDataSet1;
            // 
            // specjalizacjeTableAdapter
            // 
            this.specjalizacjeTableAdapter.ClearBeforeFill = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1452, 718);
            this.Controls.Add(this.tabControl1);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "UniManagement System";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.studentManagementDataSet1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studenciBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentsDataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studenciBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.studentManagementDataSet)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bindingNavigator1)).EndInit();
            this.bindingNavigator1.ResumeLayout(false);
            this.bindingNavigator1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownAlbumNumber)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tcAssignmentResults.ResumeLayout(false);
            this.tpUnassignedStudents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvUnassignedStudents)).EndInit();
            this.tpAssignmentLog.ResumeLayout(false);
            this.tpAssignmentLog.PerformLayout();
            this.tpGroupsPreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvGroupsPreview)).EndInit();
            this.pnlAssignmentControls.ResumeLayout(false);
            this.pnlAssignmentControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.przedmiotyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ocenyStudentowBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.specjalizacjeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private StudentManagementDataSet1 studentManagementDataSet1;
        private System.Windows.Forms.BindingSource studenciBindingSource;
        private StudentManagementDataSet1TableAdapters.StudenciTableAdapter studenciTableAdapter;
        private StudentManagementDataSet1TableAdapters.TableAdapterManager tableAdapterManager;
        private System.Windows.Forms.DataGridView studentsDataGridView;
        private StudentManagementDataSet studentManagementDataSet;
        private System.Windows.Forms.BindingSource studenciBindingSource1;
        private StudentManagementDataSetTableAdapters.StudenciTableAdapter studenciTableAdapter1;
        private StudentManagementDataSetTableAdapters.TableAdapterManager tableAdapterManager1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ComboBox comboBoxSpecialization3;
        private System.Windows.Forms.ComboBox comboBoxSpecialization2;
        private System.Windows.Forms.ComboBox comboBoxSpecialization1;
        private System.Windows.Forms.TextBox textBoxSurname;
        private System.Windows.Forms.NumericUpDown numericUpDownAlbumNumber;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.BindingNavigator bindingNavigator1;
        private System.Windows.Forms.ToolStripButton BtnAddStudent;
        private System.Windows.Forms.ToolStripLabel bindingNavigatorCountItem;
        private System.Windows.Forms.ToolStripButton btnDeleteStudent;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveFirstItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMovePreviousItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator;
        private System.Windows.Forms.ToolStripTextBox bindingNavigatorPositionItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveNextItem;
        private System.Windows.Forms.ToolStripButton bindingNavigatorMoveLastItem;
        private System.Windows.Forms.ToolStripSeparator bindingNavigatorSeparator2;
        private System.Windows.Forms.ToolStripButton btnUpdateStudent;
        private System.Windows.Forms.TextBox textBoxEmail;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox comboBoxStatus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.BindingSource przedmiotyBindingSource;
        private StudentManagementDataSetTableAdapters.PrzedmiotyTableAdapter przedmiotyTableAdapter;
        private System.Windows.Forms.BindingSource ocenyStudentowBindingSource;
        private StudentManagementDataSet1TableAdapters.OcenyStudentowTableAdapter ocenyStudentowTableAdapter;
        private System.Windows.Forms.BindingSource specjalizacjeBindingSource;
        private StudentManagementDataSet1TableAdapters.SpecjalizacjeTableAdapter specjalizacjeTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn studentIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn numerAlbumuDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn imieDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nazwiskoDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn emailDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn specjalizacjaIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn sredniaOcenKwalifikacyjnaDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn statusKwalifikacjiDataGridViewTextBoxColumn;

        // --- DEKLARACJE NOWYCH KONTROLEK (umieść je tutaj) ---
        private System.Windows.Forms.Panel pnlAssignmentControls;
        private System.Windows.Forms.Button btnLoadDataForAssignment;
        private System.Windows.Forms.Button btnAssignStep1; // Dawny button5
        private System.Windows.Forms.Button btnAssignStep2Redistribute; // Dawny button6
        private System.Windows.Forms.Button btnConfirmAssignments; // Dawny button7
        private System.Windows.Forms.Label lblInfoStudentsToProcess;
        private System.Windows.Forms.Label lblStudentsToProcessCount;
        private System.Windows.Forms.Label lblInfoAssignedStep1;
        private System.Windows.Forms.Label lblStudentsAssignedStep1Count;
        private System.Windows.Forms.Label lblInfoAssignedStep2;
        private System.Windows.Forms.Label lblStudentsAssignedStep2Count;
        private System.Windows.Forms.Label lblInfoStillUnassigned;
        private System.Windows.Forms.Label lblStudentsStillUnassignedCount;
        private System.Windows.Forms.TabControl tcAssignmentResults;
        private System.Windows.Forms.TabPage tpUnassignedStudents;
        private System.Windows.Forms.DataGridView dgvUnassignedStudents; // Dawny dataGridView1
        private System.Windows.Forms.TabPage tpAssignmentLog;
        private System.Windows.Forms.TextBox txtAssignmentLog;
        private System.Windows.Forms.TabPage tpGroupsPreview;
        private System.Windows.Forms.DataGridView dgvGroupsPreview;
        // --- KONIEC DEKLARACJI NOWYCH KONTROLEK ---
    }
}