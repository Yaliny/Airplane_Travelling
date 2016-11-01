using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Airplane_Travelling
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        public static int[][] topology;
        public static int numberOfAirports;
        public static string[][] scenario;
        public static int numberOfPlanes;

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                //read the first line of topology file and store the value as numberOfAirports
                numberOfAirports = Convert.ToInt32(File.ReadLines(textBox1.Text).First());
                //read the rest of topology file and store it to array of lines, each line as an array of integers
                topology = File.ReadAllLines(textBox1.Text).Skip(1).Select(l => l.Split(' ').Select(i => int.Parse(i)).ToArray()).ToArray();

                //read the first line of scenario file and store the value as numberOfPlanes
                numberOfPlanes = Convert.ToInt32(File.ReadLines(textBox2.Text).First());
                //read the rest of scenario file and store it to array of lines, each line as an array of strings
                scenario = File.ReadAllLines(textBox2.Text).Skip(1).Select(l => l.Split(' ').ToArray()).ToArray();

                //open simulation screen
                Form2 f2 = new Form2();
                f2.Show();

                //close input screen
                //this.Close();
            }
            catch (Exception ex) //Prevent exceptions
            {
                if (ex is ArgumentException || ex is DirectoryNotFoundException)
                    MessageBox.Show("Invalid file path. Check the path and try again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ex is FileNotFoundException)
                    MessageBox.Show("File not found. Check the path and try again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (ex is IndexOutOfRangeException || ex is InvalidDataException)
                    MessageBox.Show("Invalid data. Check data files and try again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else
                    MessageBox.Show("An error ocured while reading the data. Check path and files' data and try again.", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
