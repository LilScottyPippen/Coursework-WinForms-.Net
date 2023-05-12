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
            NpgsqlConnection conn = ConnectDB();

            NpgsqlCommand searchTest = new NpgsqlCommand($"SELECT * FROM tests WHERE lesson_id = {lesson_id}", conn);
            NpgsqlDataReader reader = searchTest.ExecuteReader();

            Control control;
            RichTextBox code = new RichTextBox();
            if (reader.Read())
            {
                Label label = new Label();
                label.Text = reader.GetString(2);
                label.AutoSize = true;
                label.Font = new Font("Open Sans", 15, FontStyle.Bold);
                label.Dock = DockStyle.Top;
                this.Controls.Add(label);

                code.Text = reader.GetString(4);
                code.Font = new Font("Open Sans", 10);
                code.Dock = DockStyle.Top;
                code.Height = 300;
            }
            control = code;
            Control lastControl = this.GetChildAtPoint(new Point(this.Width / 2, this.Height / 2), GetChildAtPointSkip.Invisible);
            this.Controls.Add(control);
            this.Controls.SetChildIndex(control, this.Controls.IndexOf(lastControl) + 1);

            Label labelTest = new Label();
            labelTest.Text = "End the test";
            labelTest.Font = new Font("Open Sans", 18, FontStyle.Bold);
            labelTest.BackColor = Color.FromArgb(45, 45, 45);
            labelTest.ForeColor = Color.White;
            labelTest.Dock = DockStyle.Bottom;
            labelTest.TextAlign = ContentAlignment.MiddleCenter;
            labelTest.Height = 50;
            labelTest.Cursor = Cursors.Hand;

            conn.Close();

            labelTest.Click += (sender, e) =>
            {
                conn.Open();
                NpgsqlCommand answer = new NpgsqlCommand($"SELECT answer FROM tests WHERE lesson_id = {lesson_id}", conn);
                NpgsqlDataReader reader1 = answer.ExecuteReader();
                if (reader1.Read())
                {
                    if (code.Text == reader1.GetString(0)) {
                        MessageBox.Show("OK");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
                conn.Close();
            };

            this.Controls.Add(labelTest);
            
        }
    }
}
