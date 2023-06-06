using DotNetEnv;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Npgsql;
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

            ConnectDB();
            CenterForm();
            LoadAvatar();

            labelEmail.Text += email;
        }

        private NpgsqlConnection ConnectDB()
        {
            DotNetEnv.Env.Load("..\\..\\.env");
            string pass = Environment.GetEnvironmentVariable("DB_PASS");

            NpgsqlConnection conn = new NpgsqlConnection($"Server=localhost;Database=html5;User Id=postgres;Password={pass}");

            conn.Open();
            return conn;
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

        private void UploadAvatar_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Изображения (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string imagePath = openFileDialog.FileName;

                var lines = File.ReadAllLines("..\\..\\.env");
                string avatarPath = imagePath;
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("AVATAR="))
                        lines[i] = $"AVATAR={avatarPath}";
                }

                File.WriteAllLines("..\\..\\.env", lines);

                pictureBoxAccount.Image = Image.FromFile(imagePath);

                NpgsqlConnection conn = ConnectDB();
                NpgsqlCommand userAvatar = new NpgsqlCommand("UPDATE users SET avatar = @imagePath WHERE email = @email", conn);
                userAvatar.Parameters.AddWithValue("@imagePath", imagePath);
                userAvatar.Parameters.AddWithValue("@email", email);
                userAvatar.ExecuteNonQuery();

            }
        }

        private void LoadAvatar()
        {
            NpgsqlConnection conn = ConnectDB();
            NpgsqlCommand userAvatar = new NpgsqlCommand($"SELECT avatar FROM users WHERE email = '{email}'", conn);
            NpgsqlDataReader reader = userAvatar.ExecuteReader();
            if (reader.Read())
            {
                try
                {
                    string avatarPath = reader.GetString(0);
                    pictureBoxAccount.Image = Image.FromFile(avatarPath);
                }
                catch{
                    pictureBoxAccount.Image = Image.FromFile("..\\..\\Resources\\Frame.png");
                }
            }
        }
    }
}
