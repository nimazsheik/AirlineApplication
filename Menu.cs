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
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            CancelBook cb = new CancelBook();
            cb.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //show the customer form
            Customer c1 = new Customer();
            c1.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //display the flight schedule(the one shown on screens)
            FlightSchedule fs1 = new FlightSchedule();
            fs1.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //show total ticket sales and if flight is fully booked or not
            FlightStatus ts = new FlightStatus();
            ts.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AddFlight af = new AddFlight();
            af.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SearchFlight sf = new SearchFlight();
            sf.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            AddPlane ap = new AddPlane();
            ap.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            SearchCustomer sc = new SearchCustomer();
            sc.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ShowPlanes sp = new ShowPlanes();
            sp.Show();
        }

   

        
    }
}
