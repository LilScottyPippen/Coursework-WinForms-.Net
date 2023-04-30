using DotNetEnv;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

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
            if (Env.GetString("GOOGLE_AUTH") == "false")
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
            }
            if (Env.GetString("GOOGLE_AUTH") == "true")
            {
                string[] scopes = { GmailService.Scope.GmailReadonly };
                var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    new ClientSecrets
                    {
                        ClientId = Env.GetString("CLIENT_ID"),
                        ClientSecret = Env.GetString("CLIENT_SECRET")
                    },
                    scopes, "user", CancellationToken.None).Result;

                credentials.RevokeTokenAsync(CancellationToken.None);
                credentials = null;

                var lines = File.ReadAllLines("..\\..\\.env");

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("EMAIL="))
                        lines[i] = "EMAIL=";
                    if (lines[i].StartsWith("GOOGLE_AUTH"))
                        lines[i] = "GOOGLE_AUTH=false";
                    if (lines[i].StartsWith("TOKEN="))
                        lines[i] = "TOKEN=";
                }
                File.WriteAllLines("..\\..\\.env", lines);
            }

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
