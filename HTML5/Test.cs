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
    public partial class Test : Form
    {
        private int lesson_id;
        public Test(int lesson_id)
        {
            this.lesson_id = lesson_id;

            InitializeComponent();

            CenterForm();

            ConnectDB();

            TestContent();
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

        private void TestContent()
        {
            using (NpgsqlConnection conn = ConnectDB())
            {
                NpgsqlCommand searchTest = new NpgsqlCommand($"SELECT * FROM tests WHERE lesson_id = {lesson_id}", conn);
                NpgsqlDataReader reader = searchTest.ExecuteReader();
                if (reader.Read())
                {
                    Label label = new Label();
                    label.Text = reader.GetString(2);
                    label.AutoSize = true;
                    label.Font = new Font("Open Sans", 10, FontStyle.Bold);
                    label.Dock = DockStyle.Top;
                    this.Controls.Add(label);

                    RichTextBox code = new RichTextBox();
                    code.Text = reader.GetString(3);
                    code.Size = new Size(100, 100);
                    code.Dock = DockStyle.Top;
                    this.Controls.Add(code);
                }
            }
        }
    }
}
