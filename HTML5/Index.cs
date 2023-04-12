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

            int centerX = Screen.PrimaryScreen.WorkingArea.Width / 2;
            int centerY = Screen.PrimaryScreen.WorkingArea.Height / 2;

            int formX = this.Width / 2;
            int formY = this.Height / 2;

            this.StartPosition = FormStartPosition.Manual;
            this.Location = new Point(centerX - formX, centerY - formY);

            DotNetEnv.Env.Load("D:\\C# Курсач\\Приложение\\HTML5\\HTML5\\.env");
            string pass = Environment.GetEnvironmentVariable("DB_PASS");

            NpgsqlConnection conn = new NpgsqlConnection($"Server=localhost;Database=html5;User Id=postgres;Password={pass}");

            try
            {
                conn.Open();
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
            catch (Exception ex){
               MessageBox.Show(ex.Message);
            }
        }

        private void buttonArtan1_Click(object sender, EventArgs e)
        {
            Lesson lesson = new Lesson();
            this.Hide();
            lesson.Show();  
        }
    }
}
