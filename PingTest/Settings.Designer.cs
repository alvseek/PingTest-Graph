namespace PingTest
{
    partial class Settings
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
            this.networkBox = new System.Windows.Forms.GroupBox();
            this.ipAddressComboBox = new System.Windows.Forms.ComboBox();
            this.ipLabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.point1 = new System.Windows.Forms.Label();
            this.point2 = new System.Windows.Forms.Label();
            this.point3 = new System.Windows.Forms.Label();
            this.textAddress1 = new System.Windows.Forms.TextBox();
            this.textAddress2 = new System.Windows.Forms.TextBox();
            this.textAddress3 = new System.Windows.Forms.TextBox();
            this.textAddress4 = new System.Windows.Forms.TextBox();
            this.appBox = new System.Windows.Forms.GroupBox();
            this.registryErrorLabel = new System.Windows.Forms.Label();
            this.clickableLabel = new System.Windows.Forms.Label();
            this.clickableChkBox = new System.Windows.Forms.CheckBox();
            this.alwaysOnTopLabel = new System.Windows.Forms.Label();
            this.alwaysOnTopChkBox = new System.Windows.Forms.CheckBox();
            this.startupChkBox = new System.Windows.Forms.CheckBox();
            this.runatStartupLabel = new System.Windows.Forms.Label();
            this.okButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.uiBox = new System.Windows.Forms.GroupBox();
            this.transparencyLabel = new System.Windows.Forms.Label();
            this.transparencyTrackBar = new System.Windows.Forms.TrackBar();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.licenseLabel = new System.Windows.Forms.Label();
            this.networkBox.SuspendLayout();
            this.panel1.SuspendLayout();
            this.appBox.SuspendLayout();
            this.uiBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transparencyTrackBar)).BeginInit();
            this.SuspendLayout();
            // 
            // networkBox
            // 
            this.networkBox.Controls.Add(this.ipAddressComboBox);
            this.networkBox.Controls.Add(this.ipLabel);
            this.networkBox.Controls.Add(this.panel1);
            this.networkBox.Location = new System.Drawing.Point(13, 67);
            this.networkBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.networkBox.Name = "networkBox";
            this.networkBox.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.networkBox.Size = new System.Drawing.Size(358, 95);
            this.networkBox.TabIndex = 0;
            this.networkBox.TabStop = false;
            this.networkBox.Text = "Network Settings";
            // 
            // ipAddressComboBox
            // 
            this.ipAddressComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ipAddressComboBox.FormattingEnabled = true;
            this.ipAddressComboBox.Location = new System.Drawing.Point(128, 25);
            this.ipAddressComboBox.Name = "ipAddressComboBox";
            this.ipAddressComboBox.Size = new System.Drawing.Size(216, 21);
            this.ipAddressComboBox.TabIndex = 7;
            this.ipAddressComboBox.SelectedValueChanged += new System.EventHandler(this.ipAddressComboBox_SelectedValueChanged);
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Location = new System.Drawing.Point(20, 29);
            this.ipLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(82, 13);
            this.ipLabel.TabIndex = 0;
            this.ipLabel.Text = "Ping IP Address";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.point1);
            this.panel1.Controls.Add(this.point2);
            this.panel1.Controls.Add(this.point3);
            this.panel1.Controls.Add(this.textAddress1);
            this.panel1.Controls.Add(this.textAddress2);
            this.panel1.Controls.Add(this.textAddress3);
            this.panel1.Controls.Add(this.textAddress4);
            this.panel1.Location = new System.Drawing.Point(128, 57);
            this.panel1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(216, 24);
            this.panel1.TabIndex = 6;
            this.panel1.Visible = false;
            // 
            // point1
            // 
            this.point1.AutoSize = true;
            this.point1.BackColor = System.Drawing.SystemColors.Window;
            this.point1.Location = new System.Drawing.Point(50, 3);
            this.point1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.point1.Name = "point1";
            this.point1.Size = new System.Drawing.Size(10, 13);
            this.point1.TabIndex = 5;
            this.point1.Text = ".";
            // 
            // point2
            // 
            this.point2.AutoSize = true;
            this.point2.BackColor = System.Drawing.SystemColors.Window;
            this.point2.Location = new System.Drawing.Point(100, 3);
            this.point2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.point2.Name = "point2";
            this.point2.Size = new System.Drawing.Size(10, 13);
            this.point2.TabIndex = 5;
            this.point2.Text = ".";
            // 
            // point3
            // 
            this.point3.AutoSize = true;
            this.point3.BackColor = System.Drawing.SystemColors.Window;
            this.point3.Location = new System.Drawing.Point(150, 3);
            this.point3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.point3.Name = "point3";
            this.point3.Size = new System.Drawing.Size(10, 13);
            this.point3.TabIndex = 5;
            this.point3.Text = ".";
            // 
            // textAddress1
            // 
            this.textAddress1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textAddress1.Location = new System.Drawing.Point(10, 3);
            this.textAddress1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textAddress1.MaxLength = 3;
            this.textAddress1.Name = "textAddress1";
            this.textAddress1.Size = new System.Drawing.Size(40, 13);
            this.textAddress1.TabIndex = 1;
            this.textAddress1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textAddress1.TextChanged += new System.EventHandler(this.textAddress1_TextChanged);
            this.textAddress1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAddress1_KeyDown);
            this.textAddress1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textAddress1_KeyPress);
            // 
            // textAddress2
            // 
            this.textAddress2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textAddress2.Location = new System.Drawing.Point(60, 3);
            this.textAddress2.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textAddress2.MaxLength = 3;
            this.textAddress2.Name = "textAddress2";
            this.textAddress2.Size = new System.Drawing.Size(40, 13);
            this.textAddress2.TabIndex = 2;
            this.textAddress2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textAddress2.TextChanged += new System.EventHandler(this.textAddress2_TextChanged);
            this.textAddress2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAddress1_KeyDown);
            this.textAddress2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textAddress1_KeyPress);
            // 
            // textAddress3
            // 
            this.textAddress3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textAddress3.Location = new System.Drawing.Point(110, 3);
            this.textAddress3.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textAddress3.MaxLength = 3;
            this.textAddress3.Name = "textAddress3";
            this.textAddress3.Size = new System.Drawing.Size(40, 13);
            this.textAddress3.TabIndex = 3;
            this.textAddress3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textAddress3.TextChanged += new System.EventHandler(this.textAddress3_TextChanged);
            this.textAddress3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAddress1_KeyDown);
            this.textAddress3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textAddress1_KeyPress);
            // 
            // textAddress4
            // 
            this.textAddress4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textAddress4.Location = new System.Drawing.Point(160, 3);
            this.textAddress4.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.textAddress4.MaxLength = 3;
            this.textAddress4.Name = "textAddress4";
            this.textAddress4.Size = new System.Drawing.Size(40, 13);
            this.textAddress4.TabIndex = 4;
            this.textAddress4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textAddress4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textAddress1_KeyDown);
            this.textAddress4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textAddress1_KeyPress);
            // 
            // appBox
            // 
            this.appBox.Controls.Add(this.registryErrorLabel);
            this.appBox.Controls.Add(this.clickableLabel);
            this.appBox.Controls.Add(this.clickableChkBox);
            this.appBox.Controls.Add(this.alwaysOnTopLabel);
            this.appBox.Controls.Add(this.alwaysOnTopChkBox);
            this.appBox.Controls.Add(this.startupChkBox);
            this.appBox.Controls.Add(this.runatStartupLabel);
            this.appBox.Location = new System.Drawing.Point(13, 168);
            this.appBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.appBox.Name = "appBox";
            this.appBox.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.appBox.Size = new System.Drawing.Size(358, 139);
            this.appBox.TabIndex = 1;
            this.appBox.TabStop = false;
            this.appBox.Text = "Application Settings";
            // 
            // registryErrorLabel
            // 
            this.registryErrorLabel.AutoSize = true;
            this.registryErrorLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.registryErrorLabel.ForeColor = System.Drawing.Color.Red;
            this.registryErrorLabel.Location = new System.Drawing.Point(187, 31);
            this.registryErrorLabel.Name = "registryErrorLabel";
            this.registryErrorLabel.Size = new System.Drawing.Size(117, 13);
            this.registryErrorLabel.TabIndex = 5;
            this.registryErrorLabel.Text = "* cannot read registry! *";
            this.registryErrorLabel.Visible = false;
            this.registryErrorLabel.MouseLeave += new System.EventHandler(this.registryErrorLabel_MouseLeave);
            this.registryErrorLabel.MouseHover += new System.EventHandler(this.registryErrorLabel_MouseHover);
            // 
            // clickableLabel
            // 
            this.clickableLabel.AutoSize = true;
            this.clickableLabel.Location = new System.Drawing.Point(20, 97);
            this.clickableLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.clickableLabel.Name = "clickableLabel";
            this.clickableLabel.Size = new System.Drawing.Size(50, 13);
            this.clickableLabel.TabIndex = 4;
            this.clickableLabel.Text = "Clickable";
            // 
            // clickableChkBox
            // 
            this.clickableChkBox.AutoSize = true;
            this.clickableChkBox.Location = new System.Drawing.Point(128, 97);
            this.clickableChkBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.clickableChkBox.Name = "clickableChkBox";
            this.clickableChkBox.Size = new System.Drawing.Size(15, 14);
            this.clickableChkBox.TabIndex = 3;
            this.clickableChkBox.UseVisualStyleBackColor = true;
            this.clickableChkBox.CheckedChanged += new System.EventHandler(this.clickableChkBox_CheckedChanged);
            // 
            // alwaysOnTopLabel
            // 
            this.alwaysOnTopLabel.AutoSize = true;
            this.alwaysOnTopLabel.Location = new System.Drawing.Point(20, 63);
            this.alwaysOnTopLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.alwaysOnTopLabel.Name = "alwaysOnTopLabel";
            this.alwaysOnTopLabel.Size = new System.Drawing.Size(73, 13);
            this.alwaysOnTopLabel.TabIndex = 2;
            this.alwaysOnTopLabel.Text = "Always on top";
            // 
            // alwaysOnTopChkBox
            // 
            this.alwaysOnTopChkBox.AutoSize = true;
            this.alwaysOnTopChkBox.Location = new System.Drawing.Point(128, 63);
            this.alwaysOnTopChkBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.alwaysOnTopChkBox.Name = "alwaysOnTopChkBox";
            this.alwaysOnTopChkBox.Size = new System.Drawing.Size(15, 14);
            this.alwaysOnTopChkBox.TabIndex = 1;
            this.alwaysOnTopChkBox.UseVisualStyleBackColor = true;
            // 
            // startupChkBox
            // 
            this.startupChkBox.AutoSize = true;
            this.startupChkBox.Location = new System.Drawing.Point(128, 31);
            this.startupChkBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.startupChkBox.Name = "startupChkBox";
            this.startupChkBox.Size = new System.Drawing.Size(15, 14);
            this.startupChkBox.TabIndex = 1;
            this.startupChkBox.UseVisualStyleBackColor = true;
            // 
            // runatStartupLabel
            // 
            this.runatStartupLabel.AutoSize = true;
            this.runatStartupLabel.Location = new System.Drawing.Point(20, 31);
            this.runatStartupLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.runatStartupLabel.Name = "runatStartupLabel";
            this.runatStartupLabel.Size = new System.Drawing.Size(74, 13);
            this.runatStartupLabel.TabIndex = 0;
            this.runatStartupLabel.Text = "Run at startup";
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(154, 433);
            this.okButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(74, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(281, 433);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 23);
            this.button1.TabIndex = 4;
            this.button1.Text = "Show Thread";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // uiBox
            // 
            this.uiBox.Controls.Add(this.transparencyLabel);
            this.uiBox.Controls.Add(this.transparencyTrackBar);
            this.uiBox.Controls.Add(this.label2);
            this.uiBox.Location = new System.Drawing.Point(13, 313);
            this.uiBox.Name = "uiBox";
            this.uiBox.Size = new System.Drawing.Size(358, 108);
            this.uiBox.TabIndex = 5;
            this.uiBox.TabStop = false;
            this.uiBox.Text = "UI Settings";
            // 
            // transparencyLabel
            // 
            this.transparencyLabel.AutoSize = true;
            this.transparencyLabel.Location = new System.Drawing.Point(125, 34);
            this.transparencyLabel.Name = "transparencyLabel";
            this.transparencyLabel.Size = new System.Drawing.Size(68, 13);
            this.transparencyLabel.TabIndex = 8;
            this.transparencyLabel.Text = "50% (default)";
            // 
            // transparencyTrackBar
            // 
            this.transparencyTrackBar.LargeChange = 10;
            this.transparencyTrackBar.Location = new System.Drawing.Point(117, 56);
            this.transparencyTrackBar.Maximum = 100;
            this.transparencyTrackBar.Name = "transparencyTrackBar";
            this.transparencyTrackBar.Size = new System.Drawing.Size(235, 45);
            this.transparencyTrackBar.SmallChange = 5;
            this.transparencyTrackBar.TabIndex = 7;
            this.transparencyTrackBar.TickFrequency = 5;
            this.transparencyTrackBar.Value = 50;
            this.transparencyTrackBar.ValueChanged += new System.EventHandler(this.transparencyTrackBar_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(20, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Transparency";
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.SystemColors.Control;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(2, 17);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(382, 20);
            this.textBox1.TabIndex = 7;
            this.textBox1.Text = "PingTest Graph © 2018 www.indonesiamadjoe.com";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // licenseLabel
            // 
            this.licenseLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.licenseLabel.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.licenseLabel.Location = new System.Drawing.Point(83, 41);
            this.licenseLabel.Name = "licenseLabel";
            this.licenseLabel.Size = new System.Drawing.Size(220, 19);
            this.licenseLabel.TabIndex = 6;
            this.licenseLabel.Text = "Free License";
            this.licenseLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.licenseLabel.Click += new System.EventHandler(this.licenseLabel_Click);
            // 
            // Settings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(383, 467);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.licenseLabel);
            this.Controls.Add(this.uiBox);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.appBox);
            this.Controls.Add(this.networkBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Settings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Settings_FormClosing);
            this.networkBox.ResumeLayout(false);
            this.networkBox.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.appBox.ResumeLayout(false);
            this.appBox.PerformLayout();
            this.uiBox.ResumeLayout(false);
            this.uiBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.transparencyTrackBar)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox networkBox;
        private System.Windows.Forms.Label ipLabel;
        private System.Windows.Forms.GroupBox appBox;
        private System.Windows.Forms.CheckBox startupChkBox;
        private System.Windows.Forms.Label runatStartupLabel;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.TextBox textAddress2;
        private System.Windows.Forms.TextBox textAddress1;
        private System.Windows.Forms.TextBox textAddress4;
        private System.Windows.Forms.TextBox textAddress3;
        private System.Windows.Forms.Label point3;
        private System.Windows.Forms.Label point2;
        private System.Windows.Forms.Label point1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label alwaysOnTopLabel;
        private System.Windows.Forms.CheckBox alwaysOnTopChkBox;
        private System.Windows.Forms.ComboBox ipAddressComboBox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label clickableLabel;
        private System.Windows.Forms.CheckBox clickableChkBox;
        private System.Windows.Forms.GroupBox uiBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar transparencyTrackBar;
        private System.Windows.Forms.Label transparencyLabel;
        private System.Windows.Forms.TextBox textBox1;
        public System.Windows.Forms.Label licenseLabel;
        private System.Windows.Forms.Label registryErrorLabel;
    }
}