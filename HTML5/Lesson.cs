using Newtonsoft.Json;
using Npgsql;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;

namespace HTML5
{
    public partial class Lesson : Form
    {
        private int lesson_id;
        private int user_id;
        public Lesson(int lesson_id, int user_id)
        {
            InitializeComponent();
            this.lesson_id = lesson_id;
            this.user_id = user_id;

            CenterForm();

            ConnectDB();

            LessonContent();

            this.FormClosing += Lesson_Close;
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

        private void LessonContent()
        {
            using (NpgsqlConnection conn = ConnectDB())
            {         
                NpgsqlCommand searchLesson = new NpgsqlCommand($"SELECT * FROM lesson WHERE lesson_id = {lesson_id}", conn);
                NpgsqlDataReader reader = searchLesson.ExecuteReader();
                if (reader.Read())
                {
                    Label heading = new Label();
                    heading.AutoSize = true;
                    heading.Dock = DockStyle.Top;
                    heading.Text = reader.GetString(1);
                    heading.Font = new Font("Open Sans, Bold", 32, FontStyle.Bold);
                    heading.Padding = new Padding(0, 10, 5, 10);
                    this.Controls.Add(heading);

                    string content = reader.GetString(2);
                    Regex regex = new Regex("<(\\w+).*?>(.*?)</\\1>");
                    MatchCollection matches = regex.Matches(content);
                    int count = regex.Matches(content).Count;
                    Control control;
                    bool isCode = false; 
                    foreach (Match match in matches)
                    {
                        string tagName = match.Groups[1].Value;
                        string tagContent = match.Groups[2].Value;

                        string formattedText = tagContent.Replace("<br>", "\n").Replace("&nbsp;", " ");
                        tagContent = formattedText;

                        Label tagLabel = new Label();
                        tagLabel.AutoSize = true;
                        tagLabel.Dock = DockStyle.Top;
                        tagLabel.MaximumSize = new Size(1000, 0);


                        GradientPanel codePanel = new GradientPanel();
                        codePanel.Dock = DockStyle.Top;
                        codePanel.BackColor = Color.FromArgb(45, 45, 45);
                        codePanel.GradientBottomColor = Color.FromArgb(45, 45, 45);
                        codePanel.GradientTopColor = Color.FromArgb(45, 45, 45);

                        LinkLabel linkLabel = new LinkLabel();

                        switch (tagName)
                        {
                            case "h1":
                                tagLabel.Font = new Font("Open Sans, Bold", 32, FontStyle.Bold);
                                tagLabel.Padding = new Padding(0, 10, 5, 10);
                                break;
                            case "h2":
                                tagLabel.Font = new Font("Open Sans, Bold", 24, FontStyle.Bold);
                                tagLabel.Padding = new Padding(0, 10, 5, 10);
                                break;
                            case "h3":
                                tagLabel.Font = new Font("Open Sans", 18);
                                tagLabel.Padding = new Padding(0, 0, 5, 0);
                                break;
                            case "code":
                                isCode = true;

                                Button copy = new Button();
                                copy.Text = "Copy";
                                copy.ForeColor = Color.White;
                                copy.AutoSize = false;
                                copy.Location = new Point(900, 10);

                                Label codeLabel = new Label();
                                codeLabel.AutoSize = true;
                                codeLabel.ForeColor = Color.White;
                                codeLabel.Location = new Point(codeLabel.Location.X + 10, codeLabel.Location.Y + 10);
                                codeLabel.BackColor = codePanel.BackColor;
                                codeLabel.Font = new Font("Open Sans", 12);
                                codeLabel.Text = tagContent;

                                copy.Click += (sender, e) =>
                                {
                                    Clipboard.SetText(codeLabel.Text);
                                };

                                Size textSize = TextRenderer.MeasureText(tagContent, codeLabel.Font);
                                Size newSize = new Size(textSize.Width, textSize.Height + 20);
                                codePanel.Size = newSize;
                                //codePanel.AutoScroll = true;

                                codePanel.Controls.Add(copy);
                                codePanel.Controls.Add(codeLabel);
                                this.Controls.Add(codePanel);

                                tagLabel = codeLabel;
                                break;
                            case "li":
                                tagLabel.Font = new Font("Open Sans", 18, FontStyle.Regular);
                                tagLabel.Padding = new Padding(30, 0, 5, 0);
                                tagLabel.Text += "•  ";
                                break;
                            case "a":
                                linkLabel.AutoSize = true;
                                linkLabel.Dock = DockStyle.Top;
                                linkLabel.Font = new Font("Open Sans", 18, FontStyle.Regular);
                                linkLabel.Padding = new Padding(10, 0, 5, 0);
                                linkLabel.LinkClicked += (sender, e) =>
                                {
                                    System.Diagnostics.Process.Start(tagContent);
                                };
                                this.Controls.Add(linkLabel);
                                tagLabel = linkLabel;
                                break;
                            case "test":
                                Label labelTest = new Label();
                                labelTest.Text = "Start the test";
                                labelTest.Font = new Font("Open Sans", 18, FontStyle.Bold);
                                labelTest.BackColor = Color.FromArgb(45, 45, 45);
                                labelTest.ForeColor = Color.White;
                                labelTest.Dock = DockStyle.Bottom;
                                labelTest.TextAlign = ContentAlignment.MiddleCenter;
                                labelTest.Height = 50;
                                labelTest.Cursor = Cursors.Hand;

                                labelTest.Click += (sender, e) =>
                                {
                                    Test test = new Test(lesson_id, user_id);
                                    test.ShowDialog();
                                };

                                this.Controls.Add(labelTest);
                                break;
                            default:
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
                            tagLabel.Text += tagContent;
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

        private void Lesson_Close(object sender, FormClosingEventArgs e)
        {
            string json = File.ReadAllText("..\\..\\config.json");
            Dictionary<string, object> positions = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
            string lessonId = lesson_id.ToString();
            var data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(json);
            var winformsData = data[lessonId];
            int current = winformsData["CurrentY"];

            if (positions.TryGetValue(lessonId, out object valuesObj))
            {
                var values = (dynamic)valuesObj;
                int currentY = this.VerticalScroll.Value + 560;

                if (currentY > current)
                {
                    values.CurrentY = this.VerticalScroll.Value + 560;  
                }
                values.MaxY = this.VerticalScroll.Maximum;
            }
            else
            {
                positions.Add(lessonId, new { CurrentY = this.VerticalScroll.Value + 560, MaxY = this.VerticalScroll.Maximum });
            }

            string newJson = JsonConvert.SerializeObject(positions, Formatting.Indented);
            File.WriteAllText("..\\..\\config.json", newJson);

            json = File.ReadAllText("..\\..\\config.json");
            data = JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, int>>>(json);
            winformsData = data[lessonId];
            current = winformsData["CurrentY"];
            int maxY = winformsData["MaxY"];

            double result = Math.Round(((double)current / maxY) * 50);

            NpgsqlConnection conn = ConnectDB();
            {
                bool isTest = false;

                NpgsqlCommand answer = new NpgsqlCommand($"SELECT passed_the_test FROM lesson_progress WHERE lesson_id = {lesson_id} and user_id = {user_id};", conn);
                NpgsqlDataReader reader1 = answer.ExecuteReader();
                if (reader1.Read())
                {
                    isTest = true;
                }

                if (!isTest)
                {
                    NpgsqlCommand getResult = new NpgsqlCommand($"SELECT progress FROM lesson_progress WHERE user_id = {user_id} AND lesson_id = {lesson_id}", conn);
                    int progress = getResult.ExecuteNonQuery();

                    if (progress < result)
                    {
                        NpgsqlCommand update = new NpgsqlCommand($"UPDATE lesson_progress SET progress = {result} WHERE user_id = {user_id} AND lesson_id = {lesson_id}", conn);
                        update.ExecuteNonQuery();
                    }
                }
            }
            //Application.Restart();
            //Environment.Exit(0);
        }
    }
}
