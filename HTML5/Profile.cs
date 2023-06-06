using DotNetEnv;
using Npgsql;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.IO;
using Google.Apis.Gmail.v1.Data;

namespace HTML5
{
    public partial class Profile : Form
    {
        private string email;
        private int user_id;
        public Profile(string email, int user_id)
        {
            InitializeComponent();
            this.email = email;
            this.user_id = user_id;

            label1.Text = email;

            CenterForm();

            ConnectDB();

            Account();

            TotalProgress();

            Statistics();

            this.user_id = user_id;
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
            DotNetEnv.Env.Load("..\\..\\.env");
            string pass = Environment.GetEnvironmentVariable("DB_PASS");

            NpgsqlConnection conn = new NpgsqlConnection($"Server=localhost;Database=html5;User Id=postgres;Password={pass}");

            conn.Open();
            return conn;
        }

        public void Account()
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
                catch
                {
                    pictureBoxAccount.Image = Image.FromFile("..\\..\\Resources\\Frame.png");
                }
            }

            int diameter = Math.Min(pictureBoxAccount.Width, pictureBoxAccount.Height);
            GraphicsPath circlePath = new GraphicsPath();
            circlePath.AddEllipse(
                pictureBoxAccount.Width / 2 - diameter / 2,
                pictureBoxAccount.Height / 2 - diameter / 2, diameter, diameter);
            pictureBoxAccount.Region = new Region(circlePath);

            pictureBoxAccount.BackColor = Color.FromArgb(255, 192, 192);
        }

        public void TotalProgress()
        {
            int totalProgress = 0;
            int sumLessons = 0;

            NpgsqlConnection conn = ConnectDB();
            NpgsqlCommand sumProgress = new NpgsqlCommand($"SELECT SUM(progress) as total_progress, COUNT(*) as sum_lessons FROM lesson_progress lp WHERE lp.user_id = {user_id}", conn);
            NpgsqlDataReader reader = sumProgress.ExecuteReader();

            if (reader.Read())
            {
                totalProgress = reader.GetInt32(0);
                sumLessons = reader.GetInt32(1);
            }

            circularProgressBar1.Value = (int)Math.Round((double)totalProgress / (sumLessons * 100) * 100);
        }

        public void Statistics()
        {
            NpgsqlConnection conn = ConnectDB();
            NpgsqlCommand statistics1 = new NpgsqlCommand($"SELECT COUNT(*) FROM lesson_progress WHERE user_id = {user_id} AND passed_the_test = true;", conn);
            NpgsqlDataReader reader = statistics1.ExecuteReader();
            if (reader.Read())
            {
                int count = reader.GetInt32(0);
                labelEndCount.Text = count.ToString();
            }
            conn.Close();

            conn.Open();
            NpgsqlCommand statistics2 = new NpgsqlCommand($"SELECT SUM(mistakes) FROM lesson_progress WHERE user_id = {user_id};", conn);
            reader = statistics2.ExecuteReader();
            if (reader.Read())
            {
                int count = reader.GetInt32(0);
                labelMistakesCount.Text = count.ToString();
            }
            conn.Close();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(email);
            settings.FormClosed += (s, args) =>
            {
                Account();

                this.Show();
            };
            settings.ShowDialog();
        }
    }
}
