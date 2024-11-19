using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace Software_Engineering1
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string connectionString = "Server=myServerAddress;Database=myDataBase;User Id=myUsername;Password=myPassword;";
            SqlConnection connection = new SqlConnection(connectionString);


            try
            {
                // Open the connection to the database
                connection.Open();

                Console.WriteLine("Connection established successfully.");

                // Use the connection to perform database operations
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error connecting to the database: " + ex.Message);
            }
            finally
            {
                // Close the connection when done
                connection.Close();
                Console.WriteLine("Connection closed.");
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
