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
    

    public partial class AddPlane : Form
    {

        NormalPlane normPlane = new NormalPlane();
        string imagename; //name of the image
        public AddPlane()
        {
            InitializeComponent();
        }

        private void btnLoad_Click(object sender, EventArgs e)
        { //btn ot load image
            try
            {
                FileDialog fdialog = new OpenFileDialog();
                fdialog.Filter = "Image File (*.jpg; *.bmp; *.gif) | *.jpg; *.bmp; *.gif";

                if (fdialog.ShowDialog() == DialogResult.OK)
                {
                    imagename = fdialog.FileName;
                    Bitmap newimg = new Bitmap(imagename);
                    pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
                    pictureBox1.Image = (Image)newimg;
                    
                }
                fdialog = null;

            }
            catch(Exception ex)
            {
                MessageBox.Show("Image cannot be loaded \nError : "+ex.Message,"Wrong image format",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
         try
            {
        
            
                if (imagename != null) //check if picture is added first
                {
                    var controls = new[] { txtModNum.Text, txtEngType.Text, txtFeature.Text }; //checking if text fields are not empty
                    if (!controls.All(x => string.IsNullOrEmpty(x)))  //if its not null or empty do not enter the details to database
                    {
                        normPlane.addPlane(txtModNum.Text, txtEngType.Text, Convert.ToInt32(numFlySpeed.Text), txtFeature.Text, Convert.ToDouble(numPrice.Text), imagename, Convert.ToInt32(numEconSeats.Text), Convert.ToInt32(numBusSeats.Text), Convert.ToInt32(numFirstSeats.Text));

                        MessageBox.Show("Plane details added successfully","Plane added",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        MessageBox.Show("Please fill all the plane details before saving ", "Empty fields", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    MessageBox.Show("Please attach a picture less than 1 MB before saving ", "Insert image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
           
           }
           catch (Exception ex)
            {
                MessageBox.Show("Make sure all the details entered are valid, and image should not be bigger than 1 MB \nError : " + ex.Message, "Image too big", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
