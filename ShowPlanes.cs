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
    public partial class ShowPlanes : Form
    {
        NormalPlane np = new NormalPlane();

        //string planeName;

        public ShowPlanes()
        {// this form is to show airplanes
            InitializeComponent();
            AbstractPlane.countPlane = 0;
            np.loadPlane();

            displayPlane();
        }

        public void displayPlane()
        {//for now we only display 8 planes refer customerclass for increasing more plane display
            PictureBox[] pBox = new PictureBox[8];
            Label[] lbl = new Label[8];
            TextBox[] txtBx = new TextBox[8];
            Button[] btn = new Button[8];

            //this to display each plane
            for (int i = 0; i < AbstractPlane.countPlane; i++)
            {
                //display image of each plane
                pBox[i] = new PictureBox();
                pBox[i].Image = AbstractPlane.planeObj[i].PlaneImage; 
                pBox[i].Size = new System.Drawing.Size(240,240) ;
                pBox[i].Location =new System.Drawing.Point(60,60+300*i) ;
                pBox[i].SizeMode =PictureBoxSizeMode.StretchImage ;
                pnlPlane.Controls.Add(pBox[i]);
                pBox[i].Refresh();

                //title of each plane
                lbl[i] = new Label();
                lbl[i].Size = new System.Drawing.Size(200, 30);
                lbl[i].Text = AbstractPlane.planeObj[i].ModelNumber;
                //planeName = lbl[i].Text;
                lbl[i].Font = new System.Drawing.Font("Microsoft Sans Serif",14F,System.Drawing.FontStyle.Bold,System.Drawing.GraphicsUnit.Point,((byte)(0)));
                lbl[i].Location = new System.Drawing.Point(320, 60 + 300 * i);
                pnlPlane.Controls.Add(lbl[i]);

                //now to display another field
                txtBx[i] = new TextBox();
                txtBx[i].TabStop = false;
                txtBx[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                txtBx[i].Location = new System.Drawing.Point(320, 100 + 300 * i);
                
                txtBx[i].Size = new System.Drawing.Size(250, 20);
                txtBx[i].Text = "Engine Type : "+ AbstractPlane.planeObj[i].EngineType;
                txtBx[i].ReadOnly = true;
                txtBx[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                pnlPlane.Controls.Add(txtBx[i]);

                //now fly speed
                txtBx[i] = new TextBox();
                txtBx[i].TabStop = false;
                txtBx[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                txtBx[i].Location = new System.Drawing.Point(320, 125 + 300 * i);
                
                txtBx[i].Size = new System.Drawing.Size(250, 20);
                txtBx[i].Text = "Maximum Cruise Speed : " + AbstractPlane.planeObj[i].FlySpeed + " km/h";
                txtBx[i].ReadOnly = true;
                txtBx[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                pnlPlane.Controls.Add(txtBx[i]);

                //display special features here
                txtBx[i] = new TextBox();
                txtBx[i].TabStop = false;
                txtBx[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                txtBx[i].Location = new System.Drawing.Point(320, 150 + 300 * i);
                txtBx[i].Multiline = true;
                txtBx[i].Size = new System.Drawing.Size(250, 70);
                txtBx[i].Text = "Special Features :\r\n" + AbstractPlane.planeObj[i].SpecialFeature;
                txtBx[i].ReadOnly = true;
                txtBx[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                pnlPlane.Controls.Add(txtBx[i]);

                //display price
                txtBx[i] = new TextBox();
                txtBx[i].TabStop = false;
                txtBx[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                txtBx[i].Location = new System.Drawing.Point(320, 250 + 300 * i);
                txtBx[i].Size = new System.Drawing.Size(250, 20);
                txtBx[i].Text = "Airplane Cost : " + AbstractPlane.planeObj[i].Price + " $";
                txtBx[i].ReadOnly = true;
                txtBx[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                pnlPlane.Controls.Add(txtBx[i]);

                //seat first
                txtBx[i] = new TextBox();
                txtBx[i].TabStop = false;
                txtBx[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                txtBx[i].Location = new System.Drawing.Point(320+420 , 100 + 300 * i);
                txtBx[i].Size = new System.Drawing.Size(500, 20);
                txtBx[i].Text = "First Class : " + AbstractPlane.planeObj[i].FirstSeats;
                txtBx[i].ReadOnly = true;
                txtBx[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                pnlPlane.Controls.Add(txtBx[i]);

                //seat bussiness
                txtBx[i] = new TextBox();
                txtBx[i].TabStop = false;
                txtBx[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                txtBx[i].Location = new System.Drawing.Point(320 + 420, 125 + 300 * i);
                txtBx[i].Size = new System.Drawing.Size(500, 20);
                txtBx[i].Text = "Business Class : " + AbstractPlane.planeObj[i].BussSeats;
                txtBx[i].ReadOnly = true;
                txtBx[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                pnlPlane.Controls.Add(txtBx[i]);

                //econ Seats
                txtBx[i] = new TextBox();
                txtBx[i].TabStop = false;
                txtBx[i].BorderStyle = System.Windows.Forms.BorderStyle.None;
                txtBx[i].Location = new System.Drawing.Point(320 + 420, 150 + 300 * i);
              
                txtBx[i].Size = new System.Drawing.Size(500, 20);
                txtBx[i].Text = "First Class : " + AbstractPlane.planeObj[i].EconSeats;
                txtBx[i].ReadOnly = true;
                txtBx[i].Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                pnlPlane.Controls.Add(txtBx[i]);



                //NOW button
                btn[i] = new Button();
                btn[i].Location = new System.Drawing.Point(320+420,220+300* i);
                btn[i].Size = new System.Drawing.Size(112, 50);
                btn[i].Text = "Choose Plane";
                //assigning unique name to each button
                string nameBtn = "btn" + i;
                btn[i].Name = nameBtn;
                btn[i].Click += new EventHandler(takePlane);
                pnlPlane.Controls.Add(btn[i]);
            }
        }


        private void takePlane(object sender,EventArgs e)
        {
            //take the plane as selected one to the AddFlight Form
            //button is not on form as it is made from the above code

            //Problem with code only last added flight is coming
           
            Button clickButton = (Button) sender;
            string s = clickButton.Name; //above method we assign names to buttons so here we get it and can pass it
            int i = Convert.ToInt16(s.Substring(3,1));
            AddFlight adfl = new AddFlight();
            this.Close();
            adfl.showPlane(i);
            adfl.Show();

        }
    }
}
