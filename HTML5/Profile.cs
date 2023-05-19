using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace HTML5
{
    public partial class Profile : Form
    {
        private string email;
        public Profile(string email)
        {
            InitializeComponent();
            this.email = email;

            label1.Text = email;

            CenterForm();

            Account();

            ToolTip toolTip1 = new ToolTip();

            toolTip1.SetToolTip(pictureBox1, "Текст подсказки");
        }

        private void CenterForm()
        {
            int centerX = Screen.PrimaryScreen.WorkingArea.Width / 2;
            int centerY = Screen.PrimaryScreen.WorkingArea.Height / 2;

            int formX = this.Width / 2;
            int formY = this.Height / 2;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(centerX - formX, centerY - formY);
        }

        public void Account()
        {
            pictureBoxAccount.Image = Image.FromFile("..\\..\\Resources\\126200705.jpeg");
            int diameter = Math.Min(pictureBoxAccount.Width, pictureBoxAccount.Height);
            GraphicsPath circlePath = new GraphicsPath();
            circlePath.AddEllipse(
                pictureBoxAccount.Width / 2 - diameter / 2,
                pictureBoxAccount.Height / 2 - diameter / 2, diameter, diameter);
            pictureBoxAccount.Region = new Region(circlePath);

            pictureBoxAccount.BackColor = Color.FromArgb(255, 192, 192);
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(email);
            settings.ShowDialog();
        }
    }
}
