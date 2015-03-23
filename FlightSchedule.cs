using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AirlineApplication
{
    public partial class FlightSchedule : Form
    {
        public FlightSchedule()
        {
            InitializeComponent();
            label1.Text = "Flights for " + DateTime.Today.ToString("yyyy/MM/dd");
        }

        private void FlightSchedule_Load(object sender, EventArgs e)
        {
           //load the current flights
            
                FlightClass flight = new FlightClass();

                dataGridView1.DataSource = flight.flightSchedule();

                flight.dbcon.CloseConnection();

                this.Refresh();

            //here i check if there are flights or no, if there are NO flight it means one row so display this message
               if (dataGridView1.Rows.Count == 1)
                {
                    MessageBox.Show("There are no flights for today", "No flights", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            
    
        }
    }
}
