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
    public partial class BookSeatPlan : Form
    {
        Button[,] btnSeat = new Button[45,20];//this is to set buttons as seat
        Label[] lblClass1 = new Label[8];//to separate firstclass
        Label[] lblClass2 = new Label[8];//to separate businessclass


        int[,] seatStatus = new int[45,20]; //to check the status of seats

        BookingClass bookClass = new BookingClass();
        seatClass stClass = new seatClass();
        FlightClass fClass = new FlightClass();

        int customerID;//to pass to bookingform
        public BookSeatPlan()
        {
            InitializeComponent();

            label5.Text = "0";
        }

        private void BookSeatPlan_Load(object sender, EventArgs e)
        {
            showFlightDetails();
        }



        public void showFlightDetails()
        {
            //code for label and comboBox
            //call in form load this method
            bookClass = new BookingClass();
            
            this.cbFlightID.DisplayMember = "flight_id";//column that you want to show on combo Box
            this.cbFlightID.ValueMember = "flight_id";
            
            this.cbFlightID.DataSource = bookClass.getFlightDetails();
        }

        private void drawPlan()
        {//copy and paste same thing to ticket check form 
            try
            {
                int offset; //this is for the difference
                int maxSeats;
                for (int j = 1; j <= 7; j++)//j  is rows , here 7 rows
                {
                    maxSeats = 35;
                    //if (j == 2) maxSeats = 15;
                    if (j == 3) maxSeats = 30;  //3rd row
                    if (j == 4) maxSeats = 30;  //4th row
                    if (j == 5) maxSeats = 30;  //5th row

                    for (int i = 1; i < maxSeats; i++) // i is for columns
                    {
                        offset = 0;
                        if (j == 1) offset = 5; //from left, offset is to remove the seats
                        if (j == 2) offset = 3;
                        if (j == 6) offset = 3;
                        if (j == 7) offset = 5;

                        btnSeat[i, j] = new Button();  //creating a new button for each seat
                        btnSeat[i, j].Width = 28; //set width and height of each button
                        btnSeat[i, j].Height = 28;

                        //from left the distance from each console, offset is variable so it increases/decrease for each row
                        btnSeat[i, j].Left = (30 * i) + (30 * offset);

                        //if i and offset is greater than 17 or 5 then move by 28 pixels to put character map eg A,B,C...
                        if (i + offset > 17) btnSeat[i, j].Left += 28;
                        if (i + offset > 5) btnSeat[i, j].Left += 28;


                        btnSeat[i, j].Top = (30 * j); //to adjust one row below the other

                        btnSeat[i, j].Image = AirlineApplication.Properties.Resources.seat1;

                        if (seatStatus[i, j] == 1)
                            btnSeat[i, j].Image = AirlineApplication.Properties.Resources.seat2;

                        //============this is code for making slected buttons yellow 1, give each button a name here
                        string buttonName = "btn";
                        if (j <= 9) buttonName += " "; //adding a space eg btn 1 for loop in i
                        buttonName += j;


                        if (i <= 9) buttonName += " "; //adding a space eg btn 1 1, 1 2 , 1 3 for loop in i
                        buttonName += i;
                        btnSeat[i, j].Name = buttonName;
                        btnSeat[i, j].Click += new EventHandler(seat_Click);// see seat click
                        //====================================================
                        pnlPlane.Controls.Add(btnSeat[i, j]);

                        //----------tooltip for each button / seat
                        int rowNumber = j;

                        //using ascii to get tooltip text eg j=1,  64+j = 65=A ; A1
                        string tooltipText = Convert.ToChar(rowNumber + 64).ToString() + (i).ToString();
                        ToolTip buttonToolTip = new ToolTip();
                        buttonToolTip.SetToolTip(btnSeat[i, j], tooltipText);



                    }//end of i loop


                    //first label A B C D E .....
                    //setting properties for the label
                    lblClass1[j] = new Label();
                    lblClass1[j].Size = new System.Drawing.Size(24, 30);
                    char c = Convert.ToChar(64 + j);  //same ascii principle as above 65 ---> A

                    lblClass1[j].Text = Convert.ToString(c);
                    lblClass1[j].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    //for each j it prints the variable here we set the location x variable is constant while y value changes according to j
                    lblClass1[j].Location = new System.Drawing.Point(185, 10 + 30 * j); 
                    pnlPlane.Controls.Add(lblClass1[j]);

                    ///second label A B C D E..........
                    lblClass2[j] = new Label();
                    lblClass2[j].Size = new System.Drawing.Size(24, 30);
                    char d = Convert.ToChar(64 + j);  //for alphabet

                    lblClass2[j].Text = Convert.ToString(d);
                    lblClass2[j].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    lblClass2[j].Location = new System.Drawing.Point(575, 10 + 30 * j);
                    pnlPlane.Controls.Add(lblClass2[j]);

                }//end j loop
            }
                
            catch(Exception ex)
            {
                MessageBox.Show("Error! Please restart the application and make sure all images are present \r\nException Error : "+ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }


        private void seat_Click(object sender, EventArgs e)
        {//create button from above method 
            try
            {
                
                Button clickedButton = (Button)sender;

                //breaking down the button to obtain row and seat number
                string s = clickedButton.Name;
                int j = Convert.ToInt16(s.Substring(3, 2)); //example btn 1 5 - after use substring, 1 will return
                int i = Convert.ToInt16(s.Substring(5, 2));//example btn 1 5 - after use substring, 5 will return

                if (seatStatus[i, j] != 1) //if 1 means its booked so we check if not booked
                {
                    if (seatStatus[i, j] == 0) //if not booked and is empty make it yellow
                    {
                        
                        seatStatus[i, j] = 2;   //assign 2 as in available
                        btnSeat[i, j].Image = AirlineApplication.Properties.Resources.seat3;
                    }
                    else //if its 2 and clicked make it back to red
                    {
                        seatStatus[i, j] = 0; //if seat is already selected and you click again then make it red
                        btnSeat[i, j].Image = AirlineApplication.Properties.Resources.seat1;
                    }
                    listSelectedSeats(); //whenever clicked it calls this method to add seats to listbox, and even when remove seats it can remove from listbox

                }


            }
            catch (ArgumentOutOfRangeException ex)
            {//if input string ever becomes less than 5 characters catch this exception
                MessageBox.Show("Error! \r\nException Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex1)
            {
               MessageBox.Show("Error!  \r\nException Error : " + ex1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        
 
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                //take flight id from comboBox selection
                int f = int.Parse(cbFlightID.Text);

                // Load the seats for the selected flights
                stClass.loadSeats(f);
                for (int i = 0; i < stClass.seatCount; i++)
                {//gettinig seatrow, seatnum and available
                    char c = stClass.seatObject[i].SeatRow;
                    int r = (int)c - 64;        //subtract 64 and change back to integer,eg A = 65-64= 1
                    int s = stClass.seatObject[i].SeatNumber;
                    int available = stClass.seatObject[i].Available;
                    seatStatus[s, r] = available; //integer available assigned to array we declare top 
                }

                int totalButtons = pnlPlane.Controls.Count; //now to remove the extra buttons that we have offset or removed from above
                for (int i = 0; i < totalButtons; i++)
                {//existing seatbuttons removed from panel when selectedindex change
                    pnlPlane.Controls.RemoveAt(0);
                }


                //DISPLAY PRICE FROM BOOK CLASS
                bookClass.getFlightDetails(Convert.ToString(f));
                label4.Text = Convert.ToString(bookClass.TotalFare);

                drawPlan();//call the draw plan method for each flight changed
            }
            catch (NullReferenceException ex0)
            {
               
               MessageBox.Show("Error! Please select a valid flight from dropdown box  \r\nException Error : " + ex0.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex1)
            {
                MessageBox.Show("Error!  \r\nException Error : " + ex1.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        
        private void listSelectedSeats()
        {// this method to take selected seats to listboxd
            double totalFare = 0;
            try
            {
                
                listBox1.Items.Clear();
                for (int j = 1; j <= 7; j++)
                {
                    for (int i = 1; i <= 35; i++)
                    {
                        if (seatStatus[i, j] == 2)//if seat is selected
                        {
                            char c = Convert.ToChar(64 + j);
                            listBox1.Items.Add("Row " + c + " Seat " + i);
                            totalFare += Convert.ToInt32(label4.Text);//adding the fare for each seat

                        }
                    }
                }
                label5.Text = totalFare.ToString("f2");
                
            }
           
            catch (Exception ex1)
            {
                MessageBox.Show("Error seat not selected" + ex1.Message, "Error ", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        

        private void button1_Click(object sender, EventArgs e)//when btnClear is pressed
        {
            try
            {
                //first clear listbox
                listBox1.Items.Clear();
                //then clear selected items and make it yellow
                for (int j = 1; j <= 7; j++)
                {
                    for (int i = 1; i <= 35; i++)
                    {
                        if (seatStatus[i, j] == 2) //when cleared whatever is  yellow make it back to seat 1 image
                        {
                            seatStatus[i, j] = 0;
                            btnSeat[i, j].Image = AirlineApplication.Properties.Resources.seat1;

                        }
                    }
                }
                // totalFare = 0;
                label5.Text = "0";
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            try
            {//Validation
                if (listBox1.Items.Count >0)//Customer must at least make one booking
                {//seatLimit is static variable from seatClass
                    if (listBox1.Items.Count <= seatClass.seatLimit) //Booking limit per customer is 6
                    {
                        //pass the details from this bookseatplan form to booking form
                        BookingForm bookForm = new BookingForm();
                        int f = int.Parse(cbFlightID.Text);
                        double totFare = Convert.ToDouble(label5.Text);
                        bookForm.getBooking(f, totFare, seatStatus, customerID);
                        //MessageBox.Show( Convert.ToString( totFare));

                        bookForm.ShowDialog();
                        this.Close();//close the form
                    }
                    else
                    {
                        MessageBox.Show("Customer can only book a maximum of 6 seats.\r\nPlease make sure you select only 6 seats and try again", "Max booking limit exceeded", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        button1_Click(sender, e); //call the button1click event and clear all the listbox and panel selections
                    }
                }
                else
                {
                    MessageBox.Show("Customer should book at least one seat!", "Book at least one seat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //this code is to pass the customer details from the customer form to be displayed on this bookSeatPlan form
        public void getCustomerNic(int cusID,string nic,string fname,string lname)
        {
            customerID = cusID;
            label2.Text = "Booking the seats for Customer" +fname+ " "+lname+ "\r\n ID = "+cusID+ "and NIC = "+nic;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            SearchFlight sf = new SearchFlight();
            sf.Show();
        }
    }
}
