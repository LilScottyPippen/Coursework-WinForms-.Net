using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTML5
{
    public partial class Lesson : Form
    {
        public int lesson_id { get; set; }
        public Lesson(int lesson_id)
        {
            InitializeComponent();
            this.lesson_id = lesson_id;

            //Centering
            int centerX = Screen.PrimaryScreen.WorkingArea.Width / 2;
            int centerY = Screen.PrimaryScreen.WorkingArea.Height / 2;

            int formX = this.Width / 2;
            int formY = this.Height / 2;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(centerX - formX, centerY - formY);
            //

            //Variable environment
            DotNetEnv.Env.Load("D:\\C# Курсач\\Приложение\\HTML5\\HTML5\\.env");
            string pass = Environment.GetEnvironmentVariable("DB_PASS");
            //

            //Connect PostgreSQL
            NpgsqlConnection conn = new NpgsqlConnection($"Server=localhost;Database=html5;User Id=postgres;Password={pass}");
            //

            conn.Open();

            NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM lesson WHERE lesson_id = {lesson_id}", conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                label1.Text = reader.GetString(1);
            }
            reader.Close();
        }
    }
}
