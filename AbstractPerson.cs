using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirlineApplication
{
     abstract class AbstractPerson
    {

        private string fname;
        private string lname;
        private string nic;
        private char gender;
        private DateTime dob;
        private string add1;
        private string add2;
        private string email;
        private string phone;

        public string Fname
        {
            get { return fname; }
            set { fname = value; }
        }


        public string Lname
        {
            get { return lname; }
            set { lname = value; }
        }


        public string Nic
        {
            get { return nic; }
            set { nic = value; }
        }


        public char Gender
        {
            get { return gender; }
            set { gender = value; }
        }


        public DateTime Dob
        {
            get { return dob; }
            set { dob = value; }
        }


        public string Add1
        {
            get { return add1; }
            set { add1 = value; }
        }


        public string Add2
        {
            get { return add2; }
            set { add2 = value; }
        }


        public string Phone
        {
            get { return phone; }
            set { phone = value; }
        }


        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public abstract int addDetails();
        public abstract void updateDetails(int id);

    }
}
