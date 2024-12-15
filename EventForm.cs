using System;
using System.Windows.Forms;
using MySqlConnector;
using static Software_Engineering1.DashboardForm;


//SUMMARY 
//THIS FORM IS MAINLY USED FOR SEARCHING FOR SPECIFIC EVENTS AS WELL AS BOOK EVENTS 
//I HAVE USED PHPMYADMIN AS MY DATABASE SOFTWARE 
//SUMMARY

namespace Software_Engineering1
{
    
    public partial class EventForm : Form
    {
        string connectionString = "server=localhost;uid=root;pwd=;database=theevents"; // connection string 
        public EventForm()
        {
            InitializeComponent(); //initializes Event form
           
        }

        private void Form1_Load(object sender, EventArgs e) //Retreives username and displays profile label if visible 
        {
            label18.Text = $"{LoggedInUser.Username}";
            label19.Visible = LoggedInUser.IsLoggedIn;
        }

    
        private void button2_Click(object sender, EventArgs e) // button click to go to dahsboard 
        {
            DashboardForm dashboardForm = new DashboardForm();
            dashboardForm.Show();
            this.Close(); // closes current form 
        }

        private void button3_Click(object sender, EventArgs e) // button click to go to membership form 
        {
            membershipForm membershipForm = new membershipForm();
            membershipForm.Show();
            this.Hide(); //hides current form 
        }

      //SUMMARY 
      // button5_Click  IS MAINLY USED FOR THE SEARCHING SPECIFIC EVENT 
      //AS WELL AS PASS THOES EVENT DETAILS TO THE EventDetailsForm
      //SUMMARY 


