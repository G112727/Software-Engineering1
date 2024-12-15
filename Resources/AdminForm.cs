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

    public partial class AdminForm : Form
    {
        

        string connectionString;

        public AdminForm(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;
            LoadUserDetails();

        }

        private void LoadUserDetails(string searchQuery = "")
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();

                    // Modify query to sort the searched username to the top
                    string query = @"SELECT Username, MembershipType, Role, PredominantInterest 
                             FROM users";

                    if (!string.IsNullOrEmpty(searchQuery))
                    {
                        query += @" WHERE Username LIKE @searchQuery 
                            ORDER BY CASE 
                                WHEN Username LIKE @exactSearch THEN 1 
                                ELSE 2 
                              END, Username";
                    }

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        if (!string.IsNullOrEmpty(searchQuery))
                        {
                            command.Parameters.AddWithValue("@searchQuery", $"%{searchQuery}%");
                            command.Parameters.AddWithValue("@exactSearch", searchQuery);
                        }

                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            dataGridView2.DataSource = dataTable;
                            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading user details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void InitializeAutoComplete()
        {
            AutoCompleteStringCollection autoComplete = new AutoCompleteStringCollection();

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT Username FROM users";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            autoComplete.Add(reader.GetString("Username"));
                        }
                    }

                    textBox1.AutoCompleteCustomSource = autoComplete;
                    textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                    textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while setting up autocomplete: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void LoadUserBookings()

        {
            
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM user_bookings"; 
                    MySqlCommand cmd = new MySqlCommand(query, conn);

                    MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                    DataTable bookingsTable = new DataTable();

                    adapter.Fill(bookingsTable);

                    // Display the data in a DataGridView
                    dataGridView1.DataSource = bookingsTable;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user bookings: " + ex.Message);
            }
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            LoadPendingApprovals();
            LoadUserBookings();
            InitializeAutoComplete();

        }

    
        private void LoadPendingApprovals()
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT UserID, Username FROM Users WHERE IsApproved = FALSE";
                    MySqlDataAdapter adapter = new MySqlDataAdapter(query, connection);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    // Bind the data to the DataGridView
                    dgvPendingApprovals.DataSource = dataTable;
                    dgvPendingApprovals.Columns["UserID"].Visible = false; // Hide UserID column
                    dgvPendingApprovals.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error loading pending approvals: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (dgvPendingApprovals.SelectedRows.Count > 0)
            {
                int userId = Convert.ToInt32(dgvPendingApprovals.SelectedRows[0].Cells["UserID"].Value);
                string username = dgvPendingApprovals.SelectedRows[0].Cells["Username"].Value.ToString();

                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        string query = @"
                                        UPDATE Users
                                        SET IsApproved = TRUE
                                        WHERE UserID = @userId";

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@userId", userId);
                     


                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show($"User '{username}' has been approved!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            LoadPendingApprovals(); // Refresh the pending approvals list
                        }
                        else
                        {
                            MessageBox.Show("Approval failed. Please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error approving user: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a user to approve.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm();
            dashboardForm.Show();
            this.Close();
        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
            string searchQuery = textBox1.Text.Trim();
            LoadUserDetails(searchQuery);
        }
    }
}
