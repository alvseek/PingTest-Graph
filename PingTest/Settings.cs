using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Media;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.AccessControl;

namespace PingTest
{
    public partial class Settings : Form
    {
        ToolTip errorToolTip = new ToolTip();
        ToolTip errorRegistryToolTip = new ToolTip();
        //Creates a layered window.
        private const uint WS_EX_LAYERED = 0x00080000;

        //Specifies that a window created with this style should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
        //The window appears transparent because the bits of underlying sibling windows have already been painted.
        private const uint WS_EX_TRANSPARENT = 0x00000020;

        private const int GWL_EXSTYLE = -20;

        // autorun key address
        RegistryKey rk; //= Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
        bool registryAccessAutoRun = true;
        string keyPathAutoRun = "SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run";
        bool rkExist = false;

        //load form1 object
        private Form1 openForm1;

        //Form1 openForm1 = Application.OpenForms["Form1"] as Form1;

        string usedIpAddress = Properties.Settings.Default.PingIPAddress;
        string newIpAddress;

        //IP address text and value
        private class ComboItem
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }

        #region win32 on the fly unclickable function
        internal static class NativeMethods
        {
            [DllImport("user32.dll", EntryPoint = "GetWindowLong")]
            public static extern IntPtr GetWindowLongPtr32(IntPtr hWnd, int nIndex);

            [DllImport("user32.dll", EntryPoint = "GetWindowLongPtr")]
            public static extern IntPtr GetWindowLongPtr64(IntPtr hWnd, int nIndex);

            [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
            public static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

            [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
            public static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);
        }
        // This static method is required because Win32 does not support
        // GetWindowLongPtr directly
        public static IntPtr GetWindowLongPtr(IntPtr hWnd, int nIndex)
        {
            if (IntPtr.Size == 8)
                return NativeMethods.GetWindowLongPtr64(hWnd, nIndex);
            else
                return NativeMethods.GetWindowLongPtr32(hWnd, nIndex);
        }

        // This helper static method is required because the 32-bit version of user32.dll does not contain this API
        // (on any versions of Windows), so linking the method will fail at run-time. The bridge dispatches the request
        // to the correct function (GetWindowLong in 32-bit mode and GetWindowLongPtr in 64-bit mode)
        public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return NativeMethods.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(NativeMethods.SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }


        #endregion

        public Settings(Form1 form1Ref)
        {
            openForm1 = form1Ref;
            InitializeComponent();
            this.AcceptButton = okButton;
            transparencyTrackBar.Value = Properties.Settings.Default.Tranparency;
            CheckLicense();
            LoadIPAddress();
            CheckStartup();
            CheckAlwaysOnTop();
            CheckClickable();
        }

        public void CheckLicense()
        {
            AppFunction appFunction = new AppFunction();
            bool errorEx = false;
            string licenseTitle = appFunction.CheckLicenseString(Properties.Settings.Default.LicenseCode);
            if (licenseTitle == string.Empty) licenseTitle = appFunction.CheckLicense(AppFunction.LicenseType.Title, ref errorEx);

            if (licenseTitle != string.Empty)
            {
                licenseLabel.Text = licenseTitle + " License";
            }
            else
            {
                licenseLabel.Text = "Free License";
            }
        }