        private void button5_Click(object sender, EventArgs e) 
        {  
            string searchedEvent = textBox1.Text.Trim(); //retrieve text from textBox1

            if (string.IsNullOrEmpty(searchedEvent))  //validate if user entered an event name 
            {
                label17.Text = "Please enter an event name."; 
                label17.ForeColor = System.Drawing.Color.Red; //gives label colour 
                return; 
            }
            // connect to database using MYSQL
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open(); //Open database connection

                    //SQL Query to search for event name and retrieve details 

                    string query = "SELECT `event_id` , `event_description` , `event_date` FROM events1 WHERE `event_name` = @eventName LIMIT 1;";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@eventName", searchedEvent); //searches using event name 

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Extrat details from details 
                                int eventId = reader.GetInt32("event_id");
                                string eventDescription = reader.GetString("event_description");
                                DateTime eventDate = reader.GetDateTime("event_date");

                                label17.Text = $"Event found: {searchedEvent} on {eventDate:yyyy-MM-dd}"; // displays event name and date 
                                label17.ForeColor = System.Drawing.Color.Green; 

                                // Display success popup
                                MessageBox.Show("Event is available! Click 'Details' to see more.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Enable the details button and attach click handlerS
                                button14.Tag = (eventId, searchedEvent, eventDescription, eventDate); // Pass event details using the button's Tag property
                                button14.Enabled = true;
                            }
                            else
                            {
                                label17.Text = "Event not found.";  // text if event is not found 
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


      
       
        private void button10_Click(object sender, EventArgs e) // event handler to see if user is logged in to book events 
        {
            if (!LoggedInUser.IsLoggedIn)  
            {
                //checks if user is logged in and displays warning message if not
                MessageBox.Show("Please log in to access this feature.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning); 
                this.Hide(); //hides current form 
                LoginForm loginForm = new LoginForm();
                loginForm.Show(); // redirects to another form 
            }

            else
            {
                // if user is logged in 
                string eventname = Mindfull_Kickboxing.Name; // event name 
                eventname = eventname.Replace("_", " "); //replaces underscores with spaces in event name 
                BookingForm bookingForm = new BookingForm(); // moves to booking form 
                bookingForm.LoadEventDetail(eventname); //loads the details to booking form 
                bookingForm.Show();
            }

        }

    //SUMMARY 
    //THIS CODE IS MAINLY USED TO TRANSFER EVENT DETAILS TO THE EventDetailsForm
    //SUMMARY

        private void button14_Click(object sender, EventArgs e)
        // event handler to retrive event details with the help button14.tg
        {

            if (button14.Tag is ValueTuple<int, string, string, DateTime> eventDetails) 
            {
                int eventId = eventDetails.Item1;
                string eventName = eventDetails.Item2;
                string eventDescription = eventDetails.Item3;
                DateTime eventDate = eventDetails.Item4;

                // Open the EventDetailsForm with the retrieved event details 
                EventDetailsForm eventDetailsForm = new EventDetailsForm(eventId, eventName, eventDescription, eventDate);
                eventDetailsForm.Show();

                //checks is form is null or disposed off
                if (eventDetailsForm == null || eventDetailsForm.IsDisposed)
                {
                    // Creates a new instance if forms not available 
                    eventDetailsForm = new EventDetailsForm(eventId, eventName, eventDescription, eventDate);
                    eventDetailsForm.Show();
                }
                else
                {
                    // Brings the  form to the front
                    eventDetailsForm.BringToFront();
                }

                }
                else
                {
                    // if button14 tag is unable to get event details displays error message 
                    MessageBox.Show("No event details available. Please search for an event first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }


        }

  

        private void button12_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
        }

        private void button7_Click(object sender, EventArgs e) //event handler for event name Turn_Up_and_Write.Name;
        {
            if (!LoggedInUser.IsLoggedIn) 
            {
                //checks if user is logged in and displays warning message if not
                MessageBox.Show("Please log in to access this feature.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                LoginForm form5 = new LoginForm();
                form5.Show();
            }

            else
            {
                string eventname = Turn_Up_and_Write.Name; //event name
                eventname = eventname.Replace("_", " ");//replaces underscores with spaces in event name
                BookingForm form2 = new BookingForm();
                form2.LoadEventDetail(eventname); //loads the details to booking form 
                form2.Show();
            }
        }

        private void label18_Click(object sender, EventArgs e)
        {
            LoginForm form5 = new LoginForm();
            form5.Show();

        }

        private void Partner_Assisted_Yoga_Click(object sender, EventArgs e)  //event handler for event name Partner_Assisted_Yoga_Click
        {
            if (!LoggedInUser.IsLoggedIn)
            {
                //checks if user is logged in and displays warning message if not
                MessageBox.Show("Please log in to access this feature.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                LoginForm form5 = new LoginForm();
                form5.Show();
            }

            else
            {
                string eventname = Partner_Assisted_Yoga.Name;
                eventname = eventname.Replace("_", " "); // replaces underscores with spaces in event name
                BookingForm form2 = new BookingForm();
                form2.LoadEventDetail(eventname);//loads the details to booking form 
                form2.Show();
            }
        }

        private void Focusing_Workshop_Click(object sender, EventArgs e) //event handler for event name Focusing_Workshop_Click
        {
            if (!LoggedInUser.IsLoggedIn)
            {
                //checks if user is logged in and displays warning message if not
                MessageBox.Show("Please log in to access this feature.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                LoginForm form5 = new LoginForm(); 
                form5.Show();
            }

            else
            {
                string eventname = Focusing_Workshop.Name;
                eventname = eventname.Replace("_", " "); // replaces underscores with spaces in event name
                BookingForm form2 = new BookingForm();
                form2.LoadEventDetail(eventname); //loads the details to booking form
                form2.Show();
            }

        }

        private void Art_Gathering_Click(object sender, EventArgs e) //event handler for event name Art_Gathering_Click
        {
              if (!LoggedInUser.IsLoggedIn)
            {
                MessageBox.Show("Please log in to access this feature.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                LoginForm form5 = new LoginForm();
                form5.Show();
            }

            else
            {
                string eventname = Art_Gathering.Name;
                eventname = eventname.Replace("_", " "); // replaces underscores with spaces in event name
                BookingForm form2 = new BookingForm();//loads the details to booking form
                form2.LoadEventDetail(eventname);
                form2.Show();
            }
        }

        private void Monthly_Restorative_Rest_Click(object sender, EventArgs e) //event handler for event name  Monthly_Restorative_Rest_Click
        {
            if (!LoggedInUser.IsLoggedIn)
            {
                MessageBox.Show("Please log in to access this feature.", "Access Denied", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Hide();
                LoginForm form5 = new LoginForm();
                form5.Show();
            }

            else
            {
                string eventname = Monthly_Restorative_Rest.Name; // event name 
                eventname = eventname.Replace("_", " "); //replaces underscores with spaces in event name
                BookingForm form2 = new BookingForm();
                form2.LoadEventDetail(eventname); //loads the details to booking form
                form2.Show();
            }
        }

        private void label19_Click(object sender, EventArgs e) // event handler for label click 
        {
           
            string currentUsername = LoggedInUser.Username;  
            ProfilePage profilePage = new ProfilePage(currentUsername); // creates instance for ProfilePage 
            profilePage.Show();
        }

   
    }
}
