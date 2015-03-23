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
    public partial class BookingForm : Form
    {
        //DatabaseConnector dbconn;
        BookingClass bookClass;
        int flID;
        double totFare;
        int[,] seatStat;
       // string plnName;
        int ticketNumber;

        public BookingForm()
        {
            InitializeComponent();
           // cmbPmt.SelectedIndex = 0;
           // cmbClass.SelectedIndex = 0;
        }

        private void BookingForm_Load(object sender, EventArgs e) 
        {
                       
            bookClass = new BookingClass();
                   
           //get the flightID from bookseatplan to here, then use that flight id to get remaining details, from,fare and to
            bookClass.FlightID = txtFlight.Text;
            
            bookClass.getFlightDetails(bookClass.FlightID);

            lblFrom.Text = bookClass.From; 
            lblTo.Text = bookClass.To;
            txtFare.Text = totFare.ToString();
            
        }

        
        public void getBooking(int flightID,double totalFare,int[,] seatStatus,int custID)
        {
            try
            {
                //getBooking details from bookseatplan
                bookClass = new BookingClass();
                ticketNumber = bookClass.getTicketNumber();
                flID = flightID;
                totFare = totalFare;
                seatStat = seatStatus;
                txtCustomer.Text = custID.ToString();
                txtFlight.Text = flID.ToString();

                //plnName = planeName;
                listBox1.Items.Clear();
                listBox1.Items.Add("Ticket Number is " + ticketNumber);
                listBox1.Items.Add("________________________________");
                listBox1.Items.Add("Flight Number " + flID);
                listBox1.Items.Add("");
                listBox1.Items.Add("Seat Details");
                listBox1.Items.Add("");
                listBox1.Items.Add("Total Fare For Flight : " + totFare);

                //going to calculate number of seat here

                bookClass.NumSeats = 0;
                //now gonna add seats selected
                listBox1.Items.Add("");
                for (int j = 1; j <= 7; j++)
                {
                    for (int i = 1; i <= 35; i++)
                    {
                        if (seatStatus[i, j] == 2)//whatever seat is chosen
                        {
                            char c = Convert.ToChar(64 + j);
                            listBox1.Items.Add("Row " + c + " Seat " + i);
                            //add count for seats
                            bookClass.NumSeats++;
                        }
                    }
                }
                lblSeat.Text = bookClass.NumSeats.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Error, Please close booking form and try again\r\n"+ex.Message,"Error occured!",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }

        }


        

        private void button1_Click(object sender, EventArgs e)
        {//close the form
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e) //btnBook
        {//if Book Ticket button is clicked
            try
            {//first validate and check if the class and payment methods are selected
                if ((cmbClass.Text == "") || (cmbPmt.Text == ""))
                {
                    MessageBox.Show("Please make sure you choose a Payment Method and Flight Class","Select Payment and Class",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                }
                else
                {//then save all to booking table
                    //assigning the form detalis into class variable to store in database
                    bookClass = new BookingClass();
                    bookClass.FlightID = txtFlight.Text;
                    bookClass.CustomerID = txtCustomer.Text;
                    bookClass.From = lblFrom.Text;
                    bookClass.To = lblTo.Text;
                    bookClass.PmtMetd = cmbPmt.Text;
                    bookClass.NumSeats = Convert.ToInt32(lblSeat.Text);

                    bookClass.TotalFare = Convert.ToDouble(txtFare.Text) * Convert.ToDouble(lblRate.Text);
                    txtTotFare.Text = Convert.ToString(bookClass.TotalFare);
                    bookClass.addBooking();



                    char seatRow;
                    int seatNum;
                    int rowNumber;
                    //update seatRecords in database
                    seatClass seatObj = new seatClass();
                    seatObj.loadSeats(Convert.ToInt32(bookClass.FlightID));
                    for (int i = 0; i < seatObj.seatCount; i++)
                    {
                        //get row and number from seat class and assign here
                        seatRow = seatObj.seatObject[i].SeatRow;
                        seatNum = seatObj.seatObject[i].SeatNumber;

                        rowNumber = (int)seatRow - 64; //take seatrow above and subtract from 64 coz we added before to get ascii here we subtract

                        if (seatStat[seatNum, rowNumber] == 2) //seatStat is just the array variable assigned top
                        {//send to update seat class, which turns seat==2 into seat ==1 i.e.booked
                            seatObj.updateSeat(Convert.ToInt32(bookClass.FlightID), seatRow, seatNum, Convert.ToInt32(bookClass.BookingID));
                        }

                    }

                    MessageBox.Show("Data added successfully! Booking Completed", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex, "Exception Error");
            }


        }

        private void cmbClass_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {//automatically calculate the fare amount and display in the textboxes
                lblRate.Text = bookClass.getFareRate(cmbClass.Text).ToString();
                txtTotFare.Text = Convert.ToString(bookClass.TotalFare);
            }
            catch(NullReferenceException ex)
            {
                MessageBox.Show("Select a value from comboBox"+ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (Exception ex2)
            {
                MessageBox.Show("Select a value from comboBox"+ex2.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
            
        
    }
}
