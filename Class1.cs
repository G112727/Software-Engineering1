using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace Software_Engineering1
{
    internal class Class1
    {


        public static class BookingHelper
        {
            private static string connectionString = "server=localhost;database=theevents;uid=root;pwd=;";

            public static void BookTicket( string username ,string eventName, string guestName, string guestEmail, int ticketCount)
            {
                
                using (var conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string insertBookingQuery = @"
                    INSERT INTO user_bookings (username, event_name, booking_time, ticket_count)
                    VALUES (@username, @eventName, @bookingTime, @ticketCount)";

                        using (var cmd = new MySqlCommand(insertBookingQuery, conn))
                        {
                            
                            cmd.Parameters.AddWithValue("@username", username);
                            cmd.Parameters.AddWithValue("@eventName", eventName);
                            cmd.Parameters.AddWithValue("@bookingTime", DateTime.Now);
                            cmd.Parameters.AddWithValue("@ticketCount", ticketCount);

                            cmd.ExecuteNonQuery();
                        }

                        MessageBox.Show($"Successfully booked {ticketCount} ticket(s) for {eventName}!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error booking ticket: " + ex.Message);
                    }
                }
            }

   

            public static EventDetails RetrieveEvent(string eventName)
            {
                using (var conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();

                        string retrieveeventQuery = @"
                SELECT event_id, event_name, event_description, event_date, event_time
                FROM events1
                WHERE event_name = @eventName";

                        using (var cmd = new MySqlCommand(retrieveeventQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("@eventName", eventName);

                            using (var reader = cmd.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    return new EventDetails
                                    {
                                        EventId = reader.GetInt32("event_id"),
                                        EventName = reader.GetString("event_name"),
                                        EventDescription = reader.GetString("event_description"),
                                        EventDate = reader.GetDateTime("event_date"),
                                        EventTime = reader.GetTimeSpan("event_time")
                                    };
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error retrieving event: " + ex.Message);
                    }
                }

                return null; // Return null if the event is not found or an error occurs
            }

        }

    }




}

