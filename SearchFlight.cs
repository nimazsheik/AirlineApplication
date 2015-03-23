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
    public partial class SearchFlight : Form
    {
        //variables
        DataTable fltDataTable;
        DatabaseConnector dbcon = new DatabaseConnector();

        //this is to search from location
        string srcLocation;
        string dstLocation;

        public SearchFlight()
        {
            InitializeComponent();
        }



        private void SearchFlight_Load(object sender, EventArgs e)
        {
            loadFlight();
        }

        private void loadFlight()
        {//add try catch statements


            dbcon.OpenConnection();

            fltDataTable = dbcon.GetData("SELECT * FROM flight"); //this returns a datatable assign it to custdatatable variable
            dataGridFlight.DataSource = fltDataTable;
            dbcon.CloseConnection();

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            string displayInfo = "";
            srcLocation = txtFrom.Text;
            displayInfo = "(source_location LIKE '%" + srcLocation + "%' AND dest_location LIKE '%"+dstLocation+"%')";  //FROM code
            dbcon.myview = fltDataTable.DefaultView;
            dbcon.myview.RowFilter = displayInfo;
        }

        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            string displayInfo = "";
            dstLocation = txtTo.Text;
            displayInfo = "(source_location LIKE '%" + srcLocation + "%' AND dest_location LIKE '%" + dstLocation + "%')";  //TO code
            dbcon.myview = fltDataTable.DefaultView;
            dbcon.myview.RowFilter = displayInfo;
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            string displayInfo = "";

            if (string.IsNullOrEmpty(this.txtFlNum.Text))
            {
                //MessageBox.Show("e");
                loadFlight();
            }
            else
            {

                displayInfo = "(flight_id=" + txtFlNum.Text + ")";
                dbcon.myview = fltDataTable.DefaultView;
                dbcon.myview.RowFilter = displayInfo;
                //if i use equal there is an error when = null cant use LIKE either so i add 0 default is 0
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
