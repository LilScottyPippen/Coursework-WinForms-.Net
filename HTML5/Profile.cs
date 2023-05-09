using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

            Account();
        }

        public void Account()
        {
            pictureBoxAccount.Image = Image.FromFile("D:\\C# Курсач\\Приложение\\HTML5\\HTML5\\Resources\\126200705.jpeg");
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
