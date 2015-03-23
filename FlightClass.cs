using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;//for datatable


namespace AirlineApplication
{
     class FlightClass
    {
        private string flightID;
        private string planeName;    //from plane model table
        private string sourceLocation;
        private string via;
        private string destLocation;
        private DateTime departDate;
        private string departTime;
        private DateTime arrivalDate;
        private string arrivalTime;
        private int fare;

        public string FlightID
        {
            get { return flightID; }
            set { flightID = value; }
        }
       public string PlaneName
        {
            get { return planeName; }
            set { planeName = value; }
        }
       public string SourceLocation
       {
           get { return sourceLocation; }
           set { sourceLocation = value; }
       }
         public string Via
        {
            get { return via; }
            set { via = value; }
        }
         public string DestLocation
         {
             get { return destLocation; }
             set { destLocation = value; }
         }
         public DateTime DepartDate
         {
             get { return departDate; }
             set { departDate = value; }
         }
         public string DepartTime
         {
             get { return departTime; }
             set { departTime = value; }
         }
         public DateTime ArrivalDate
         {
             get { return arrivalDate; }
             set { arrivalDate = value; }
         }
         public string ArrivalTime
         {
             get { return arrivalTime; }
             set { arrivalTime = value; }
         }
         public int Fare
         {
             get { return fare; }
             set { fare = value; }
         }

        public DatabaseConnector dbcon;

 

        public void addFlight()   
        {
           dbcon = new DatabaseConnector();
            
            dbcon.OpenConnection();

            //insert into tbl_nm(db FIELDS) VALUES(variables)
            dbcon.InitSqlCommand("INSERT INTO flight(flight_id,plane_name,source_location,via,dest_location,dept_date,dept_time,arrival_date,arrival_time,fare) VALUES(@fid,@pn,@src,@via,@dst,@dpd,@dpt,@ard,@art,@fr)");
            dbcon.mySqlCommand.Parameters.AddWithValue("@fid", FlightID);
            dbcon.mySqlCommand.Parameters.AddWithValue("@pn", PlaneName);
            dbcon.mySqlCommand.Parameters.AddWithValue("@src", SourceLocation);
            dbcon.mySqlCommand.Parameters.AddWithValue("@via", Via);
            dbcon.mySqlCommand.Parameters.AddWithValue("@dst", DestLocation);
            dbcon.mySqlCommand.Parameters.AddWithValue("@dpd", DepartDate);
            dbcon.mySqlCommand.Parameters.AddWithValue("@dpt", DepartTime);
            dbcon.mySqlCommand.Parameters.AddWithValue("@ard", ArrivalDate);
            dbcon.mySqlCommand.Parameters.AddWithValue("@art", ArrivalTime);
            dbcon.mySqlCommand.Parameters.AddWithValue("@fr", Fare);   
            

            dbcon.mySqlCommand.ExecuteNonQuery();

            //================================================
            //this is to pass last flightID to assign seats
           // dbcon.InitSqlCommand("SELECT LAST_INSERT_ID() FROM flight");
            dbcon.InitSqlCommand("SELECT MAX(flight_id) FROM flight");
            int idFlight = Convert.ToInt32(dbcon.mySqlCommand.ExecuteScalar()); //execute statement with only one result //execute nonquery no result
            dbcon.CloseConnection();
            assignSeats(idFlight);





            //================================================
            //dbcon.CloseConnection();

        }

       

         public DataTable flightSchedule()
         {
             dbcon = new DatabaseConnector();
             dbcon.OpenConnection();
             DateTime today = DateTime.Today;
             return dbcon.GetData("SELECT * FROM flight WHERE dept_date = \"" + today.ToString("yyyy/MM/dd") + "\" ORDER BY dept_time"); //this returns a datatable assign it to custdatatable variable
             //dataGridView1.DataSource = custDataTable;
            // dbcon.CloseConnection();
             
         }

      
         public void assignSeats(int fltID)   //assign seats for each flight
         {
             dbcon = new DatabaseConnector();
             dbcon.OpenConnection();
             //dbcon.InitSqlCommand();
             //for each seat have to call initsqlcomm method
             for(int r=1;r<=7;r++)//rows
             {
                 char rowLetter = Convert.ToChar(64 + r);
                 for(int s=1;s<=35;s++)//seats
                 {
                     dbcon.InitSqlCommand("INSERT INTO seat(seatRow,seatNumber,flightID,available,bookingID) VALUES('"+rowLetter+"','"+s+"','"+fltID+"',0,0)");
                     dbcon.mySqlCommand.ExecuteNonQuery();
                 }
             }
             dbcon.CloseConnection();

         }

    }
}
