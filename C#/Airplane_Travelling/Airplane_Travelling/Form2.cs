using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;

namespace Airplane_Travelling
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        public static string logFilePath;
        public static int simulationTime;
        public static int terminalStatePlanes;

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
            private bool terminalStateReached;

            public void Input(string[] data)
            {
                planeID = Convert.ToInt16(data[0]);
                capacity = Convert.ToInt16(data[1]);
                startAirportID = Convert.ToInt16(data[2]);
                direction = Convert.ToChar(data[3]);
            }

            //Setters and getters for Plane class
            public int PlaneID
            {
                get { return planeID; }
                set { planeID = value; }
            }
            public int Capacity
            {
                get { return capacity; }
                set { capacity = value; }
            }
            public int StartAirportID
            {
                get { return startAirportID; }
                set { startAirportID = value; }
            }
            public char Direction
            {
                get { return direction; }
                set { direction = value; }
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
            public bool TerminalStateReached
            {
                get { return terminalStateReached; }
                set { terminalStateReached = value; }
            }
            public void DEPARTURE(TextBox output)
            {
                output.AppendText(Environment.NewLine + "Plane " + planeID + " departed from airport " + startAirportID);
                using (StreamWriter log = File.AppendText(logFilePath))
                {
                    log.WriteLine(simulationTime.ToString() + "h: Plane " + planeID + " departed from airport " + startAirportID);
                }
            }
            public void ARRIVAL (TextBox output)
            {
                terminalStateReached = true;
                terminalStatePlanes++;
                output.AppendText(Environment.NewLine + "Plane " + planeID + " arrived to the airport " + destAirportID);
                using (StreamWriter log = File.AppendText(logFilePath))
                {
                    log.WriteLine(simulationTime.ToString() + "h: Plane " + planeID + " arrived to the airport " + destAirportID);
                }
            }
            public void CRASH(TextBox output)
            {
                terminalStateReached = true;
                terminalStatePlanes++;
                output.AppendText(Environment.NewLine + "Plane " + planeID + " crashed!");
                using (StreamWriter log = File.AppendText(logFilePath))
                {
                    log.WriteLine("<" + simulationTime.ToString() + "h><" + planeID + "><-><-><"+ isRefueled +"><CRASHED>");
                }
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

            public void REFUEL(Plane p, char from, TextBox output)
            {
                p.PassedMidAirport = true;
                double percentage = (double)p.FuelLeft/p.Capacity;
                //if the plane has 50% of fuel or less, refuel it
                if (percentage <= 0.5)
                {
                    p.IsRefueled = true;
                    p.FuelLeft = p.Capacity;
                    output.AppendText(Environment.NewLine + "Plane " + p.PlaneID + " is refueled at the airport " + airportID);
                    using (StreamWriter log = File.AppendText(logFilePath))
                    {
                        log.WriteLine("<" + simulationTime.ToString() + "h><" + p.PlaneID + "><" + airportID + "><" + from + "><"+p.IsRefueled+"><REFUELING>");
                    }
                }
                else //otherwise, decline the refueling
                {
                    output.AppendText(Environment.NewLine + "Refueling of the plane " + p.PlaneID + " at the airport " + airportID + " is not confirmed.");
                    using (StreamWriter log = File.AppendText(logFilePath))
                    {
                        log.WriteLine("<" + simulationTime.ToString() + "h><" + p.PlaneID + "><" + airportID + "><" + from + "><" + p.IsRefueled + "><PASSED BY>");
                    }
                    if (A == p.DestAirportID)
                        p.HoursLeft = distanceToA;
                    else
                        p.HoursLeft = distanceToB;
                }
            }

            //Setters and getters for Airport class
            public int AirportID
            {
                get { return airportID; }
                set { airportID = value; }
            }
            public int AID
            {
                get { return A; }
                set { A = value; }
            }
            public int DistanceToA
            {
                get { return distanceToA; }
                set { distanceToA = value; }
            }
            public int BID
            {
                get { return B; }
                set { B = value; }
            }
            public int DistanceToB
            {
                get { return distanceToB; }
                set { distanceToB = value; }
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
            //(instead of dataGridView these data can be stored in simple matrix)
            foreach (Airport a in airports)
            {
                //calculation of the entire route in A direction
                dataGridView1.Rows.Add();
                int rowIndex = dataGridView1.Rows.Count - 1;
                dataGridView1.Rows[rowIndex].Cells[0].Value = a.AirportID;
                dataGridView1.Rows[rowIndex].Cells[1].Value = "A";
                dataGridView1.Rows[rowIndex].Cells[2].Value = a.AID;
                dataGridView1.Rows[rowIndex].Cells[3].Value = a.DistanceToA;
                Airport destinationA = airports.Find(x => x.AirportID == a.AID);
                if (destinationA.AID == a.AirportID)
                {
                    dataGridView1.Rows[rowIndex].Cells[4].Value = destinationA.BID;
                    dataGridView1.Rows[rowIndex].Cells[5].Value = destinationA.DistanceToB;
                }
                else
                {
                    dataGridView1.Rows[rowIndex].Cells[4].Value = destinationA.AID;
                    dataGridView1.Rows[rowIndex].Cells[5].Value = destinationA.DistanceToA;
                }

                //calculation of the entire route in B direction
                dataGridView1.Rows.Add();
                rowIndex = dataGridView1.Rows.Count - 1;
                dataGridView1.Rows[rowIndex].Cells[0].Value = a.AirportID;
                dataGridView1.Rows[rowIndex].Cells[1].Value = "B";
                dataGridView1.Rows[rowIndex].Cells[2].Value = a.BID;
                dataGridView1.Rows[rowIndex].Cells[3].Value = a.DistanceToB;
                Airport destinationB = airports.Find(x => x.AirportID == a.BID);
                if (destinationB.AID == a.AirportID)
                {
                    dataGridView1.Rows[rowIndex].Cells[4].Value = destinationB.BID;
                    dataGridView1.Rows[rowIndex].Cells[5].Value = destinationB.DistanceToB;
                }
                else
                {
                    dataGridView1.Rows[rowIndex].Cells[4].Value = destinationB.AID;
                    dataGridView1.Rows[rowIndex].Cells[5].Value = destinationB.DistanceToA;
                }
            }

            //initialize plane calculated parameters
            foreach (Plane p in planes)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[0].Value.ToString().Equals(p.StartAirportID.ToString())
                        && row.Cells[1].Value.ToString().Equals(p.Direction.ToString()))
                    {
                        p.MidAirportID = Convert.ToInt16(row.Cells[2].Value);
                        p.HoursLeft = Convert.ToInt16(row.Cells[3].Value);
                        p.DestAirportID = Convert.ToInt16(row.Cells[4].Value);
                        break;
                    }
                }
                p.FuelLeft = p.Capacity;
            }
        }

        //SIMULATION
        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            simulationTime = 0;
            logFilePath = "log" + DateTime.Now.Ticks + ".txt";
            terminalStatePlanes = 0;

            using (StreamWriter log = File.CreateText(logFilePath))
            {
                log.WriteLine("Airplane travelling simulation started. Current time: "+DateTime.Now);
                log.WriteLine("Simulation parameters: {0} planes, {1} airports.", Form1.numberOfPlanes, Form1.numberOfAirports);
                log.WriteLine("Simulation time scale is: 1 hour = 1 second.");
                log.WriteLine("");
            }

            textBox1.AppendText("Airplane travelling simulation started. Current time: " + DateTime.Now);
            textBox1.AppendText(Environment.NewLine + "Simulation parameters: "+ Form1.numberOfPlanes + " planes, "+ Form1.numberOfAirports + " airports.");
            textBox1.AppendText(Environment.NewLine + "Simulation time scale is: 1 second = 1 hour.");
            textBox1.AppendText(Environment.NewLine);

            foreach (Plane p in planes)
            {
                p.DEPARTURE(textBox1);
            }

            Thread.Sleep(1000);
            simulationTime++;

            while (terminalStatePlanes < Form1.numberOfPlanes)
            {
                textBox1.AppendText(Environment.NewLine + "Events in " + simulationTime.ToString() + " hour:");
                foreach (Plane p in planes)
                {
                    if (!p.TerminalStateReached) //if the plane is active
                    {
                        if (p.IsRefueled && p.HoursLeft == 0) //if the plane was refilled
                        {
                            Airport fuelStation = airports.Find(x => x.AirportID == p.MidAirportID);
                            //it departs from fuel station and continues the flight
                            if (fuelStation.AID == p.DestAirportID)
                                p.HoursLeft = fuelStation.DistanceToA;
                            else
                                p.HoursLeft = fuelStation.DistanceToB;
                        }
                        else
                        {
                            //every plane covers 1h of the flight and spends 100 l of fuel
                            p.HoursLeft = p.HoursLeft - 1;
                            p.FuelLeft = p.FuelLeft - 100;

                            //if the plane reaches the destination airport
                            if (p.PassedMidAirport && p.HoursLeft == 0)
                            {
                                p.ARRIVAL(textBox1);
                            }

                            //if the plane reaches the middle airport, it requests refueling
                            else if (!p.PassedMidAirport && p.HoursLeft == 0)
                            {
                                char from;
                                Airport fuelStation = airports.Find(x => x.AirportID == p.MidAirportID);
                                if (fuelStation.AID == p.StartAirportID)
                                    from = 'A';
                                else
                                    from = 'B';
                                //airport decides whether to refuel the plane or no
                                fuelStation.REFUEL(p, from, textBox1);
                            }

                            //if the plane runs out of fuel, it crashes
                            else if (p.FuelLeft == 0)
                            {
                                p.CRASH(textBox1);
                            }
                        }
                    }
                }
                Thread.Sleep(1000);
                simulationTime++;
            }
            button2.Visible = true;
            button3.Visible = true;
            button1.Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start("notepad.exe", logFilePath);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //destroy form
            Dispose(true);
            this.Dispose();
        }
    }
}
