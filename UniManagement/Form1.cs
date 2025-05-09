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
            LoadStatuses();
        }

       

        private void LoadStatuses()
        {
            comboBoxStatus.Items.Add("Oczekujący");
            comboBoxStatus.Items.Add("Zakwalifikowany");
            comboBoxStatus.Items.Add("Odrzucony");
            comboBoxStatus.SelectedIndex = 0;
        }


        private void BtnAddStudent_Click(object sender, EventArgs e)
        {

        }

        private void btnUpdateStudent_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteStudent_Click(object sender, EventArgs e)
        {

        }



        private void panel2_Paint(object sender, PaintEventArgs e) { }
        private void button1_Click(object sender, EventArgs e) { }
        private void button2_Click(object sender, EventArgs e) { }
        private void button3_Click(object sender, EventArgs e) { }
        private void sortStudents1_Load(object sender, EventArgs e) { }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void button2_Click_1(object sender, EventArgs e) { }
    }
}