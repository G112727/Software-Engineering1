using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySqlConnector;

namespace Software_Engineering1
{
    public partial class ProfilePage : Form
    {
        private string connectionString = "server=localhost;database=theevents;uid=root;pwd=;";
        private string currentUsername;
        public ProfilePage(string currentUsername)
        {
            InitializeComponent();
            this.currentUsername = currentUsername;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string newUsername = textBox1.Text.Trim();
            string predominantInterest = comboBox1.SelectedItem?.ToString();
            DateTime dateOfBirth = dateTimePicker1.Value;

            if (string.IsNullOrEmpty(newUsername) || string.IsNullOrEmpty(predominantInterest))
            {
                MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "UPDATE Users SET Username = @newUsername, PredominantInterest = @interest, DateOfBirth = @dob WHERE Username = @currentUsername";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@newUsername", newUsername);
                        command.Parameters.AddWithValue("@interest", predominantInterest);
                        command.Parameters.AddWithValue("@dob", dateOfBirth);
                        command.Parameters.AddWithValue("@currentUsername", currentUsername);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            currentUsername = newUsername; // Update the currentUsername to the new one
                            button2.Enabled = true;
                            button1.Enabled = false;
                            DisableEditingFields();
                        }
                        else
                        {
                            MessageBox.Show("Failed to update profile.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error updating profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ProfilePage_Load(object sender, EventArgs e)
        {
            label4.Text = $"Welcome to your Profile {currentUsername}!";

            // Load user details from database
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Username, PredominantInterest, DateOfBirth FROM Users WHERE Username = @username";
                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@username", currentUsername);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                textBox1.Text = reader.GetString("Username");
                                comboBox1.SelectedItem = reader["PredominantInterest"].ToString();
                                dateTimePicker1.Value = reader["DateOfBirth"] == DBNull.Value
                                    ? DateTime.Now
                                    : Convert.ToDateTime(reader["DateOfBirth"]);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading profile: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }   }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Enabled = true;
            dateTimePicker1.Enabled = true;
            comboBox1.Enabled = true;

            // Enable the Save Changes button
            button1.Enabled = true;

            // Optionally, disable the Edit button
            button2.Enabled = false;
        }


        private void DisableEditingFields()
        {
            textBox1.Enabled = false;
            dateTimePicker1.Enabled = false;
            comboBox1.Enabled = false;
        }

      
    }
}
