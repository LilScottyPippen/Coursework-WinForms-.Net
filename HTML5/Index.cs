using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HTML5
{
    public partial class Index : Form
    {
        private string email;
        private string password;
        public Index(string email, string password)
        {
            InitializeComponent();
            this.email = email;
            this.password = password;

            this.FormClosed += (s, args) => Application.Exit();

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
            DotNetEnv.Env.Load("..\\..\\.env");
            string pass = Environment.GetEnvironmentVariable("DB_PASS");

            NpgsqlConnection conn = new NpgsqlConnection($"Server=localhost;Database=html5;User Id=postgres;Password={pass}");

            conn.Open();
            return conn;
        }

        private void IndexContent()
        {
            using (NpgsqlConnection conn = ConnectDB())
            {
                NpgsqlCommand countCmd = new NpgsqlCommand("SELECT COUNT(*) FROM lesson", conn);
                int lessonCount = Convert.ToInt32(countCmd.ExecuteScalar());
                for (int i = 1; i <= lessonCount; i++)
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
            lesson.ShowDialog();
        }

        private void labelButton2_Click(object sender, EventArgs e)
        {
            Lesson lesson = new Lesson(2);
            lesson.ShowDialog();
        }

        private void labelButton3_Click(object sender, EventArgs e)
        {
            Lesson lesson = new Lesson(3);
            lesson.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            Settings settings = new Settings(email);
            settings.ShowDialog();
        }
    }
}
