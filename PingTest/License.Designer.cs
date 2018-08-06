namespace PingTest
{
    partial class License
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(License));
            this.label1 = new System.Windows.Forms.Label();
            this.licenseTxtBox = new System.Windows.Forms.TextBox();
            this.okButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.donationLink = new System.Windows.Forms.LinkLabel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.removeLicenseButton = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(16, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(260, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Enter your license code";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // licenseTxtBox
            // 
            this.licenseTxtBox.Location = new System.Drawing.Point(16, 31);
            this.licenseTxtBox.Name = "licenseTxtBox";
            this.licenseTxtBox.Size = new System.Drawing.Size(260, 20);
            this.licenseTxtBox.TabIndex = 1;
            // 
            // okButton
            // 
            this.okButton.Location = new System.Drawing.Point(122, 147);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(75, 23);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "OK";
            this.okButton.UseVisualStyleBackColor = true;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(16, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(260, 23);
            this.label2.TabIndex = 3;
            this.label2.Text = "You can get your license code by donating from";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // donationLink
            // 
            this.donationLink.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.donationLink.Location = new System.Drawing.Point(16, 83);
            this.donationLink.Name = "donationLink";
            this.donationLink.Size = new System.Drawing.Size(260, 16);
            this.donationLink.TabIndex = 4;
            this.donationLink.TabStop = true;
            this.donationLink.Text = "donation page";
            this.donationLink.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.donationLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.licenseTxtBox);
            this.panel1.Controls.Add(this.donationLink);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(290, 121);
            this.panel1.TabIndex = 5;
            // 
            // removeLicenseButton
            // 
            this.removeLicenseButton.Location = new System.Drawing.Point(227, 147);
            this.removeLicenseButton.Name = "removeLicenseButton";
            this.removeLicenseButton.Size = new System.Drawing.Size(75, 23);
            this.removeLicenseButton.TabIndex = 5;
            this.removeLicenseButton.Text = "Remove License";
            this.removeLicenseButton.UseVisualStyleBackColor = true;
            this.removeLicenseButton.Visible = false;
            this.removeLicenseButton.Click += new System.EventHandler(this.removeLicenseButton_Click);
            // 
            // License
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(314, 185);
            this.Controls.Add(this.removeLicenseButton);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.okButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "License";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "License";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox licenseTxtBox;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel donationLink;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button removeLicenseButton;
    }
}