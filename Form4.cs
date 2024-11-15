using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Relational;


namespace Software_Engineering1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string connectionString = "Server=localhost;Database=jeevan;User ID=root;Password=;";

            string query = "SELECT * FROM jeevan1 ";

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                {
                    try
                    {
                        connection.Open();
                        using (MySqlCommand cmd = new MySqlCommand(query, connection))
                        {
                            // Add parameterized query to prevent SQL injection
                            cmd.Parameters.AddWithValue("@searchTerm", "%" + textBox1.Text + "%");

                            // Execute the query and read the results
                            using (MySqlDataReader reader = cmd.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    // Read the data and display it in the label
                                    reader.Read();
                                    string result = reader["names"].ToString();
                                    label1.Text = "Found: " + result;
                                }
                                else
                                {
                                    label1.Text = "No results found.";
                                }
                            }
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Database error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Unexpected error: {ex.Message}");
                    }

                }
            }

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                {
                    try
                    {
                        connection.Open();
                        // Define the query to retrieve data
                        //string squery = "SELECT * FROM your_table_name";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        using (MySqlDataAdapter adapter = new MySqlDataAdapter(command))
                        {
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);

                            // Bind the data to a DataGridView control
                            dataGridView1.DataSource = dataTable;
                            Image.FromFile('');
                        }
                    }
                    catch (MySqlException ex)
                    {
                        MessageBox.Show($"Database error: {ex.Message}");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Unexpected error: {ex.Message}");
                    }

                }
            }
        }
    }
}