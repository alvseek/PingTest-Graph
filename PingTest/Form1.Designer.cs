namespace PingTest
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
                this.cts.Dispose();
                this.reset.Dispose();
                this.newSeries.Dispose();
                this.waiter.Dispose();
                this.notifyIcon.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.PingLabel = new System.Windows.Forms.Label();
            this.PingChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.positionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetToCenterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toTopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toBottomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toTopRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toBottomRightToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toBottomLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toTopLeftToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PingChart)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // PingLabel
            // 
            this.PingLabel.BackColor = System.Drawing.Color.Transparent;
            this.PingLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.PingLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PingLabel.ForeColor = System.Drawing.Color.White;
            this.PingLabel.Location = new System.Drawing.Point(0, 44);
            this.PingLabel.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.PingLabel.Name = "PingLabel";
            this.PingLabel.Padding = new System.Windows.Forms.Padding(2, 0, 0, 3);
            this.PingLabel.Size = new System.Drawing.Size(63, 17);
            this.PingLabel.TabIndex = 0;
            this.PingLabel.Text = "-";
            this.PingLabel.TextAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.PingLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.PingLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.PingLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            // 
            // PingChart
            // 
            this.PingChart.BackColor = System.Drawing.Color.Transparent;
            this.PingChart.BorderlineColor = System.Drawing.Color.Transparent;
            this.PingChart.BorderlineWidth = 0;
            this.PingChart.BorderSkin.BorderWidth = 0;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.IsMarginVisible = false;
            chartArea1.AxisX.LabelStyle.Enabled = false;
            chartArea1.AxisX.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisX.LabelStyle.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            chartArea1.AxisX.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.MajorGrid.Enabled = false;
            chartArea1.AxisX.MajorTickMark.Enabled = false;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Transparent;
            chartArea1.AxisX2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.AxisX2.TitleForeColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.VariableCount;
            chartArea1.AxisY.IsLabelAutoFit = false;
            chartArea1.AxisY.IsMarginVisible = false;
            chartArea1.AxisY.LabelAutoFitStyle = ((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles)(((System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.DecreaseFont | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.StaggeredLabels) 
            | System.Windows.Forms.DataVisualization.Charting.LabelAutoFitStyles.WordWrap)));
            chartArea1.AxisY.LabelStyle.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LabelStyle.Format = "D";
            chartArea1.AxisY.LabelStyle.Interval = 0D;
            chartArea1.AxisY.LineColor = System.Drawing.Color.Transparent;
            chartArea1.AxisY.MajorGrid.Enabled = false;
            chartArea1.AxisY.MajorTickMark.Enabled = false;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.Silver;
            chartArea1.AxisY.Minimum = 0D;
            chartArea1.AxisY2.Enabled = System.Windows.Forms.DataVisualization.Charting.AxisEnabled.False;
            chartArea1.BackColor = System.Drawing.Color.Transparent;
            chartArea1.BackImageTransparentColor = System.Drawing.Color.Transparent;
            chartArea1.BackSecondaryColor = System.Drawing.Color.Transparent;
            chartArea1.BorderWidth = 0;
            chartArea1.InnerPlotPosition.Auto = false;
            chartArea1.InnerPlotPosition.Height = 52F;
            chartArea1.InnerPlotPosition.Width = 65F;
            chartArea1.InnerPlotPosition.X = 27F;
            chartArea1.InnerPlotPosition.Y = 13F;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 100F;
            chartArea1.Position.Width = 100F;
            this.PingChart.ChartAreas.Add(chartArea1);
            this.PingChart.Enabled = false;
            this.PingChart.Location = new System.Drawing.Point(0, 0);
            this.PingChart.Margin = new System.Windows.Forms.Padding(0);
            this.PingChart.Name = "PingChart";
            this.PingChart.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Grayscale;
            this.PingChart.Size = new System.Drawing.Size(62, 61);
            this.PingChart.SuppressExceptions = true;
            this.PingChart.TabIndex = 1;
            this.PingChart.Text = "chart1";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem,
            this.toolStripSeparator1,
            this.positionToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(118, 82);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(114, 6);
            // 
            // positionToolStripMenuItem
            // 
            this.positionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.resetToCenterToolStripMenuItem,
            this.toolStripSeparator2,
            this.toTopToolStripMenuItem,
            this.toRightToolStripMenuItem,
            this.toBottomToolStripMenuItem,
            this.toLeftToolStripMenuItem,
            this.toolStripSeparator3,
            this.toTopRightToolStripMenuItem,
            this.toBottomRightToolStripMenuItem,
            this.toBottomLeftToolStripMenuItem,
            this.toTopLeftToolStripMenuItem});
            this.positionToolStripMenuItem.Name = "positionToolStripMenuItem";
            this.positionToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.positionToolStripMenuItem.Text = "Position";
            // 
            // resetToCenterToolStripMenuItem
            // 
            this.resetToCenterToolStripMenuItem.Name = "resetToCenterToolStripMenuItem";
            this.resetToCenterToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.resetToCenterToolStripMenuItem.Text = "To Center";
            this.resetToCenterToolStripMenuItem.Click += new System.EventHandler(this.resetToCenterToolStripMenuItem_Click_1);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(160, 6);
            // 
            // toTopToolStripMenuItem
            // 
            this.toTopToolStripMenuItem.Name = "toTopToolStripMenuItem";
            this.toTopToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.toTopToolStripMenuItem.Text = "To Top";
            this.toTopToolStripMenuItem.Click += new System.EventHandler(this.toTopToolStripMenuItem_Click);
            // 
            // toRightToolStripMenuItem
            // 
            this.toRightToolStripMenuItem.Name = "toRightToolStripMenuItem";
            this.toRightToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.toRightToolStripMenuItem.Text = "To Right";
            this.toRightToolStripMenuItem.Click += new System.EventHandler(this.toRightToolStripMenuItem_Click);
            // 
            // toBottomToolStripMenuItem
            // 
            this.toBottomToolStripMenuItem.Name = "toBottomToolStripMenuItem";
            this.toBottomToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.toBottomToolStripMenuItem.Text = "To Bottom";
            this.toBottomToolStripMenuItem.Click += new System.EventHandler(this.toBottomToolStripMenuItem_Click);
            // 
            // toLeftToolStripMenuItem
            // 
            this.toLeftToolStripMenuItem.Name = "toLeftToolStripMenuItem";
            this.toLeftToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.toLeftToolStripMenuItem.Text = "To Left";
            this.toLeftToolStripMenuItem.Click += new System.EventHandler(this.toLeftToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(160, 6);
            // 
            // toTopRightToolStripMenuItem
            // 
            this.toTopRightToolStripMenuItem.Name = "toTopRightToolStripMenuItem";
            this.toTopRightToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.toTopRightToolStripMenuItem.Text = "To Top-Right";
            this.toTopRightToolStripMenuItem.Click += new System.EventHandler(this.toTopRightToolStripMenuItem_Click);
            // 
            // toBottomRightToolStripMenuItem
            // 
            this.toBottomRightToolStripMenuItem.Name = "toBottomRightToolStripMenuItem";
            this.toBottomRightToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.toBottomRightToolStripMenuItem.Text = "To Bottom-Right";
            this.toBottomRightToolStripMenuItem.Click += new System.EventHandler(this.toBottomRightToolStripMenuItem_Click);
            // 
            // toBottomLeftToolStripMenuItem
            // 
            this.toBottomLeftToolStripMenuItem.Name = "toBottomLeftToolStripMenuItem";
            this.toBottomLeftToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.toBottomLeftToolStripMenuItem.Text = "To Bottom-Left";
            this.toBottomLeftToolStripMenuItem.Click += new System.EventHandler(this.toBottomLeftToolStripMenuItem_Click);
            // 
            // toTopLeftToolStripMenuItem
            // 
            this.toTopLeftToolStripMenuItem.Name = "toTopLeftToolStripMenuItem";
            this.toTopLeftToolStripMenuItem.Size = new System.Drawing.Size(163, 22);
            this.toTopLeftToolStripMenuItem.Text = "To Top-Left";
            this.toTopLeftToolStripMenuItem.Click += new System.EventHandler(this.toTopLeftToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(114, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "Ping Test";
            this.notifyIcon.Visible = true;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(63, 61);
            this.ContextMenuStrip = this.contextMenuStrip1;
            this.ControlBox = false;
            this.Controls.Add(this.PingLabel);
            this.Controls.Add(this.PingChart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(1, 1);
            this.Name = "Form1";
            this.Opacity = 0.5D;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "PingTest";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Form1_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.PingChart)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label PingLabel;
        private System.Windows.Forms.DataVisualization.Charting.Chart PingChart;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem positionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToCenterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toTopToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toTopRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toBottomRightToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toBottomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toBottomLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toTopLeftToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

