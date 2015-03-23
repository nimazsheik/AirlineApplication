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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DatabaseConnector dbcon = new DatabaseConnector();
            

            if (txtName.Text == "" || txtPass.Text == "")
            {
                MessageBox.Show("Please make sure username and password are entered","Login Failed",MessageBoxButtons.OK,MessageBoxIcon.Stop);
            }
            else
            {
                dbcon.OpenConnection();
               // dbcon.InitSqlCommand("SELECT * FROM login WHERE username='"+txtName.Text+"' AND password='"+txtPass.Text+"'");
               int i = dbcon.GetData("SELECT * FROM login WHERE username='" + txtName.Text + "' AND password='" + txtPass.Text + "'").Rows.Count;
              
                if(i==1)
                {
                    
                    MessageBox.Show("Login Details are correct","Success",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    Menu main = new Menu();
                    
                    main.Show();
                  
                   this.Owner = main; //so when main form is closed this is also closed
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Invalid username and password","Login Failed",MessageBoxButtons.OK,MessageBoxIcon.Stop);
                }
            }
        }



    }
}
