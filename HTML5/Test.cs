using Npgsql;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace HTML5
{
    public partial class Test : Form
    {
        private int lesson_id;
        private int user_id;
        public Test(int lesson_id, int user_id)
        {
            this.lesson_id = lesson_id;
            this.user_id = user_id;

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
                code.Dock = DockStyle.Fill;
                code.BackColor = Color.FromArgb(45, 45, 45);
                code.BorderStyle = BorderStyle.None;
                code.ForeColor = Color.White;
                code.Margin = new Padding(10);

                GradientPanel innerPanel = new GradientPanel();
                innerPanel.Dock = DockStyle.Fill;
                innerPanel.BackColor = Color.FromArgb(45, 45, 45);
                innerPanel.Controls.Add(code);

                GradientPanel outerPanel = new GradientPanel();
                outerPanel.Dock = DockStyle.Top;
                outerPanel.Padding = new Padding(10);
                outerPanel.GradientBottomColor = Color.FromArgb(45, 45, 45);
                outerPanel.GradientTopColor = Color.FromArgb(45, 45, 45);
                outerPanel.Controls.Add(innerPanel);

                Control lastControl = this.GetChildAtPoint(new Point(this.Width / 2, this.Height / 2), GetChildAtPointSkip.Invisible);
                this.Controls.Add(outerPanel);
                this.Controls.SetChildIndex(outerPanel, this.Controls.IndexOf(lastControl) + 1);
            }



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
                bool isAnswer = false;
                conn.Open();
                NpgsqlCommand answer = new NpgsqlCommand($"SELECT answer FROM tests WHERE lesson_id = {lesson_id}", conn);
                NpgsqlDataReader reader1 = answer.ExecuteReader();
                if (reader1.Read())
                {
                    if (code.Text == reader1.GetString(0))
                    {
                        isAnswer = true;
                        MessageBox.Show("OK");
                    }
                    else
                    {
                        MessageBox.Show("Error");
                    }
                }
                reader1.Close();
                conn.Close();

                if (isAnswer)
                {
                    try
                    {
                        conn.Open();
                        NpgsqlCommand updateProgress = new NpgsqlCommand($"UPDATE lesson_progress SET progress = progress + 50 WHERE lesson_id = {lesson_id} and user_id = {user_id}", conn);
                        updateProgress.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                    conn.Close();

                    try
                    {
                        conn.Open();
                        NpgsqlCommand updatePassedTest = new NpgsqlCommand($"UPDATE lesson_progress SET passed_the_test = true WHERE lesson_id = {lesson_id} and user_id = {user_id}", conn);
                        updatePassedTest.ExecuteNonQuery();
                        this.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}");
                    }
                }
            };

            this.Controls.Add(labelTest);
            
        }
    }
}
