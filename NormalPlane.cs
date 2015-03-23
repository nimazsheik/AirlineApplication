using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Data;

using MySql.Data.MySqlClient;

using System.Drawing; //to use image.fromfile


namespace AirlineApplication
{
    class NormalPlane : AbstractPlane
    {
        DatabaseConnector dbcon;
        public override void addPlane(string mod,string eng,int fly,string spec,double price,string img,int econ, int buss,int first)
        {//convert the picture into array of binary data then save
            FileStream fs;
            fs = new FileStream(img, FileMode.Open, FileAccess.Read);
            byte[] picbyte = new byte[fs.Length];

            fs.Read(picbyte, 0, System.Convert.ToInt32(fs.Length));
            fs.Close();


            //now connect to database
             dbcon = new DatabaseConnector ();
            dbcon.OpenConnection();
            //adding all the details
            string query = "INSERT INTO plane(modelNumber,engineType,flySpeed,specialFeature,price,planeImage,econSeats,bussSeats,firstSeats) VALUES('"+mod+"','"+eng+"','"+fly+"','"+spec+"','"+price+"',"+"@pic,'"+econ+"','"+buss+"','"+first+"')";
            
            MySqlParameter picparameter = new MySqlParameter();
            picparameter.MySqlDbType = MySqlDbType.Blob;
            picparameter.ParameterName = "pic";
            picparameter.Value = picbyte;
            dbcon.InitSqlCommand(query);
            dbcon.mySqlCommand.Parameters.Add(picparameter);
            dbcon.mySqlCommand.ExecuteNonQuery();
            dbcon.CloseConnection();



         }

        public void loadPlane()
        {

            //convert picture to jpg and load other details
            MySqlDataAdapter sqladapt;
            DataSet dataSet;
            DatabaseConnector dbcon = new DatabaseConnector();
            dbcon.OpenConnection();
            sqladapt = new MySqlDataAdapter();
            sqladapt.SelectCommand = dbcon.InitSqlCommand("SELECT * FROM plane");
            dataSet = new DataSet("dst");
            sqladapt.Fill(dataSet);
            dbcon.CloseConnection();

            DataTable tableData = dataSet.Tables[0];
            //use from iplane class
            AbstractPlane.countPlane = tableData.Rows.Count;
            for (int i = 0; i < AbstractPlane.countPlane; i++)
            {
                AbstractPlane.planeObj[i] = new NormalPlane();
                DataRow drow = tableData.Rows[i];
                string plName = "pic" + Convert.ToString(i);

                FileStream FSNew = new FileStream(plName+"jpg",FileMode.Create);
                byte[] blob = (byte[])drow[6];
                FSNew.Write(blob,0,blob.Length);
                FSNew.Flush();
                FSNew.Close();
                FSNew = null;

                //before mistake replace image from file to memory stream
                using (var stream = new MemoryStream(blob))      //if not work replace blob with this (byte[])drow[5]
                {
                  //  var image = Image.FromStream(stream);
                  //  AbstractPlane.planeObj[i].PlaneImage = image;
                    System.Drawing.Bitmap bmp = new System.Drawing.Bitmap(stream);
                    AbstractPlane.planeObj[i].PlaneImage = bmp;
                }




                //IPlane.planeObj[i].PlaneImage=(Image.FromFile(plName + "jpg"));
                AbstractPlane.planeObj[i].PlaneID = drow[0].ToString();
                AbstractPlane.planeObj[i].ModelNumber = drow[1].ToString();
                AbstractPlane.planeObj[i].EngineType = drow[2].ToString();
                AbstractPlane.planeObj[i].FlySpeed = Convert.ToInt32( drow[3]);
                AbstractPlane.planeObj[i].SpecialFeature = drow[4].ToString();
                AbstractPlane.planeObj[i].Price = Convert.ToDouble( drow[5]);
                AbstractPlane.planeObj[i].EconSeats = Convert.ToInt32(drow[7]);
                AbstractPlane.planeObj[i].BussSeats = Convert.ToInt32(drow[8]);
                AbstractPlane.planeObj[i].FirstSeats = Convert.ToInt32(drow[9]);
                //drow 5 is the image

            }
            
            
        }


    }
}
