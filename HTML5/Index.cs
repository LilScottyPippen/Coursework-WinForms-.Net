using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DotNetEnv;
using Npgsql;

namespace HTML5
{
    public partial class Index : Form
    {
        public Index()
        {

            InitializeComponent();

            CenterForm();

            IndexContent();
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
            DotNetEnv.Env.Load("D:\\C# Курсач\\Приложение\\HTML5\\HTML5\\.env");
            string pass = Environment.GetEnvironmentVariable("DB_PASS");

            NpgsqlConnection conn = new NpgsqlConnection($"Server=localhost;Database=html5;User Id=postgres;Password={pass}");

            conn.Open();
            return conn;
        }

        private void IndexContent() {
            using (NpgsqlConnection conn = ConnectDB())
            {
                for (int i = 1; i <= 100; i++)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM lesson WHERE lesson_id = {i}", conn);
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        Label label = (Label)this.Controls.Find($"labelLesson{i}", true)[0];
                        label.Text = reader.GetString(1);
                    }
                    reader.Close();
                }
            }
        }

        private void labelButton1_Click(object sender, EventArgs e)
        {
            Lesson lesson = new Lesson(1);
            lesson.Show();
        }

        private void labelButton2_Click(object sender, EventArgs e)
        {
            Lesson lesson = new Lesson(2);
            lesson.Show();
        }
    }
}
