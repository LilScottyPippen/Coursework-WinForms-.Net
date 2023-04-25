﻿using DotNetEnv;
using Npgsql;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

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

        }

        private void label2_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            this.Hide();
            registration.Show();
        }
    }
}
