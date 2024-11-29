using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Software_Engineering1
{
    public partial class Form6 : Form
    {
        public Form6()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            Form1 form1 = new Form1();
            form1.Show();

            this.Hide();



        }

        
        public static class LoggedInUser
        {
            private static string _username = "Guest";
            private static bool _isLoggedIn = false;
            public static string Username
            {
                get => _username;
                set => _username = value;
            }


            public static bool IsLoggedIn
            {
                get => _isLoggedIn;
                set => _isLoggedIn = value;
            }


        }

        private void label2_Click(object sender, EventArgs e)
        {

            LoggedInUser.Username = "Guest";
            LoggedInUser.IsLoggedIn = false;

            Form5 form5 = new Form5();
            this.Hide();
            form5.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {

            //LoggedInUser.Username = "Guest";
            //LoggedInUser.IsLoggedIn = false;

            //Form5 form5 = new Form5();
            //this.Hide();
            //form5.Show();
            
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            label2.Text = $"{LoggedInUser.Username}";
            
        }
    }
}
