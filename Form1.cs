using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using MySqlConnector;
using static Software_Engineering1.Form6;


namespace Software_Engineering1
{
    
    public partial class Form1 : Form
    {
         
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            label18.Text = $": {LoggedInUser.Username}";
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void button9_Click(object sender, EventArgs e)
        {

        }


        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }

        private void button16_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void roundedPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        { }

        private void button5_Click(object sender, EventArgs e)
        {

            string connectionString = "server=localhost;database=theevents;uid=root;pwd=;";
            string searchedEvent = textBox1.Text.Trim();

            if (string.IsNullOrEmpty(searchedEvent))
            {
                label17.Text = "Please enter an event name.";
                label17.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    string query = "SELECT `event_id` , `event_description` , `event_date` FROM events1 WHERE `event_name` = @eventName LIMIT 1;";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@eventName", searchedEvent);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Event found
                                int eventId = reader.GetInt32("event_id");
                                string eventDescription = reader.GetString("event_description");
                                DateTime eventDate = reader.GetDateTime("event_date");

                                label17.Text = $"Event found: {searchedEvent} on {eventDate:yyyy-MM-dd}";
                                label17.ForeColor = System.Drawing.Color.Green;

                                // Show success popup
                                MessageBox.Show("Event is available! Click 'Details' to see more.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Enable the details button and attach click handler
                                //button14.Click += button14_Click;
                                button14.Tag = (eventId, searchedEvent, eventDescription, eventDate); // Pass event details using the button's Tag property
                                button14.Enabled = true;
                            }
                            else
                            {
                                label17.Text = "Event not found.";
                                label17.ForeColor = System.Drawing.Color.Red;

                                // Disable the details button
                                button14.Enabled = false;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        
        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click_1(object sender, EventArgs e)
        {

        }

        private void roundedPanel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click_1(object sender, EventArgs e)
        {

        }

        private void roundedPanel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (!LoggedInUser.IsLoggedIn)
            {
                MessageBox.Show("Please log in to access this feature.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                Form5 form5 = new Form5();
                form5.Show();
            }

            else
            {
                string eventname = Mindfull_Kickboxing.Name;
                eventname = eventname.Replace("_", " ");
                Form2 form2 = new Form2();
                form2.LoadEventDetail(eventname);
                form2.Show();
            }

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)

        {

            if (button14.Tag is ValueTuple<int, string, string, DateTime> eventDetails)
            {
                int eventId = eventDetails.Item1;
                string eventName = eventDetails.Item2;
                string eventDescription = eventDetails.Item3;
                DateTime eventDate = eventDetails.Item4;

                // Open the EventDetailsForm
                EventDetailsForm eventDetailsForm = new EventDetailsForm(eventId, eventName, eventDescription, eventDate);
                eventDetailsForm.Show();


                if (eventDetailsForm == null || eventDetailsForm.IsDisposed)
                {
                    // Create a new instance if it's null or disposed
                    eventDetailsForm = new EventDetailsForm(eventId, eventName, eventDescription, eventDate);
                    eventDetailsForm.Show();
                }
                else
                {
                    // Bring the existing form to the front
                    eventDetailsForm.BringToFront();
                }

                }
                else
                {
                    MessageBox.Show("No event details available. Please search for an event first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


            //if (button14.Tag is Tuple<int, string, string, DateTime> eventDetails)
            //{




            //    //int eventId = eventDetails.Item1;
            //    //string eventName = eventDetails.Item2;
            //    //string eventDescription = eventDetails.Item3;
            //    //DateTime eventDate = eventDetails.Item4;

            //    //EventDetailsForm eventDetailsForm = new EventDetailsForm(eventId, eventName, eventDescription, eventDate);
            //    //eventDetailsForm.Show();

            //}


        }

        private void button15_Click_1(object sender, EventArgs e)
        {
           
        }

        private void button16_Click_1(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (!LoggedInUser.IsLoggedIn)
            {
                MessageBox.Show("Please log in to access this feature.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                Form5 form5 = new Form5();
                form5.Show();
            }

            else
            {
                string eventname = Turn_Up_and_Write.Name;
                eventname = eventname.Replace("_", " ");
                Form2 form2 = new Form2();
                form2.LoadEventDetail(eventname);
                form2.Show();
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            Form5 form5 = new Form5();
            form5.Show();

        }

        private void Partner_Assisted_Yoga_Click(object sender, EventArgs e)
        {
            if (!LoggedInUser.IsLoggedIn)
            {
                MessageBox.Show("Please log in to access this feature.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                Form5 form5 = new Form5();
                form5.Show();
            }

            else
            {
                string eventname = Partner_Assisted_Yoga.Name;
                eventname = eventname.Replace("_", " ");
                Form2 form2 = new Form2();
                form2.LoadEventDetail(eventname);
                form2.Show();
            }
        }

        private void Focusing_Workshop_Click(object sender, EventArgs e)
        {
            if (!LoggedInUser.IsLoggedIn)
            {
                MessageBox.Show("Please log in to access this feature.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                Form5 form5 = new Form5();
                form5.Show();
            }

            else
            {
                string eventname = Focusing_Workshop.Name;
                eventname = eventname.Replace("_", " ");
                Form2 form2 = new Form2();
                form2.LoadEventDetail(eventname);
                form2.Show();
            }

        }

        private void Art_Gathering_Click(object sender, EventArgs e)
        {
              if (!LoggedInUser.IsLoggedIn)
            {
                MessageBox.Show("Please log in to access this feature.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                Form5 form5 = new Form5();
                form5.Show();
            }

            else
            {
                string eventname = Art_Gathering.Name;
                eventname = eventname.Replace("_", " ");
                Form2 form2 = new Form2();
                form2.LoadEventDetail(eventname);
                form2.Show();
            }
        }

        private void Monthly_Restorative_Rest_Click(object sender, EventArgs e)
        {
            if (!LoggedInUser.IsLoggedIn)
            {
                MessageBox.Show("Please log in to access this feature.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                Form5 form5 = new Form5();
                form5.Show();
            }

            else
            {
                string eventname = Monthly_Restorative_Rest.Name;
                eventname = eventname.Replace("_", " ");
                Form2 form2 = new Form2();
                form2.LoadEventDetail(eventname);
                form2.Show();
            }
        }
    }
}
