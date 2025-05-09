using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UniManagement
{
    public partial class Form1 : Form
    {
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
    }
}
