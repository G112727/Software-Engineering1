﻿using System;
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

    public partial class Form4 : Form
    {
        

        string connectionString;

        public Form4(string connectionString)
        {
            InitializeComponent();
            this.connectionString = connectionString;

        }

        private void btnAssignMembershipType_Click(object sender, EventArgs e)
        {
           
           

        }


        private void LoadUserBookings()

        {
            
            try
            {
                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT * FROM user_bookings"; // Adjust columns if needed
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
        }

       

        private void btnAssignMembershipType_Click_1(object sender, EventArgs e)
        {
           
        }


        private void btnApproveMember_Click_1(object sender, EventArgs e)
        {
            //string username = txtUsernameToApprove.Text.Trim();

            //if (string.IsNullOrEmpty(username))
            //{
            //    MessageBox.Show("Please enter a username to approve.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}

            //using (MySqlConnection connection = new MySqlConnection(connectionString))
            //{
            //    try
            //    {
            //        connection.Open();
            //        string query = "UPDATE Users SET IsApproved = 1 WHERE Username = @username";
            //        MySqlCommand command = new MySqlCommand(query, connection);
            //        command.Parameters.AddWithValue("@username", username);

            //        int rowsAffected = command.ExecuteNonQuery();
            //        if (rowsAffected > 0)
            //        {
            //            MessageBox.Show("Member approved successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            MessageBox.Show("Username not found or already approved.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //    }
            //}
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
            string MembershipType = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrEmpty(MembershipType))
            {
                MessageBox.Show("Please select a membership type.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                    SET IsApproved = TRUE,
                        MembershipType = @MembershipType
                    WHERE UserID = @userId";
                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@userId", userId);
                        command.Parameters.AddWithValue("@MembershipType", MembershipType);


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

        private void txtUsernameToApprove_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtUsernameToAssign_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbMembershipType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dgvPendingApprovals_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}