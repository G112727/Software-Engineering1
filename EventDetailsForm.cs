using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;

namespace Software_Engineering1
{
    public partial class EventDetailsForm : Form
    {
        private int EventId;
        private string EventName;
        private string EventDescription;
        private DateTime EventDate;


        public EventDetailsForm(int eventid, string eventname, string eventdescription, DateTime eventdate)
        {
            InitializeComponent();
            EventId = eventid;
            EventName = eventname;
            EventDescription = eventdescription;
            EventDate = eventdate;
        }

        
        
        private void EventDetailsForm_Load(object sender, EventArgs e)
        {
            
            label1.Text = $"{EventName}";
            label12.Text = $"Event Date: {EventDate:yyyy-MM-dd}";
            label4.Text = EventDescription;
            label4.MaximumSize = new Size(300, 0); // 300 pixels wide, unlimited height
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Booking Complete.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
