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
    public partial class CancelBook : Form
    {
        DatabaseConnector dbcon = new DatabaseConnector(); //same method as search customer
        DataTable cancelTable;
        public CancelBook()
        {
            InitializeComponent();
        }

        private void CancelBook_Load(object sender, EventArgs e)
        {//show all the booked seats
            loadBook();
        }
        private void loadBook()
        {
            dbcon.OpenConnection();

            cancelTable = dbcon.GetData("SELECT booking.bookingID,booking.customerID,CONCAT(seatRow,seatNumber) as seat FROM booking INNER JOIN seat on booking.bookingID = seat.bookingID WHERE booking.cancelled=0"); //only show the records which are not cancelled(cancelled=0)
            dataGridView1.DataSource = cancelTable;
            dbcon.CloseConnection();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                //on keyup this event is called
                string displayInfo = "";

                if (string.IsNullOrEmpty(this.textBox1.Text))
                {
                    //MessageBox.Show("e");
                    loadBook();
                }
                else
                {

                    displayInfo = "(bookingID=" + textBox1.Text + ")";
                    dbcon.myview = cancelTable.DefaultView;
                    dbcon.myview.RowFilter = displayInfo;
                    //if i use equal bug when = null cant use LIKE either so i add 0 default is 0
                }


                //apply filter to dataview

                //using the same dbcon object 
            }
            catch (Exception)
            {
                MessageBox.Show("Please enter only numeric digits to filter the bookingID","Only digits should be entered", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox1.Text))
            {
                MessageBox.Show("Please make sure to enter the Booking ID you wish to cancel in the text box", "Empty BookingID", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                BookingClass bookClass = new BookingClass();
                bookClass.BookingID = Convert.ToInt32(textBox1);
                bookClass.cancelBooking(bookClass.BookingID);
                MessageBox.Show("Booking successfully cancelled", "Booking Cancelled", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
