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
    public partial class FlightStatus : Form
    {
        BookingClass bookClass;

        Button[,] btnSeat = new Button[45, 20];//this is to buttons as seat
        Label[] lblClass = new Label[8];
        Label[] lblClass0 = new Label[8];

        int[,] seatStatus = new int[44, 20];

        public FlightStatus()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void TicketSales_Load(object sender, EventArgs e)
        {
            bookClass = new BookingClass();
           //show status of all the flights not yet arrived
            this.comboBox1.DisplayMember = "flight_id";//column that you want to show on combo Box
            this.comboBox1.ValueMember = "flight_id";

            this.comboBox1.DataSource = bookClass.getFlightDetails();

            //if there are no flights remaining(all flights have left the airport) then show error message
            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("There are no more remaining flights to be booked. Please add a new flight","No flights",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                this.Dispose();
            }
        }



        private void drawPlan()
        {
            int offset; //this is for the difference
            int maxSeats;
            for (int j = 1; j <= 7; j++)//7 rows
            {
                maxSeats = 35;
                //if (j == 2) maxSeats = 15;
                if (j == 3) maxSeats = 30;
                if (j == 4) maxSeats = 30;
                if (j == 5) maxSeats = 30;
                for (int i = 1; i < maxSeats; i++)
                {
                    offset = 0;
                    if (j == 1) offset = 5;
                    if (j == 2) offset = 3;
                    if (j == 6) offset = 3;
                    if (j == 7) offset = 5;

                    btnSeat[i, j] = new Button();  //creating a new button for each seat
                    btnSeat[i, j].Width = 28;
                    btnSeat[i, j].Height = 28;


                    btnSeat[i, j].Left = (30 * i) + (30 * offset);

                    if (i + offset > 17) btnSeat[i, j].Left += 28;
                    if (i + offset > 5) btnSeat[i, j].Left += 28;


                    btnSeat[i, j].Top = (30 * j);
                    btnSeat[i, j].Image = AirlineApplication.Properties.Resources.seat1;
                    if (seatStatus[i, j] == 1)
                        btnSeat[i, j].Image = AirlineApplication.Properties.Resources.seat2;

                    //============this is code for making slected buttons yellow 1, give each button a name here
                    //NO NEED HERE FOR THIS
                    //string buttonName = "btn";
                    //if (j <= 9) buttonName += " "; //adding a space eg btnA 9
                    //buttonName += j;

                    //if (i <= 9) buttonName += " "; //adding a space eg btnA 9
                    //buttonName += i;
                    //btnSeat[i, j].Name = buttonName;
                    //btnSeat[i, j].Click += new EventHandler(seat_Click);
                    //====================================================
                    panel1.Controls.Add(btnSeat[i, j]);

                    //----------tooltip for each button / seat
                    int rowNumber = j;
                    //if (j > 8)
                    //{
                    //    rowNumber++;
                    //}
                    string tooltipText = Convert.ToChar(rowNumber + 64).ToString() + (i).ToString();
                    ToolTip buttonToolTip = new ToolTip();
                    buttonToolTip.SetToolTip(btnSeat[i, j], tooltipText);


                }


                ///first label abcde...
                lblClass[j] = new Label();
                lblClass[j].Size = new System.Drawing.Size(24, 30);
                char c = Convert.ToChar(64 + j);
                //if (j > 8)
                //{
                //    c = Convert.ToChar(65 + j);
                //}
                lblClass[j].Text = Convert.ToString(c);
                lblClass[j].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblClass[j].Location = new System.Drawing.Point(185, 10 + 30 * j);
                panel1.Controls.Add(lblClass[j]);

                ///second label
                lblClass0[j] = new Label();
                lblClass0[j].Size = new System.Drawing.Size(24, 30);
                char d = Convert.ToChar(64 + j);  //for alphabet
                //if (j > 8)
                //{
                //    d = Convert.ToChar(65 + j);
                //}
                lblClass0[j].Text = Convert.ToString(d);
                lblClass0[j].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                lblClass0[j].Location = new System.Drawing.Point(575, 10 + 30 * j);
                panel1.Controls.Add(lblClass0[j]);

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //load the seat for the selected flight
            int f = Convert.ToInt32( comboBox1.Text);
            bookClass.getFlightDetails(Convert.ToString(f));
            label3.Text = Convert.ToString(bookClass.TotalFare); 
            seatClass stCl = new seatClass();
            stCl.loadSeats(f);

            double totSales = 0;
            for (int i = 0; i < stCl.seatCount; i++)
            {
                char c = stCl.seatObject[i].SeatRow;
                int r = ((int)c) - 64;

                int s = stCl.seatObject[i].SeatNumber;
                int available = stCl.seatObject[i].Available;
                seatStatus[s, r] = available;
                if (available == 1)
                {
                    totSales += Convert.ToInt32(label3.Text); //adding totSales with the fare for each seat

                }

            }
            int totalButtons = panel1.Controls.Count;
            for (int i = 0; i < totalButtons; i++)
            {
                panel1.Controls.RemoveAt(0);
            }
            //now drawPlan 
            drawPlan();
            textBox1.Text=totSales.ToString();
        }
    }
}
