using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;//

namespace AirlineApplication
{
    class seatClass
    {
        private char seatRow;
        private int seatNumber;
        private int flightID;
        private int available;
        private int bookingID;

        public static int seatLimit=6; //maximum seats per customer

        public int BookingID
        {
            get { return bookingID; }
            set { bookingID = value; }
        }
        public int Available
        {
            get { return available; }
            set { available = value; }
        }
        
        public int FlightID
        {
            get { return flightID; }
            set { flightID = value; }
        }
       
        public int SeatNumber
        {
            get { return seatNumber; }
            set { seatNumber = value; }
        }
        
        public char SeatRow
        {
            get { return seatRow; }
            set { seatRow = value; }
        }

        DatabaseConnector dbcon;


        public int seatCount;
        public seatClass[] seatObject = new seatClass[260];

        public void loadSeats(int f)
        {   //this class is to load the seats for each flight
            seatCount = 0;
            dbcon = new DatabaseConnector();
            //dbcon.dataSet;
            dbcon.OpenConnection();
            //dbcon.InitSqlCommand("SELECT * FROM seat WHERE flightID=" + f + "");
            dbcon.GetData("SELECT * FROM seat WHERE flightID=" + f + "");
            seatCount = dbcon.dataSetMain.Tables[0].Rows.Count;

            for (int i = 0; i < seatCount; i++)
            {//load values from database for each seat
                seatObject[i] = new seatClass();
                dbcon.dataRowMain = dbcon.dataSetMain.Tables[0].Rows[i];
                seatObject[i].SeatRow =(Convert.ToChar(dbcon.dataRowMain[0]));
                seatObject[i].SeatNumber =(int) dbcon.dataRowMain[1];
                seatObject[i].FlightID = (int)dbcon.dataRowMain[2];
                seatObject[i].Available = (int)dbcon.dataRowMain[3];
                seatObject[i].BookingID = (int)dbcon.dataRowMain[4];
                
            }

        }

        public void updateSeat(int flightID,char seatRow,int stNumber,int bookingID)
        {
            //to update seat table and make booked seats as available=1
            dbcon = new DatabaseConnector();
            dbcon.OpenConnection();
            dbcon.InitSqlCommand("UPDATE seat SET available='1',bookingID='"+bookingID+"' WHERE flightID='"+flightID+"' AND seatRow='"+seatRow+"' AND seatNumber='"+stNumber+"'").ExecuteNonQuery();
            dbcon.CloseConnection();
        }



        }

    }

