using Npgsql;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.Text.RegularExpressions;
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

            CenterForm();

            ConnectDB();

            LessonContent();
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

        private void LessonContent()
        {
            using (NpgsqlConnection conn = ConnectDB())
            {
                NpgsqlCommand searchLesson = new NpgsqlCommand($"SELECT * FROM lesson WHERE lesson_id = {lesson_id}", conn);
                NpgsqlDataReader reader = searchLesson.ExecuteReader();
                if (reader.Read())
                {
                    label1.Text = reader.GetString(1);
                    string content = reader.GetString(2);
                    Regex regex = new Regex("<(\\w+).*?>(.*?)</\\1>");
                    MatchCollection matches = regex.Matches(content);
                    Control control;
                    bool isCode = true; 
                    foreach (Match match in matches)
                    {
                        string tagName = match.Groups[1].Value;
                        string tagContent = match.Groups[2].Value;

                        Label tagLabel = new Label();
                        tagLabel.AutoSize = true;
                        tagLabel.Dock = DockStyle.Top;


                        GradientPanel codePanel = new GradientPanel();
                        codePanel.Dock = DockStyle.Top;
                        codePanel.BackColor = Color.FromArgb(45, 45, 45);
                        codePanel.GradientBottomColor = Color.FromArgb(45, 45, 45);
                        codePanel.GradientTopColor = Color.FromArgb(45, 45, 45);

                        switch (tagName)
                        {
                            case "h1":
                                isCode = false;
                                tagLabel.Font = new Font("Open Sans, Bold", 32, FontStyle.Bold);
                                tagLabel.Padding = new Padding(5, 0, 5, 0);
                                break;
                            case "h2":
                                isCode = false;
                                tagLabel.Font = new Font("Open Sans, Bold", 24, FontStyle.Bold);
                                tagLabel.Padding = new Padding(8, 0, 5, 0);
                                break;
                            case "h3":
                                isCode = false;
                                tagLabel.Font = new Font("Open Sans, Bold", 18, FontStyle.Bold);
                                tagLabel.Padding = new Padding(10, 0, 5, 0);
                                break;
                            case "code":
                                isCode = true;
                                Label codeLabel = new Label();
                                codeLabel.AutoSize = true;
                                codeLabel.ForeColor = Color.White;
                                codeLabel.Location = new Point(codeLabel.Location.X + 10, codeLabel.Location.Y + 10);
                                codeLabel.BackColor = codePanel.BackColor;
                                codeLabel.Text = tagContent;

                                codePanel.Controls.Add(codeLabel);
                                this.Controls.Add(codePanel);

                                tagLabel = codeLabel;
                                break;
                            default:
                                MessageBox.Show("default");
                                control = new Label();
                                control.Text = tagContent;
                                break;
                        }

                        if (isCode)
                        {
                            control = codePanel;
                            isCode = false;
                        }
                        else
                        {
                            tagLabel.Text = tagContent;
                            control = tagLabel;
                        }

                        Control lastControl = this.GetChildAtPoint(new Point(this.Width / 2, this.Height / 2), GetChildAtPointSkip.Invisible);
                        this.Controls.Add(control);
                        this.Controls.SetChildIndex(control, this.Controls.IndexOf(lastControl) + 1);
                    }
                }
                reader.Close();
            }
        }
    }
}
