using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing; //for image data type

namespace AirlineApplication
{
      abstract class AbstractPlane
    {
        private string planeID;
          //make public if plane id doesn't work
        public string PlaneID
        {
            get { return planeID; }
            set { planeID = value; }
        }
        private string modelNumber;
        
        private string engineType;
        private int flySpeed;
        private string specialFeature;
        private double price;
        

        private Image planeImage;
        private int economySeats;
       private int bussinessSeats;
       private int firstclassSeats;
        public string ModelNumber
        {
            get { return modelNumber; }
            set { modelNumber = value; }
        }
        

        
        public string EngineType
        {
            get { return engineType; }
            set { engineType = value; }
        }
       

        public int FlySpeed
        {
            get { return flySpeed; }
            set { flySpeed = value; }
        }

        public string SpecialFeature
        {
            get { return specialFeature; }
            set { specialFeature = value; }
        }

        public double Price
        {
            get { return price; }
            set { price = value; }
        }

        public Image PlaneImage
        {
            get { return planeImage; }
            set { planeImage = value; }
        }

        public int EconSeats
        {
            get { return economySeats; }
            set { economySeats = value; }
        }

        public int BussSeats
        {
            get { return bussinessSeats; }
            set { bussinessSeats = value; }
        }
        public int FirstSeats
        {
            get { return firstclassSeats; }
            set { firstclassSeats = value; }
        }


        //static variables - occur only once for whole class
        public static int countPlane;
        public static AbstractPlane[] planeObj = new AbstractPlane[30]; //30 planes maximum for the system


          //abstract method to add plane to database
        public abstract void addPlane(string mod, string eng, int fly, string spec, double price, string img, int econ, int buss, int first);


    }
}
