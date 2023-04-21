using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace HTML5
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();

            CenterForm();

            labelEmail.Text += Environment.GetEnvironmentVariable("EMAIL");
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

        private void button1_Click(object sender, EventArgs e)
        {
            /*var lines = File.ReadAllLines("..\\..\\.env");

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("EMAIL="))
                    lines[i] = "EMAIL=";
                else if (lines[i].StartsWith("PASSWORD="))
                    lines[i] = "PASSWORD=";
            }

            File.WriteAllLines("..\\..\\.env", lines);

            Login login = new Login();
            login.ShowDialog();*/
        }
    }
}
