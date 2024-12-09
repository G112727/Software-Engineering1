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
    public partial class ShopForm : Form
    {
        public ShopForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DashboardForm dashboardForm = new DashboardForm();
            dashboardForm.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            EventForm eventForm = new EventForm();
            eventForm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            membershipForm membershipform = new membershipForm();
            membershipform.Show();
            this.Close();
        }
    }
}
