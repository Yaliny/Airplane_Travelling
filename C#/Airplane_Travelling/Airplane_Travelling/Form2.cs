using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Airplane_Travelling
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        class Plane
        {
            private int planeID;
            private int capacity;
            private int startAirportID;
            private char direction;
            private int midAirportID;
            private int destAirportID;
            private int hoursLeft;
            private int fuelLeft;
            private bool isRefueled;
            private bool passedMidAirport;
            public void Input(string[] data)
            {
                planeID = Convert.ToInt16(data[0]);
                capacity = Convert.ToInt16(data[1]);
                startAirportID = Convert.ToInt16(data[2]);
                direction = Convert.ToChar(data[3]);
            }
            public int MidAirportID
            {
                get { return midAirportID; }
                set { midAirportID = value; }
            }
            public int DestAirportID
            {
                get { return destAirportID; }
                set { destAirportID = value; }
            }
            public int HoursLeft
            {
                get { return hoursLeft; }
                set { hoursLeft = value; }
            }
            public int FuelLeft
            {
                get { return fuelLeft; }
                set { fuelLeft = value; }
            }
            public bool IsRefueled
            {
                get { return isRefueled; }
                set { isRefueled = value; }
            }
            public bool PassedMidAirport
            {
                get { return passedMidAirport; }
                set { passedMidAirport = value; }
            }
        }

        class Airport
        {
            private int airportID;
            private int A;
            private int distanceToA;
            private int B;
            private int distanceToB;
            public void Input(int[] data)
            {
                airportID = data[0];
                A = data[1];
                distanceToA = data[2];
                B = data[3];
                distanceToB = data[4];
            }
            public int GetAirportID()
            {
                return airportID;
            }
            public int GetA()
            {
                return A;
            }
            public int GetDistanceToA()
            {
                return distanceToA;
            }
            public int GetB()
            {
                return B;
            }
            public int GetDistanceToB()
            {
                return distanceToB;
            }
        }

        List<Plane> planes; //list of planes
        List<Airport> airports; //list of airports

        private void Form2_Shown(object sender, EventArgs e)
        {
            //declaration of the flightmap table
            dataGridView1.Columns.Add("startID","Start Airport");
            dataGridView1.Columns.Add("direct", "Direction");
            dataGridView1.Columns.Add("midID", "Middle Airport");
            dataGridView1.Columns.Add("distToMA", "Distance to MA, h");
            dataGridView1.Columns.Add("destID", "Destination Airport");
            dataGridView1.Columns.Add("distToDA", "Distance to DA, h");

            //output of number of planes and airports
            label6.Text = Form1.numberOfAirports.ToString();
            label7.Text = Form1.numberOfPlanes.ToString();

            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                col.Width = 75;
            }

            //initializanion of the planes list with data from input file
            planes = new List<Plane>();
            for (int i = 0; i < Form1.numberOfPlanes; i++)
            {
                planes.Add(new Plane());
                planes[i].Input(Form1.scenario[i]);
            }

            //initializanion of the airports list with data from input file
            airports = new List<Airport>();
            for (int i = 0; i < Form1.numberOfAirports; i++)
            {
                airports.Add(new Airport());
                airports[i].Input(Form1.topology[i]);
            }

            //initialization of the flightmap table
            foreach (Airport a in airports)
            {
                //calculation of the entire route in A direction
                dataGridView1.Rows.Add();
                int rowIndex = dataGridView1.Rows.Count - 1;
                dataGridView1.Rows[rowIndex].Cells[0].Value = a.GetAirportID();
                dataGridView1.Rows[rowIndex].Cells[1].Value = "A";
                dataGridView1.Rows[rowIndex].Cells[2].Value = a.GetA();
                dataGridView1.Rows[rowIndex].Cells[3].Value = a.GetDistanceToA();
                Airport destinationA = airports.Find(x => x.GetAirportID() == a.GetA());
                if (destinationA.GetA() == a.GetAirportID())
                {
                    dataGridView1.Rows[rowIndex].Cells[4].Value = destinationA.GetB();
                    dataGridView1.Rows[rowIndex].Cells[5].Value = destinationA.GetDistanceToB();
                }
                else
                {
                    dataGridView1.Rows[rowIndex].Cells[4].Value = destinationA.GetA();
                    dataGridView1.Rows[rowIndex].Cells[5].Value = destinationA.GetDistanceToA();
                }

                //calculation of the entire route in B direction
                dataGridView1.Rows.Add();
                rowIndex = dataGridView1.Rows.Count - 1;
                dataGridView1.Rows[rowIndex].Cells[0].Value = a.GetAirportID();
                dataGridView1.Rows[rowIndex].Cells[1].Value = "B";
                dataGridView1.Rows[rowIndex].Cells[2].Value = a.GetB();
                dataGridView1.Rows[rowIndex].Cells[3].Value = a.GetDistanceToB();
                Airport destinationB = airports.Find(x => x.GetAirportID() == a.GetB());
                if (destinationB.GetA() == a.GetAirportID())
                {
                    dataGridView1.Rows[rowIndex].Cells[4].Value = destinationB.GetB();
                    dataGridView1.Rows[rowIndex].Cells[5].Value = destinationB.GetDistanceToB();
                }
                else
                {
                    dataGridView1.Rows[rowIndex].Cells[4].Value = destinationB.GetA();
                    dataGridView1.Rows[rowIndex].Cells[5].Value = destinationB.GetDistanceToA();
                }
            }

            //initialize plane calculated parameters
        }

        //SIMULATION
    }
}