        #region Initial Load Settings
        private void LoadIPAddress()
        {
            ipAddressComboBox.DataSource = new ComboItem[]
            {
                new ComboItem { Text = "(8.8.8.8) Google DNS-a", Value = "8.8.8.8"  },
                new ComboItem { Text = "(8.8.4.4) Google DNS-b", Value = "8.8.4.4"  },
                new ComboItem { Text = "(1.1.1.1) Cloudflare", Value = "1.1.1.1"  },
                new ComboItem { Text = "(1.0.0.1) Cloudflare", Value = "1.0.0.1"  },
                new ComboItem { Text = "(208.67.222.222) Open DNS-1", Value = "208.67.222.222"  },
                new ComboItem { Text = "(208.67.220.220) Open DNS-2", Value = "208.67.220.220"  },
                new ComboItem { Text = "Manually input IP Address", Value = "manual"  },
            };
            ipAddressComboBox.DisplayMember = "Text";
            ipAddressComboBox.ValueMember = "Value";
            ipAddressComboBox.SelectedValue = usedIpAddress;
            string[] ipAddressSplit = usedIpAddress.Split('.');
            this.textAddress1.Text = ipAddressSplit[0];
            this.textAddress2.Text = ipAddressSplit[1];
            this.textAddress3.Text = ipAddressSplit[2];
            this.textAddress4.Text = ipAddressSplit[3];
        }

        private void CheckStartup()
        {
            try
            {
                rk = Registry.CurrentUser.OpenSubKey(keyPathAutoRun, RegistryKeyPermissionCheck.ReadSubTree);
                if (rk.GetValue("PingTest") == null)
                {
                    startupChkBox.Checked = false;
                }
                else
                {
                    if (rk.GetValue("PingTest").ToString() == Application.ExecutablePath)
                    {
                        startupChkBox.Checked = true;
                        rkExist = true;
                    }
                }
                rk.Close();
            }
            catch
            {
                
                //MessageBox.Show("Cannot read Windows start up Run Registry key!\n\nPlease allow this application to have read and write control of current user Windows Registry", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                startupChkBox.Enabled = false;
                runatStartupLabel.Enabled = false;
                registryErrorLabel.Visible = true;
                registryAccessAutoRun = false;
            }
        }

        private void CheckAlwaysOnTop()
        {
            alwaysOnTopChkBox.Checked = openForm1.TopMost;
        }

        private void CheckClickable()
        {
            clickableChkBox.Checked = Properties.Settings.Default.Clickable;
        }
        #endregion

        #region Ok button function
        private void okButton_Click(object sender, EventArgs e)
        {
            if (CheckIPAddress() && CheckTransparency())
            {
                SetStartup(this.startupChkBox.Checked);
                //if 100 set the transparency to 99 first then make unclickable
                SetTransparency();
                SetClickable();
                SetTopMost();
                bool ipChanged = false;
                if (newIpAddress != usedIpAddress)
                {
                    Properties.Settings.Default.PingIPAddress = newIpAddress;
                    Properties.Settings.Default.Save();
                    openForm1.reset.Cancel();
                    ipChanged = true;

                }
                this.DialogResult = DialogResult.OK;
                this.Close();
                if (ipChanged) MessageBox.Show("Target IP Address changed to " + newIpAddress, "IP Address changed", MessageBoxButtons.OK, MessageBoxIcon.None);
            }
        }

