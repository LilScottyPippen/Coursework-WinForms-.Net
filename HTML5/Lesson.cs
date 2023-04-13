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

            NpgsqlCommand searchLesson = new NpgsqlCommand($"SELECT * FROM lesson WHERE lesson_id = {lesson_id}", conn);
            NpgsqlDataReader reader = searchLesson.ExecuteReader();
            if (reader.Read())
            {
                label1.Text = reader.GetString(1);
                string content = reader.GetString(2);

                int startIndex = 0;
                while (true)
                {
                    // Ищем следующий тег
                    int startTagIndex = content.IndexOf("<", startIndex);
                    if (startTagIndex == -1)
                    {
                        break;
                    }

                    int endTagIndex = content.IndexOf(">", startTagIndex);
                    if (endTagIndex == -1)
                    {
                        break;
                    }

                    // Получаем имя тега
                    string tagName = content.Substring(startTagIndex + 1, endTagIndex - startTagIndex - 1);

                    // Создаем элемент управления в зависимости от типа тега
                    Control control;
                    switch (tagName)
                    {
                        case "h1":
                            Label headerLabel = new Label();
                            headerLabel.Font = new Font("Open Sans, Bold", 16, FontStyle.Bold);
                            headerLabel.Dock = DockStyle.Top;
                            headerLabel.AutoSize = true;
                            control = headerLabel;
                            break;
                        case "p":
                            Label paragraphLabel = new Label();
                            paragraphLabel.Font = new Font("Arial", 12, FontStyle.Regular);
                            paragraphLabel.ForeColor = Color.Black;
                            paragraphLabel.Dock = DockStyle.Top;
                            control = paragraphLabel;
                            break;
                        // Добавляем обработку других типов тегов, если необходимо
                        default:
                            startIndex = endTagIndex + 1;
                            continue;
                    }

                    // Получаем содержимое тега и устанавливаем его свойства элемента управления
                    string tagContent = content.Substring(endTagIndex + 1);
                    int endTagContentIndex = tagContent.IndexOf("<");
                    if (endTagContentIndex == -1)
                    {
                        endTagContentIndex = tagContent.Length;
                    }
                    else
                    {
                        tagContent = tagContent.Substring(0, endTagContentIndex);
                    }

                    // Устанавливаем свойства элемента управления на основе содержимого тега
                    switch (tagName)
                    {
                        case "h1":
                        case "p":
                            Label label = (Label)control;
                            label.Text = tagContent;
                            break;
                        // Добавляем обработку других типов тегов, если необходимо
                        default:
                            break;
                    }

                    // Добавляем новый элемент после последнего элемента управления
                    Control lastControl = this.GetChildAtPoint(new Point(this.Width / 2, this.Height / 2), GetChildAtPointSkip.Invisible);
                    this.Controls.Add(control);
                    this.Controls.SetChildIndex(control, this.Controls.IndexOf(lastControl) + 1);

                    // Переходим к следующему тегу
                    startIndex = endTagIndex + 1;
                }
            }
            reader.Close();
        }
    }
}
