using System;
using System.Drawing;
using System.Windows.Forms;

namespace PingTest
{
    public partial class QuestionWindow : Form
    {
        public QuestionWindow(string contentString, string titleString)
        {
            InitializeComponent();
            contentLabel.Text = contentString;
            this.Text = titleString;
            pictureBox.Image = SystemIcons.Question.ToBitmap();
        }

        private void yesButton_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
