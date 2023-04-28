using DotNetEnv;
using Npgsql;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util;
using System.Threading;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;

namespace HTML5
{
    public partial class Login : Form
    {
        private AuthManager authManager;
        private bool isAuth;
        public Login()
        {
            InitializeComponent();

            CenterForm();

            ConnectDB();

            LoadAccount();

            this.FormClosed += (s, args) => Application.Exit();
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

        private NpgsqlConnection ConnectDB()
        {
            Env.Load("..\\..\\.env");
            string pass = Environment.GetEnvironmentVariable("DB_PASS");

            NpgsqlConnection conn = new NpgsqlConnection($"Server=localhost;Database=html5;User Id=postgres;Password={pass}");

            conn.Open();
            return conn;
        }

        public void LoadAccount()
        {
            string email = DotNetEnv.Env.GetString("EMAIL");
            string password = DotNetEnv.Env.GetString("PASSWORD");

            using (NpgsqlConnection conn = ConnectDB())
            {
                NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM users WHERE email = '{email}' AND password = '{password}'", conn);
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read()){
                    Index index = new Index(email, password);
                    index.ShowDialog();
                    this.Hide();
                }
                reader.Close();
            }
        }

        private void labelLogin_Click(object sender, EventArgs e)
        {
            string email = textBoxEmail.Text;
            string password = textBoxPassword.Text;

            AuthManager authManager = new AuthManager();
            authManager.Email = email;
            authManager.Password = password;

            if (authManager.Login())
            {
                Index index = new Index(email, password);
                index.Show();
                this.Hide();
            }

            if(checkBoxRemember.Checked == true)
            {
                var lines = File.ReadAllLines("..\\..\\.env");

                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("EMAIL="))
                        lines[i] = $"EMAIL={email}";
                    else if (lines[i].StartsWith("PASSWORD="))
                        lines[i] = $"PASSWORD={password}";
                }

                File.WriteAllLines("..\\..\\.env", lines);
            }
        }

        private void textBoxPassword_TextChanged(object sender, EventArgs e)
        {
            textBoxPassword.UseSystemPasswordChar = true;
        }

        private void pictureBoxGoogle_Click(object sender, EventArgs e)
        {
            string clientId = Env.GetString("CLIENT_ID");
            string clientSecret = Env.GetString("CLIENT_SECRET");

            string[] scopes = { GmailService.Scope.GmailReadonly };
            var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                },
                scopes, "user", CancellationToken.None).Result;

            if (credentials.Token.IsExpired(SystemClock.Default))
                credentials.RefreshTokenAsync(CancellationToken.None).Wait();

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials
            });
            var userInfoRequest = service.Users.GetProfile("me");
            var userInfo = userInfoRequest.Execute();
            string userEmail = userInfo.EmailAddress;

            MessageBox.Show(userEmail);
            var profile = service.Users.GetProfile(userEmail).Execute();
            MessageBox.Show(profile.MessagesTotal.ToString());
        }

        private void labelRegistration_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            this.Hide();
            registration.Show();
        }

        private void labelForgetPas_Click(object sender, EventArgs e)
        {
            ForgetPass forgetPass = new ForgetPass();
            this.Hide();
            forgetPass.Show();
        }
    }
}
