using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data; //for datatable


namespace AirlineApplication
{
    class BookingClass
    {
        private int bookingID;
        private string customerID;
        private string flightID;
        private string from;
        private string to;
       
        private int numberOfSeats;
        private string paymentMethod;
        
        private double totalFare;

        //can keep separate form to set these rates, but for scope of this project just hard code it
        private double economyRate = 1.5;
        private double bussinessRate = 2.0;
        private double firstclassRate = 2.5;



        public int BookingID
        {
            get { return bookingID; }
            set { bookingID = value; }
        }
        public string CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }
        public string FlightID
        {
            get { return flightID; }
            set { flightID = value; }
        }
        public string From
        {
            get { return from; }
            set { from = value; }
        }
        public string To
        {
            get { return to; }
            set { to = value; }
        }
        public int NumSeats
        {
            get { return numberOfSeats; }
            set { numberOfSeats = value; }
        }
        public string PmtMetd
        {
            get { return paymentMethod; }
            set { paymentMethod = value; }
        }
        public double TotalFare
        {
            get { return totalFare; }
            set { totalFare = value; }
        }


        DatabaseConnector dbconn;




        public DataTable getFlightDetails()
        {
            dbconn = new DatabaseConnector();

            dbconn.OpenConnection();
                                                //select flight_id FROM flight;
            var bookDataTable = dbconn.GetData("SELECT flight_ID from flight WHERE dept_date > CURDATE()"); 
            //WHERE departure date > today date , if departure date is before today that flight cannot be booked anymore

            dbconn.CloseConnection();

            return bookDataTable;

           
        }

        public void getFlightDetails(string flightID)   //overloaded method with string value passed
        {
            dbconn = new DatabaseConnector();

            dbconn.OpenConnection();
            
            dbconn.dataReader = dbconn.InitSqlCommand("SELECT * FROM flight WHERE flight_id='" + flightID + "'").ExecuteReader();

            if (dbconn.dataReader.Read())
            {
                From = dbconn.dataReader["source_location"].ToString();//column name should be that you want to show on textbox
                To = dbconn.dataReader["dest_location"].ToString();
                TotalFare = Convert.ToInt32(dbconn.dataReader["fare"]);

            }
            dbconn.CloseConnection();
        }

        public void addBooking()
        {
            
            dbconn = new DatabaseConnector();


            dbconn.OpenConnection();

            //insert into tbl_nm(db FIELDS) VALUES(variables)
            dbconn.InitSqlCommand("INSERT INTO booking(customerID,flightID,location_from,location_to,numSeats,pmtMethod,totalFare) VALUES(@cid,@fid,@frm,@to,@num,@pmt,@tot)");
            dbconn.mySqlCommand.Parameters.AddWithValue("@cid", CustomerID);       //parameter are used instead of directly adding coz its prevent sql injection
            dbconn.mySqlCommand.Parameters.AddWithValue("@fid", FlightID);
            dbconn.mySqlCommand.Parameters.AddWithValue("@frm", From);
            dbconn.mySqlCommand.Parameters.AddWithValue("@to", To);
            dbconn.mySqlCommand.Parameters.AddWithValue("@num", NumSeats);
            dbconn.mySqlCommand.Parameters.AddWithValue("@pmt", PmtMetd);
            dbconn.mySqlCommand.Parameters.AddWithValue("@tot", TotalFare);
            


            dbconn.mySqlCommand.ExecuteNonQuery();

          
            dbconn.CloseConnection();

        }

        public int getTicketNumber()
        {//add one to get the new booking ID as ticket number
            dbconn = new DatabaseConnector();
            dbconn.OpenConnection();
            dbconn.InitSqlCommand("SELECT MAX(bookingID) FROM booking");
            int newBookingID = Convert.ToInt32(dbconn.mySqlCommand.ExecuteScalar()); //execute statement with only one result //execute nonquery no result
            newBookingID += 1; //add 1 coz previous maximum bookingId+1 = currentBookingID(before its created)
            dbconn.CloseConnection();
            return newBookingID;
        }


        public void cancelBooking(int bookingID)
        {//if booking is cancelled set cancelled field to 1 in booking table
            dbconn = new DatabaseConnector();
            dbconn.OpenConnection();
            dbconn.InitSqlCommand("UPDATE booking SET cancelled="+1+" WHERE bookingID="+bookingID).ExecuteNonQuery();
           

            //and set booking id back to 0 in seat table
            //NOTE: bookingID will not turn to 0 but doesnt matter since it gets overwritten when ticket is booked anyway
            dbconn.InitSqlCommand("UPDATE seat SET available=" + 0 + " AND bookingID="+0+" WHERE bookingID=" + bookingID).ExecuteNonQuery(); 
            dbconn.CloseConnection();
        }

        public double getFareRate(string flightClass)
        {
            
            if(flightClass =="Economy")
            {
                  return economyRate;
            }
            else if (flightClass == "Business")
            {
                return bussinessRate;
            }
            else
            {
                return firstclassRate;
            }

            
        }
    }
}