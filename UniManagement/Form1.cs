﻿using System;
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
            studentManagement1.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            studentManagement1.BringToFront();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            studentGrades1.BringToFront();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sortStudents1.BringToFront();
        }

        private void sortStudents1_Load(object sender, EventArgs e)
        {

        }
    }
}
