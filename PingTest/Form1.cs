using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;

namespace PingTest
{
    public partial class Form1 : Form
    {
        private AutoResetEvent waiter = new AutoResetEvent(false);

        private const int WM_NCHITTEST = 0x84;
        private const int HT_CLIENT = 0x1;
        private const int HT_CAPTION = 0x2;
        private const int WM_NCRBUTTONDOWN = 0xa4;
        Series newSeries = new Series();

        static string IPTarget = "8.8.8.8";
        Ping pingSender = new Ping();
        IPAddress address = IPAddress.Parse(IPTarget);

        delegate void SetTextCallback(string text);
        delegate void SetChartCallback();

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
                m.Result = (IntPtr)(HT_CAPTION);
            
            if (m.Msg == WM_NCRBUTTONDOWN)
            {
                var pos = new Point(m.LParam.ToInt32());
                OnTitlebarClick(pos);
            }           
        }

        protected void OnTitlebarClick(Point pos)
        {
            contextMenuStrip1.Show(pos);
        }

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = FormBorderStyle.None;
            MakeSeries();
            ThreadPool.QueueUserWorkItem(o => DoPingAsync(IPTarget));
        }

        private void MakeSeries()
        {
            Console.WriteLine("we get in?");
            
            PingChart.Series.Add(newSeries);
            newSeries.ChartArea = "ChartArea1";
            newSeries.ChartType = SeriesChartType.Line;
            newSeries.Color = Color.White;
            newSeries.MarkerColor = Color.White;
            newSeries.MarkerStyle = MarkerStyle.Circle;
            newSeries.MarkerSize = 1;
        }

        private void DoPingAsync(string args)
        {

            if (args.Length == 0)
                throw new ArgumentException("Ping needs a host or IP Address.");

            string who = args;



            Ping pingSender = new Ping();

            // When the PingCompleted event is raised,
            // the PingCompletedCallback method is called.
            pingSender.PingCompleted += new PingCompletedEventHandler(PingCompletedCallback);

            /*
            // Create a buffer of 1 bytes of data to be transmitted.
            string data = "1";
            byte[] buffer = Encoding.ASCII.GetBytes(data);
            */
            // Wait 5 seconds for a reply.
            int timeout = 5000;

            // Set options for transmission:
            // The data can go through 64 gateways or routers
            // before it is destroyed, and the data packet
            // cannot be fragmented.
            /*
            PingOptions options = new PingOptions(64, true);

            Console.WriteLine("Time to live: {0}", options.Ttl);
            Console.WriteLine("Don't fragment: {0}", options.DontFragment);
            */
            // Send the ping asynchronously.
            // Use the waiter as the user token.
            // When the callback completes, it can wake up this thread.
            //Trace.WriteLine("Ping example started.");

            SetChart();

            do
            {
                Thread.Sleep(1000);
                pingSender.SendAsync(who, timeout, waiter);
                //Console.WriteLine("waits on AutoResetEvent");
                waiter.WaitOne();
            } while (true);
            //Console.WriteLine("Ping example completed.");
        }

        private void PingCompletedCallback(object sender, PingCompletedEventArgs e)
        {
            // If the operation was canceled, display a message to the user.
            if (e.Cancelled)
            {
                Console.WriteLine("Ping canceled.");

                // Let the main thread resume. 
                // UserToken is the AutoResetEvent object that the main thread 
                // is waiting for.
                ((AutoResetEvent)e.UserState).Set();
            }

            // If an error occurred, display the exception to the user.
            if (e.Error != null)
            {
                Console.WriteLine("Ping failed:");
                Console.WriteLine(e.Error.ToString());

                // Let the main thread resume. 
                ((AutoResetEvent)e.UserState).Set();
            }

            PingReply reply = e.Reply;

            DisplayReply(reply);

            // Let the main thread resume.
            ((AutoResetEvent)e.UserState).Set();
        }

        public void DisplayReply(PingReply reply)
        {
            if (reply == null)
                return;

            //Console.WriteLine("ping status: {0}", reply.Status.ToString());
            if (reply.Status == IPStatus.Success)
            {
                SetText(reply.RoundtripTime.ToString());
            }
            else
            {
                string statusCode = reply.Status.ToString();
                if (statusCode == "11050") statusCode = "Error";
                SetText(statusCode);
            }
        }

        private void SetChart()
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.PingChart.InvokeRequired)
            {
                SetChartCallback d = new SetChartCallback(SetChart);
                this.Invoke(d, new object[] {});
            }
            else
            {
                this.PingChart.Visible = true;
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
                    newSeries.Points.RemoveAt(0);
                }
                try
                {
                    int pingTime = int.Parse(text);

                    newSeries.Points.AddXY(DateTime.Now, pingTime);

                    if (pingTime <= 300)
                    {
                        this.PingLabel.ForeColor = Color.White;
                        //Console.WriteLine("Warna putih " + PingLabel.ForeColor);
                        this.newSeries.Points[newSeries.Points.Count - 1].Color = Color.White;
                    }
                    else if (pingTime >= 300 && pingTime < 1000)
                    {
                        this.PingLabel.ForeColor = Color.Orange;
                        //Console.WriteLine("Warna oren " + PingLabel.ForeColor);
                        this.newSeries.Points[newSeries.Points.Count - 1].Color = Color.Orange;
                    }
                    else
                    {
                        this.PingLabel.ForeColor = Color.Red;
                        //Console.WriteLine("Warna mewah " + PingLabel.ForeColor);
                        this.newSeries.Points[newSeries.Points.Count - 1].Color = Color.Red;
                    }

                    this.PingLabel.Text += " ms";
       
                    PingChart.ChartAreas[0].AxisX.Maximum = newSeries.Points[newSeries.Points.Count - 1].XValue;
                    PingChart.ChartAreas[0].AxisX.Minimum = newSeries.Points[0].XValue;
                    PingChart.ChartAreas[0].AxisY.Maximum = newSeries.Points.Max(y => y.YValues[0]);
                    PingChart.ChartAreas[0].AxisY.Minimum = newSeries.Points.Min(y => y.YValues[0]);
                    double axisInterval;
                    if (PingChart.ChartAreas[0].AxisY.Maximum - PingChart.ChartAreas[0].AxisY.Minimum <= 3)
                    {
                        PingChart.ChartAreas[0].AxisY.Maximum += 3;
                    }
                    axisInterval = (PingChart.ChartAreas[0].AxisY.Maximum - PingChart.ChartAreas[0].AxisY.Minimum) / 3.3;
                    PingChart.ChartAreas[0].AxisY.LabelStyle.Interval = axisInterval;
                }
                catch
                {
                    this.PingLabel.ForeColor = Color.Red;
                    if (newSeries.Points.Count != 0)
                    {
                        newSeries.Points.AddXY(DateTime.Now, newSeries.Points[newSeries.Points.Count - 1].YValues[0]);
                    }
                    else newSeries.Points.AddXY(DateTime.Now, 0);
                    this.newSeries.Points[newSeries.Points.Count - 1].Color = Color.Transparent;
                }
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
