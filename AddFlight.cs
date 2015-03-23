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
    public partial class AddFlight : Form
    {

      
        
        public AddFlight()
        {
            InitializeComponent();

            //set the format for the date and time in date time pickers
            dtpDept.Format = DateTimePickerFormat.Custom;
            dtpDept.CustomFormat = "yyyy-MM-dd HH:mm";
            
            dtpArrive.Format = DateTimePickerFormat.Custom;
            dtpArrive.CustomFormat = "yyyy-MM-dd HH:mm";
        }

       
        private void button1_Click(object sender, EventArgs e)
        {//for the fare put a default minimum value
            try
            {       //validation to check if null or not
                var controls = new[] { txtSrc.Text, txtVia.Text, txtDest.Text, txtMod.Text };
                if (!controls.All(x => string.IsNullOrEmpty(x)))
                {

                    //ADD VALIDATION FOR DATE- check if dates are in the future
                    if ((this.dtpDept.Value.Date > DateTime.Today.Date) && (this.dtpArrive.Value.Date > DateTime.Today.Date))
                    {//date validation 2
                        if (this.dtpDept.Value.Date < this.dtpArrive.Value.Date)
                        {
                            //button to save
                            FlightClass fl = new FlightClass();
                            fl.SourceLocation = txtSrc.Text;
                            fl.Via = txtVia.Text;
                            fl.DestLocation = txtDest.Text;
                            fl.DepartDate = dtpDept.Value.Date;
                            fl.DepartTime = dtpDept.Value.ToShortTimeString();
                            fl.ArrivalDate = dtpArrive.Value.Date;
                            fl.ArrivalTime = dtpArrive.Value.ToShortTimeString();
                            fl.PlaneName = txtMod.Text;   //try to put combo box from plane table
                            fl.Fare = int.Parse(numFare.Text);

                             fl.addFlight();
                            MessageBox.Show("Flight for " + fl.PlaneName + " on " + fl.DepartDate + " added", "Success!");
                        }
                        else
                        {//date validation 2 end
                            MessageBox.Show("Departure date should be before the arrival date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {//date validation end
                        MessageBox.Show("Arrival Date or Departure date cannot be set as past date or current date", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {//not null validation end
                    MessageBox.Show("Make sure all fields are filled","Error",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Error :" + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ShowPlanes shwPln = new ShowPlanes();
            this.Close();
            shwPln.Show();
        }

        private void AddFlight_Load(object sender, EventArgs e)
        {
           //onload 
        }

        public void showPlane( int i)
        {
            string planeTitle = NormalPlane.planeObj[i].ModelNumber;
            txtMod.Text = planeTitle;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


    }
}
