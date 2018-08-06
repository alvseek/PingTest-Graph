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
    public partial class License : Form
    {
        Settings openSettings;
        AppFunction appFunction = new AppFunction();
        string initialLicenseCode = string.Empty;       
        public License(Settings settingsRef)
        {
            openSettings = settingsRef;
            InitializeComponent();
            this.AcceptButton = okButton;
            initialLicenseCode = Properties.Settings.Default.LicenseCode;           
            if (initialLicenseCode == string.Empty)
            {
                bool errorEx;
                initialLicenseCode = appFunction.CheckLicense(AppFunction.LicenseType.Value, out errorEx); ;
            }
            
            if (initialLicenseCode != string.Empty)
            {
                removeLicenseButton.Visible = true;
                licenseTxtBox.Text = initialLicenseCode;
            }                      
        }

        private void okButton_Click(object sender, EventArgs e)
        {
            if (licenseTxtBox.Text != string.Empty && (licenseTxtBox.Text != initialLicenseCode))
            {
                string license = appFunction.CheckLicenseString(licenseTxtBox.Text);
                if (!string.IsNullOrEmpty(license))
                {
                    MessageBox.Show("You're now using the " + license + " License!", "License changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    openSettings.licenseLabel.Text = license + " License";
                    bool errorCreatingRegistry;
                    appFunction.CreateLicenseKey(licenseTxtBox.Text, out errorCreatingRegistry);
                    if (errorCreatingRegistry)
                    {
                        MessageBox.Show("Please note:\n\nBecause the application cannot write to Windows Current Registry, the license code will be stored locally", "Note", MessageBoxButtons.OK, MessageBoxIcon.None);
                        Properties.Settings.Default.LicenseCode = licenseTxtBox.Text;
                        Properties.Settings.Default.Save();
                    }              
                }
                else
                {
                    MessageBox.Show("License code is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AppFunction appFunction = new AppFunction();
            System.Diagnostics.Process.Start(appFunction.DonationLink);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (Form.ModifierKeys == Keys.None && keyData == Keys.Escape)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void removeLicenseButton_Click(object sender, EventArgs e)
        {            
            Properties.Settings.Default.LicenseCode = string.Empty;
            Properties.Settings.Default.Save();
            bool removeSuccess = true;

            AppFunction appFunction = new AppFunction();
            bool errorAccess;
            string licenseValue = appFunction.CheckLicense(AppFunction.LicenseType.Value, out errorAccess); ;
            if (!errorAccess && licenseValue != string.Empty)
            {
                bool errorDeletingKey;
                appFunction.DeleteLicenseKey(out errorDeletingKey);
                if (errorDeletingKey)
                {
                    removeSuccess = false;
                    MessageBox.Show("Failed to remove license from Windows Registry\n\nReason: The application has no permission to delete Registry key", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }   
                     
            if (removeSuccess)
            {
                MessageBox.Show("License has been removed", "Information", MessageBoxButtons.OK, MessageBoxIcon.None);                               
            }
            this.Close();
            openSettings.CheckLicense();

        }
    }
}
