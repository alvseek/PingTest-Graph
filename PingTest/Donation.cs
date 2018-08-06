using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PingTest
{
    public partial class Donation : Form
    {
        public Donation()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.logo_icon_complete;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AppFunction appFunction = new AppFunction();
            System.Diagnostics.Process.Start(appFunction.DonationLink);
        }
    }
}
