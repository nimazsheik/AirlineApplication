using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Text.RegularExpressions; //gonna use regex to extract number only from the comboBox

namespace AirlineApplication
{
    public partial class Customer :Form 
    {
        CustomerClass customerObject;
        

        bool newCustomer = true; //check if customer entered is new or old, default is true

        int customerID;

        public Customer()
        {
                InitializeComponent();
                //load the customers to the combobox
                loadCustomersToCombo();
               // dateTimePicker1.CustomFormat = "MMMM-dd-yyyy";
               dateTimePicker1.CustomFormat = "yyyy-MM-dd";
               dateTimePicker1.Value = DateTime.Today;
        }


        CustomerService.CustomerServiceClient client = new AirlineApplication.CustomerService.CustomerServiceClient();
        CustomerService.CustomerClass customer = null;


        //ComboBox dropdown style = dropdown list
        private void loadCustomersToCombo()
        {//loading from customer class to combobox
            try
            {
                

               // customerObject = new CustomerClass();
              //  customerObject.loadCustomers();
               // customer = new CustomerService.CustomerClass();
              customer=  client.loadCustomers();
                
                
                comboBox1.Items.Clear();
                for (int i = 0; i < customer.customerCount; i++)
                {
                    customerID = customer.custObject[i].Cid;
                    string customerFullName = customer.custObject[i].Fname + " " + customer.custObject[i].Lname; //display name
                    string cmbDisplay = customerID + " - " + customerFullName;
                    //comboBox1.Items.Add(customerFullName);
                    comboBox1.Items.Add(cmbDisplay);
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception error :" + ex.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }

        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                newCustomer = false; //assuming no new customers from combobox

                //Use the same loaded object in loadCustomerstoCombo method
               // CustomerService.CustomerClass customer = null;
              //  here just using the above variables customer and client variables, not creating new ones

                int i = comboBox1.SelectedIndex;
                txtFname.Text = customer.custObject[i].Fname;
                txtLname.Text = customer.custObject[i].Lname;
                txtNic.Text = customer.custObject[i].Nic;
                if (customer.custObject[i].Gender == 'M')
                {
                    rbMale.PerformClick(); //click it(choose Male if its gender from database or else female
                }
                else
                {
                    rbFemale.PerformClick();
                }
                dateTimePicker1.Value = customer.custObject[i].Dob;
                txtAdd1.Text = customer.custObject[i].Add1;
                txtAdd2.Text = customer.custObject[i].Add2;
                txtPhn.Text = customer.custObject[i].Phone;
                txtEmail.Text = customer.custObject[i].Email;
            }
            catch (NullReferenceException ex)
            {
                MessageBox.Show("Make sure to choose a value from the combo box\r\nException Error : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception exc)
            {
                MessageBox.Show("Exception Error : " + exc.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

      


        private void button2_Click(object sender, EventArgs e)
        {
            //clear entire form and set newCustomer=true
            try
            {// clear textbox
                foreach (Control ctrl in this.Controls)

                    if ((ctrl is TextBox))

                        (ctrl as TextBox).Clear();

                foreach (Control ctrl0 in this.Controls)

                    if ((ctrl0 is MaskedTextBox))

                        (ctrl0 as MaskedTextBox).Clear();
                    
                    
                //clear groupbox
                foreach (Control ctrl1 in this.groupBox1.Controls)

                    if (ctrl1 is TextBox)

                        (ctrl1 as TextBox).Clear();

                //for masked textbox in groupbox
                foreach (Control ctrl2 in this.groupBox1.Controls)

                    if ((ctrl2 is MaskedTextBox))

                        (ctrl2 as MaskedTextBox).Clear();


                newCustomer = true;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Error has occured " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }

        //exit button
        private void button3_Click(object sender, EventArgs e)
        { //Close the form
            this.Dispose();
        }

        //edit button
        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                var controls = new[] { txtFname.Text, txtLname.Text, txtNic.Text, txtAdd1.Text, txtAdd2.Text, txtEmail.Text, txtPhn.Text };
                if (!controls.All(x => string.IsNullOrEmpty(x)))  //if its not null or empty do not enter the details to database
                {//Validation^^^^^^^
                    if (dateTimePicker1.Value < DateTime.Today)//validation for date must be in past
                    {
                        if ((Regex.IsMatch(txtFname.Text, @"^[a-zA-Z]+$"))|| (Regex.IsMatch(txtLname.Text, @"^[a-zA-Z]+$")))
                        {
                
                        customerObject = new CustomerClass();
                        //customerID = comboBox1.SelectedIndex;
                        string cmbText = comboBox1.Text;
                        //im gonna use regex to extract only number(cid) from cmbtext

                        customerID = Convert.ToInt32(Regex.Match(cmbText, @"\d+").Value);

                        customerObject.Fname = txtFname.Text;

                        customerObject.Lname = txtLname.Text;
                        customerObject.Nic = txtNic.Text;
                        //gender
                                if (rbMale.Checked)
                                {
                                    customerObject.Gender = Convert.ToChar(rbMale.Tag); //male
                                }
                                else
                                {
                                    customerObject.Gender = Convert.ToChar(rbFemale.Tag); //female
                                }
                        customerObject.Dob = Convert.ToDateTime(dateTimePicker1.Value.ToString("yyyy-MM-dd"));
                        customerObject.Add1 = txtAdd1.Text;
                        customerObject.Add2 = txtAdd2.Text;
                        customerObject.Phone = txtPhn.Text;
                        customerObject.Email = txtEmail.Text;

                        //MessageBox.Show(updCust.Dob.ToString());
                        customerObject.updateDetails(customerID);
                        MessageBox.Show("Details have been successfully modified","Success",MessageBoxButtons.OK,MessageBoxIcon.Information);
                       }
                       else
                        {
                            MessageBox.Show("date should be in past", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                 }
                else
                {
                    MessageBox.Show("Make sure first name and last name contain only letters", "Format Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Please make sure that all fields are filled","Empty Fields",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Exception Error occured " + ex.Message, "Exception", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
        }


        private void button1_Click_1(object sender, EventArgs e)    //Book Seat
        {
            CustomerService.CustomerServiceClient client = new AirlineApplication.CustomerService.CustomerServiceClient();
            CustomerService.CustomerClass customer = null;

            try
            {
               
                if (newCustomer == true)
                {
                    //validation
                var controls = new[] { txtFname.Text, txtLname.Text, txtNic.Text, txtAdd1.Text, txtAdd2.Text, txtEmail.Text, txtPhn.Text };
                if (!controls.All(x => string.IsNullOrEmpty(x)))  //if its not null or empty enter the details to database
                {//Validation^^^^^^^
                    if (dateTimePicker1.Value < DateTime.Today)//validation for date must be in past
                    {
                        if ((Regex.IsMatch(txtFname.Text, @"^[a-zA-Z]+$"))&& (Regex.IsMatch(txtLname.Text, @"^[a-zA-Z]+$")))
                        {
                            // addDetails(); assign all the varialbes from customer class to these controls
                          //  customerObject = new CustomerClass();   //reuse as new object
                            customer = new CustomerService.CustomerClass();
                            customer.Fname = txtFname.Text;
                            customer.Lname = txtLname.Text;
                            customer.Nic = txtNic.Text;
                            //gender
                                if (rbMale.Checked)
                                {
                                    customer.Gender = Convert.ToChar(rbMale.Tag); //male
                                }
                                else
                                {
                                    customer.Gender = Convert.ToChar(rbFemale.Tag); //female
                                }
                                customer.Dob = dateTimePicker1.Value;
                                customer.Add1 = txtAdd1.Text;
                                customer.Add2 = txtAdd2.Text;
                                customer.Phone = txtPhn.Text;
                                customer.Email = txtEmail.Text;

                             //   customerID = customer.addDetails();
                            this.Refresh();

                           BookSeatPlan bookSeat = new BookSeatPlan();
         //// check this             bookSeat.getCustomerNic(customerID, txtNic.Text, txtFname.Text, txtLname.Text);
                            client.addDetails(customer);
                            newCustomer = false;
                            MessageBox.Show("Data added successfully! \r\nSelect from existing customer before booking", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            MessageBox.Show("Please wait while the booking form loads......", "Loading", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                          bookSeat.ShowDialog();
                            this.Close();


                        }
                        else
                        {//check if value is selected from comboBox, if selected - then newCustomer = false and dont allow to add same details to database
                            MessageBox.Show("Make sure first name and last name contain only letters", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        MessageBox.Show("date should be in past", "Invalid Date", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("Please make sure that all fields are filled", "Empty Fields", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }




                }
                else//newCustomer=false
                {//if its false it means from comboBox
                    int i = comboBox1.SelectedIndex;
     ////check this               customerID = customerObject.custObject[i].Cid;
                  //  customerID = customer.custObject[i].Cid;
                    BookSeatPlan bookSeat = new BookSeatPlan();
                    bookSeat.getCustomerNic(customerID, txtNic.Text, txtFname.Text, txtLname.Text);
                    newCustomer = false;
                    //MessageBox.Show("Data added successfully! \r\nSelect from existing customer before booking", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    MessageBox.Show("Please wait while the booking form loads......", "Loading", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);

                    bookSeat.ShowDialog();
                    this.Close();
                }


              
            }

            catch(IndexOutOfRangeException exc)
            {
                MessageBox.Show("Select the customer before booking seat\n\nError:"+exc.Message,"Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch(Exception ex)
            {
                MessageBox.Show("Exception error " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