        private bool CheckIPAddress()
        {
            try
            {
                if (ipAddressComboBox.SelectedValue.ToString() == "manual")
                {
                    newIpAddress = IPAddress.Parse(textAddress1.Text + "." + textAddress2.Text + "." + textAddress3.Text + "." + textAddress4.Text).ToString();
                }
                else
                {
                    newIpAddress = (string)ipAddressComboBox.SelectedValue;
                }
                return true;
            }
            catch
            {
                MessageBox.Show("IP Address is not valid", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool CheckTransparency()
        {
            if (transparencyTrackBar.Value == 0)
            {
                MessageBox.Show("Application will not be visible!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private void SetStartup(bool autoRun)
        {
            if (registryAccessAutoRun)
            {
                try
                {
                    rk = Registry.CurrentUser.OpenSubKey(keyPathAutoRun, RegistryKeyPermissionCheck.ReadWriteSubTree);
                    if (autoRun && !rkExist)
                        rk.SetValue("PingTest", Application.ExecutablePath);
                    else if (!autoRun && rkExist)
                        rk.DeleteValue("PingTest", false);
                    rk.Close();
                }
                catch
                {
                    MessageBox.Show("Cannot write Registry key!\n\nYou need to give the application a permission to write the Windows Current User Registry\n\nThis settings cannot be changed: Run at Startup", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void SetTopMost()
        {
            if (openForm1.TopMost != this.alwaysOnTopChkBox.Checked)
            {
                openForm1.Hide();
                openForm1.TopMost = this.alwaysOnTopChkBox.Checked;
                openForm1.ShowInTaskbar = !openForm1.TopMost;
                openForm1.Show();
                Properties.Settings.Default.AlwaysOnTop = openForm1.TopMost;
                Properties.Settings.Default.Save();
            }
        }

        private void SetTransparency()
        {
            if (transparencyTrackBar.Value != Properties.Settings.Default.Tranparency)
            {
                openForm1.Opacity = (float)transparencyTrackBar.Value / 100;
                Properties.Settings.Default.Tranparency = transparencyTrackBar.Value;
                Properties.Settings.Default.Save();
            }
        }

        private void SetClickable()
        {
            if (clickableChkBox.Checked != Properties.Settings.Default.Clickable)
            {
                Properties.Settings.Default.Clickable = clickableChkBox.Checked;
                IntPtr initialStyle = GetWindowLongPtr(this.Handle, -20);
                if (!clickableChkBox.Checked)
                {
                    if (openForm1.Opacity == 1) openForm1.Opacity = (float)99.99 / 100;
                    SetWindowLongPtr(new HandleRef(openForm1, openForm1.Handle), -20, (IntPtr)((int)initialStyle | WS_EX_LAYERED | WS_EX_TRANSPARENT));
                    Properties.Settings.Default.Clickable = false;
                }
                else
                {
                    SetWindowLongPtr(new HandleRef(openForm1, openForm1.Handle), -20, (IntPtr)((int)initialStyle & ~WS_EX_LAYERED & ~WS_EX_TRANSPARENT));
                    SetWindowLongPtr(new HandleRef(openForm1, openForm1.Handle), -20, (IntPtr)((int)initialStyle | WS_EX_LAYERED));
                    openForm1.Opacity = 0.5;
                    Properties.Settings.Default.Clickable = true;
                }
                openForm1.Hide();
                openForm1.ShowInTaskbar = openForm1.TopMost;
                openForm1.ShowInTaskbar = !openForm1.TopMost;
                openForm1.Show();
                Properties.Settings.Default.Save();
            }
        }


        #endregion

        #region Manual Input IP Address validation

        private void ipAddressComboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            if (ipAddressComboBox.SelectedValue == null)
            {
                ipAddressComboBox.SelectedValue = "manual";
            }
            if (ipAddressComboBox.SelectedValue.ToString() == "manual")
            {
                panel1.Visible = true;
            }
            else
            {
                panel1.Visible = false;
            }
        }

        private void textAddress1_TextChanged(object sender, EventArgs e)
        {
            if (textAddress1.TextLength == textAddress1.MaxLength)
            {
                textAddress2.Focus();
            }
        }

        private void textAddress2_TextChanged(object sender, EventArgs e)
        {
            if (textAddress2.TextLength == textAddress2.MaxLength)
            {
                textAddress3.Focus();
            }
        }

        private void textAddress3_TextChanged(object sender, EventArgs e)
        {
            if (textAddress3.TextLength == textAddress3.MaxLength)
            {
                textAddress4.Focus();
            }
        }

        //filter digit only input
        private void textAddress1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                errorToolTip.Show("IP Address input has to be a number", sender as TextBox, 8, 15, 2000);
                e.Handled = true;
                SystemSounds.Beep.Play();
            }
        }

        //filter if pasted text is IP address compatible
        private void textAddress1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.V && e.Modifiers == Keys.Control)
            {
                if (Clipboard.ContainsText())
                {
                    string[] checkIpAddress = Clipboard.GetText().Split('.');
                    bool ipPacketValid = true;
                    if (checkIpAddress.Count() == 4)
                    {
                        foreach (string packet in checkIpAddress)
                        {
                            //check inner ipPacketValid false
                            if (ipPacketValid)
                            {
                                if (packet.Length >= 1 && packet.Length <= 3)
                                {
                                    foreach (char c in packet)
                                    {
                                        if (!char.IsDigit(c))
                                        {
                                            ipPacketValid = false;
                                            break;
                                        }
                                    }
                                }
                                else
                                {
                                    ipPacketValid = false;
                                    break;
                                }
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    else
                    {
                        ipPacketValid = false;

                        if (checkIpAddress.Count() == 1)
                        {
                            if (!Clipboard.GetText().All(char.IsDigit))
                            {
                                errorToolTip.Show("IP Address input has to be a number", sender as TextBox, 8, 15, 2000);
                                SystemSounds.Beep.Play();
                                e.SuppressKeyPress = true;
                                return;
                            }
                            else
                            {
                                e.SuppressKeyPress = false;
                                return;
                            }
                        }
                    }

                    if (ipPacketValid)
                    {
                        this.textAddress1.Text = checkIpAddress[0];
                        this.textAddress2.Text = checkIpAddress[1];
                        this.textAddress3.Text = checkIpAddress[2];
                        this.textAddress4.Text = checkIpAddress[3];
                        e.SuppressKeyPress = true;
                        return;
                    }
                    else
                    {
                        errorToolTip.Show("Pasted IP Address format not valid", this.panel1, 20, 20, 2000);
                        SystemSounds.Beep.Play();
                        e.SuppressKeyPress = true;
                        return;
                    }
                }
            }
        }

        #endregion

        #region dev only showthreads function
        private void button1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process procces = System.Diagnostics.Process.GetCurrentProcess();
            System.Diagnostics.ProcessThreadCollection threadCollection = procces.Threads;

            string threads = string.Empty;

            foreach (System.Diagnostics.ProcessThread proccessThread in threadCollection)
            {
                threads += string.Format("Thread Id: {0}, ThreadState: {1}\r\n", proccessThread.Id, proccessThread.ThreadState);
            }

            MessageBox.Show(threads);
        }
        #endregion

        private void transparencyTrackBar_ValueChanged(object sender, EventArgs e)
        {
            int moreThanHalf = 0;
            if (transparencyTrackBar.Value % 5 > 3) { moreThanHalf = 1; }
            transparencyTrackBar.Value = (((int)transparencyTrackBar.Value / 5) + (moreThanHalf)) * 5;
            transparencyLabel.Text = transparencyTrackBar.Value.ToString() + "%";
            openForm1.Opacity = (float)transparencyTrackBar.Value / 100;
            if (transparencyTrackBar.Value == 50)
            {
                transparencyLabel.Text += " (default)";
            }
        }

        private void Settings_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (this.DialogResult != DialogResult.OK)
                {
                    openForm1.Opacity = (float)Properties.Settings.Default.Tranparency / 100;
                }
            }
        }

        private void licenseLabel_Click(object sender, EventArgs e)
        {
            License licenseForm = new License(this);
            licenseForm.ShowDialog();
            licenseForm.Dispose();
        }

        private void clickableChkBox_CheckedChanged(object sender, EventArgs e)
        {
            if (clickableChkBox.Checked == false)
            {
                MessageBox.Show("You can make it clickable again from the tray icon", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
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

        private void registryErrorLabel_MouseHover(object sender, EventArgs e)
        {
            string registryErrorString = "\nYou need to give the application a permission to read and write the Windows Current User Registry.";
            errorRegistryToolTip.ToolTipTitle = "Cannot read Windows start up Run Registry key!";
            errorRegistryToolTip.Show(registryErrorString, registryErrorLabel, 15,30);
        }

        private void registryErrorLabel_MouseLeave(object sender, EventArgs e)
        {
            errorRegistryToolTip.Hide(registryErrorLabel);
        }
    }
}
