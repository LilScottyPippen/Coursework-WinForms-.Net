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
        private string email;
        public Settings(string email)
        {
            InitializeComponent();
            this.email = email;

            CenterForm();

            labelEmail.Text += email;
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

        private void buttonLogout_Click(object sender, EventArgs e)
        {
            var lines = File.ReadAllLines("..\\..\\.env");

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("EMAIL="))
                    lines[i] = "EMAIL=";
                else if (lines[i].StartsWith("PASSWORD="))
                    lines[i] = "PASSWORD=";
            }

            File.WriteAllLines("..\\..\\.env", lines);

            Login login = new Login();
            List<Form> openForms = new List<Form>(Application.OpenForms.Cast<Form>());
            foreach (Form form in openForms)
            {
                if (form != login)
                {
                    form.Hide();
                }
            }
            login.Show();
        }
    }
}
