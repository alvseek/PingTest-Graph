using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace PingTest
{
    public partial class Form1 : Form
    {
        #region basic Form properties

        #region color
        private Color color1st = Color.White;
        private Color color2nd = Color.Wheat;
        private Color color3rd = Color.Orange;
        private Color color4th = Color.Red;
        private Color color5th = Color.Purple;

        #endregion

        #region clickable constant properties of win32.dll address
        //Creates a layered window.
        private const int WS_EX_LAYERED = 0x80000;

        //Specifies that a window created with this style should not be painted until siblings beneath the window (that were created by the same thread) have been painted.
        //The window appears transparent because the bits of underlying sibling windows have already been painted.
        private const int WS_EX_TRANSPARENT = 0x20;
        #endregion

        // thread ping waiter
        private AutoResetEvent waiter = new AutoResetEvent(false);

        // Invoking thread to set text
        delegate void SetTextCallback(string text);

        private CancellationTokenSource cts = new CancellationTokenSource();
        public CancellationTokenSource reset;

        Series newSeries = new Series();
        #endregion

        #region UI basic set up
        private bool mouseDown;
        private Point lastLocation;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true;
            lastLocation = e.Location;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown)
            {
                this.Location = new Point(
                    (this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);

                this.Update();
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false;
        }

        //set to click thru
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                if (!Properties.Settings.Default.Clickable)
                {
                    cp.ExStyle |= WS_EX_LAYERED | WS_EX_TRANSPARENT;
                }
                return cp;
            }
        }
        #endregion

        public Form1()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.logo_icon_complete;
            LoadFormPosition();
            InitializeToolStripMenu();
            CheckLicense();
            MakeSeries();
            ThreadPool.QueueUserWorkItem(async q => await SetupPingAsync(cts.Token));
        }

        #region initial setting
        private void LoadFormPosition()
        {
            this.Opacity = (float)Properties.Settings.Default.Tranparency / 100;
            if (this.Opacity == 1 && !Properties.Settings.Default.Clickable) this.Opacity = (float)99 / 100;
            if (Properties.Settings.Default.WPosX == -1 && Properties.Settings.Default.WPosY == -1)                
            {
                UIFunction uiFunction = new UIFunction();
                uiFunction.MoveToDefault(this);
                //belum di check bisa apa engga nya
            }
            else
            {
                this.Location = new Point(Properties.Settings.Default.WPosX, Properties.Settings.Default.WPosY);
            }
            this.TopMost = Properties.Settings.Default.AlwaysOnTop;
            this.ShowInTaskbar = !this.TopMost;
        }

        private void InitializeToolStripMenu()
        {
            makeUnClickableToolStripMenuItem.Available = Properties.Settings.Default.Clickable;
            makeClickableToolStripMenuItem.Available = !makeUnClickableToolStripMenuItem.Available;
        }

        private void CheckLicense()
        {
            AppFunction appFunction = new AppFunction();
            bool errorEx;
            string licenseTitle = appFunction.CheckLicenseString(Properties.Settings.Default.LicenseCode);
            if (licenseTitle == string.Empty) licenseTitle = appFunction.CheckLicense(AppFunction.LicenseType.Title, out errorEx);

            if (licenseTitle == string.Empty)
            {
                Donation donationForm = new Donation();
                donationForm.Show();
            }
        }

        private void MakeSeries()
        {
            PingChart.Series.Add(newSeries);
            newSeries.ChartArea = "ChartArea1";
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.Color = Color.White;
            newSeries.MarkerColor = Color.White;
            newSeries.MarkerStyle = MarkerStyle.Circle;
            newSeries.MarkerSize = 0;
        }
        #endregion

        private async Task SetupPingAsync(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                reset = new CancellationTokenSource();
                await DoPingAsync(reset.Token);
                reset.Dispose();
                //needRestart = false;
            }
        }

        private async Task DoPingAsync(CancellationToken ct)
        {
            // load IP address from data serialization
            string IPTarget = Properties.Settings.Default.PingIPAddress;
            IPAddress address = null;
            try
            {
                address = IPAddress.Parse(IPTarget);
            }
            catch
            {
                SetText("Invalid");
            }

            Ping pingSender = new Ping();

            // attach event handler when completed
            pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);

            // Wait 5 seconds for a reply.
            int timeout = 5000;

            //using ctr mean the form close will wait until sendasync finishes its task (waiting for the timeout is too long)
            //using (CancellationTokenRegistration ctr = ct.Register(() => pingSender.SendAsyncCancel()))
            {
                while (/*!needRestart && */!ct.IsCancellationRequested)
                {
                    try
                    {
                        pingSender.SendAsync(address, timeout, waiter);
                    }
                    catch (Exception ex)
                    {
                        SetText("Error");
                        Console.WriteLine(ex.Message);
                        continue;
                    }
                    // waits on AutoResetEvent
                    // resume on AutoResetEvent.UserState.Set in PingCompletedCallback
                    waiter.WaitOne();

                    //wait 1 sec after ping completed (instead of bursting every ping finished)
                    await Task.Delay(1000);
                }
            }
        }

        private void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            if (/*!needRestart && */!reset.IsCancellationRequested)
            {
                PingReply reply = e.Reply;
                DisplayReply(reply);
            }

            // Let the main thread resume.
            ((AutoResetEvent)e.UserState).Set();
        }

        public void DisplayReply(PingReply reply)
        {
            if (reply == null)
                return;

            if (reply.Status == IPStatus.Success)
            {
                SetText(reply.RoundtripTime.ToString());
            }
            else
            {
                // return the error code to the display
                string statusCode = reply.Status.ToString();
                if (statusCode == "11050") statusCode = "NoNetSvc";
                SetText(statusCode);
            }
        }

        private void SetText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.PingLabel.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.PingLabel.Text = text;
                if (newSeries.Points.Count >= 10)
                {
                    // remove the oldest data and in case more than 1 happened
                    do
                    {
                        newSeries.Points.RemoveAt(0);
                    } while (newSeries.Points.Count >= 10);
                }
                try
                {
                    int pingTime = int.Parse(text);

                    newSeries.Points.AddXY(DateTime.Now, pingTime);

                    if (pingTime < 100)
                    {
                        this.PingLabel.ForeColor = color1st;
                        this.newSeries.Points[newSeries.Points.Count - 1].Color = color1st;
                        PingChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = color1st;
                        notifyIcon.Icon = Properties.Resources.logo_micro_green;
                        notifyIcon.Text = "Ping Test Result\nUnder 100ms";                        
                    }
                    else if (pingTime >= 100 && pingTime < 300)
                    {
                        this.PingLabel.ForeColor = color2nd;
                        this.newSeries.Points[newSeries.Points.Count - 1].Color = color2nd;
                        PingChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = color2nd;
                        notifyIcon.Icon = Properties.Resources.logo_micro_yellow;
                        notifyIcon.Text = "Ping Test Result\nBetween 100ms and 300ms";
                    }
                    else if (pingTime >= 300 && pingTime < 1000)
                    {
                        this.PingLabel.ForeColor = color3rd;
                        this.newSeries.Points[newSeries.Points.Count - 1].Color = color3rd;
                        PingChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = color3rd;
                        notifyIcon.Icon = Properties.Resources.logo_micro_orange;
                        notifyIcon.Text = "Ping Test Result\nBetween 300ms and 1000ms";
                    }
                    else
                    {
                        this.PingLabel.ForeColor = color4th;
                        this.newSeries.Points[newSeries.Points.Count - 1].Color = color4th;
                        PingChart.ChartAreas[0].AxisY.LabelStyle.ForeColor = color4th;
                        notifyIcon.Icon = Properties.Resources.logo_micro_red;
                        notifyIcon.Text = "Ping Test Result\nAbove 1000ms";
                    }

                    this.PingLabel.Text += " ms";

                    RedrawGraph();
                }
                // ping result is not number
                catch
                {
                    this.PingLabel.ForeColor = Color.Red;
                    // add a straight horizontal line from the last data
                    if (newSeries.Points.Count != 0)
                    {
                        newSeries.Points.AddXY(DateTime.Now, newSeries.Points[newSeries.Points.Count - 1].YValues[0]);
                    }
                    else
                    {
                        newSeries.Points.AddXY(DateTime.Now, 0);
                    }
                    this.newSeries.Points[newSeries.Points.Count - 1].Color = color5th;
                    notifyIcon.Icon = Properties.Resources.logo_micro_purple;
                    RedrawGraph();
                }
            }
        }

        // readjust the min-max x axis and y axis value of the chart
        private void RedrawGraph()
        {
            PingChart.ChartAreas[0].AxisX.Maximum = newSeries.Points[newSeries.Points.Count - 1].XValue;
            PingChart.ChartAreas[0].AxisX.Minimum = newSeries.Points[0].XValue;
            PingChart.ChartAreas[0].AxisY.Maximum = newSeries.Points.Max(y => y.YValues[0]);
            PingChart.ChartAreas[0].AxisY.Minimum = newSeries.Points.Min(y => y.YValues[0]);
            // if axis is less than 5, to avoid number crumble on axis invterval (because of without decimal) adds it with 5 
            if (PingChart.ChartAreas[0].AxisY.Maximum - PingChart.ChartAreas[0].AxisY.Minimum <= 5)
            {
                PingChart.ChartAreas[0].AxisY.Maximum = PingChart.ChartAreas[0].AxisY.Minimum + 5;
            }
            double axisInterval;
            //axis interval need to be more than 3 to avoid axis chart display on top max (which is not displayed at all)
            axisInterval = (PingChart.ChartAreas[0].AxisY.Maximum - PingChart.ChartAreas[0].AxisY.Minimum) / 3.2;
            PingChart.ChartAreas[0].AxisY.LabelStyle.Interval = axisInterval;
        }

        #region ToolStrip function
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Settings openFormSettings = Application.OpenForms["Settings"] as Settings;
            if (openFormSettings == null)
            {
                Settings FormSettings = new Settings(this);
                FormSettings.Show();
            }
            else
            {
                openFormSettings.Focus();
            }
        }

        private void makeClickableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIFunction.Win32Access win32Access = new UIFunction.Win32Access();
            win32Access.MakeClickable(this);
            makeClickableToolStripMenuItem.Visible = false;
            makeUnClickableToolStripMenuItem.Visible = true;
        }

        private void makeUnClickableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            QuestionWindow questionWindow = new QuestionWindow("Do you want to make the application unclickable?\n\nYou can make the application clickable again from the tray/notification icon", "This feature may confuse you");
            questionWindow.ShowDialog();
            if (questionWindow.DialogResult == DialogResult.Yes)
            {
                UIFunction.Win32Access win32Access = new UIFunction.Win32Access();
                win32Access.MakeUnClickable(this);
                makeUnClickableToolStripMenuItem.Visible = false;
                makeClickableToolStripMenuItem.Visible = true;
            }
            questionWindow.Dispose();
        }

        private void clickableToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            InitializeToolStripMenu();
        }

        #region Position Tool Strip Menu Function
        private void resetToCenterToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            UIFunction uiFunction = new UIFunction();
            uiFunction.MovePosition(this, UIFunction.UIPosition.Center);
        }

        private void toTopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIFunction uiFunction = new UIFunction();
            uiFunction.MovePosition(this, UIFunction.UIPosition.Top);
        }

        private void toTopRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIFunction uiFunction = new UIFunction();
            uiFunction.MovePosition(this, UIFunction.UIPosition.TopRight);
        }

        private void toRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIFunction uiFunction = new UIFunction();
            uiFunction.MovePosition(this, UIFunction.UIPosition.Right);
        }

        private void toBottomRightToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIFunction uiFunction = new UIFunction();
            uiFunction.MovePosition(this, UIFunction.UIPosition.BottomRight);
        }

        private void toBottomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIFunction uiFunction = new UIFunction();
            uiFunction.MovePosition(this, UIFunction.UIPosition.Bottom);
        }

        private void toBottomLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIFunction uiFunction = new UIFunction();
            uiFunction.MovePosition(this, UIFunction.UIPosition.BottomLeft);
        }

        private void toLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIFunction uiFunction = new UIFunction();
            uiFunction.MovePosition(this, UIFunction.UIPosition.Left);
        }

        private void toTopLeftToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UIFunction uiFunction = new UIFunction();
            uiFunction.MovePosition(this, UIFunction.UIPosition.TopLeft);
        }
        #endregion
        #endregion

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            Properties.Settings.Default.WPosX = this.Location.X;
            Properties.Settings.Default.WPosY = this.Location.Y;
            Properties.Settings.Default.Save();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            reset.Cancel();
            cts.Cancel();
        }

        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Activate();
            }
        }
    }
}
