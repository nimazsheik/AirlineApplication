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
    public partial class SearchCustomer : Form
    {
        DatabaseConnector dbcon = new DatabaseConnector(); //directly connect to db
        DataTable custDataTable;        //so this variable can be accessed anywhere in this form

        public SearchCustomer()
        {
            InitializeComponent();

        }



        private void SearchCustomer_Load(object sender, EventArgs e)
        {//trying new method
            //load db here
     
     
            dbcon.OpenConnection();

            custDataTable = dbcon.GetData("SELECT * FROM customer"); //this returns a datatable assign it to custdatatable variable
            dataGridView1.DataSource = custDataTable;
            dbcon.CloseConnection();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
         //on keyup this event is called
            string displayInfo = "";
            string[] keyWords = textBox1.Text.Split(' ');  //first take text and split into words
            foreach(string word in keyWords) //do a LIKE Comparison to each search field
            {
                if(displayInfo.Length ==0)          //create and assign filter statement to do comparison
                {
                    displayInfo = "(fname LIKE '%" + word+ "%' OR lname LIKE '%" + word+ "%')";
                }
                else
                { //here we AND each filter statement (eg Sh fa will not give any result coz two diff fields but sh ni will give result)
                    displayInfo += " AND (fname LIKE '%" + word+ "%' OR lname LIKE '%" + word+ "%')";
                }
            }

            //apply filter to dataview
            
            //using the same dbcon object 
            dbcon.myview = custDataTable.DefaultView;
            dbcon.myview.RowFilter = displayInfo;
           
            
         

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
