using System;
using System.Drawing;
using System.Net.Mail;
using System.Net;
using System.Windows.Forms;
using DotNetEnv;
using Npgsql;

namespace HTML5
{
    public partial class Registration : Form
    {
        private int count;
        private int code;
        private string email;
        private string password;

        public Registration()
        {
            InitializeComponent();
            Login login = new Login();
            this.FormClosed += (s, args) => login.Show();

            CenterForm();
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
            Env.Load("..\\..\\.env");
            string pass = Environment.GetEnvironmentVariable("DB_PASS");

            NpgsqlConnection conn = new NpgsqlConnection($"Server=localhost;Database=html5;User Id=postgres;Password={pass}");

            conn.Open();
            return conn;
        }

        private void labelConfirm_Click(object sender, EventArgs e)
        {

            if (count == 0)
            {
                try
                {
                    DotNetEnv.Env.Load("..\\..\\.env");
                    string EmailSender = Environment.GetEnvironmentVariable("EMAIL_SENDER");
                    string password = Environment.GetEnvironmentVariable("PASSWORD_SENDER");

                    Random rand = new Random();

                    email = textBox1.Text;
                    code = rand.Next(99999, 1000000);
                    var fromAddress = new MailAddress(EmailSender, "Confirm email");
                    var toAddress = new MailAddress(email, "HTML5");
                    const string subject = "Email verification code";
                    var body = "Verification code: " + code;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(fromAddress.Address, password)
                    };
                    using (var message = new MailMessage(fromAddress, toAddress)
                    {
                        Subject = subject,
                        Body = body
                    })
                    {
                        smtp.Send(message);
                    }

                    gradientPanel1.GradientBottomColor = Color.FromArgb(116, 209, 157);
                    gradientPanel1.GradientTopColor = Color.FromArgb(116, 209, 157);

                    gradientPanel2.GradientBottomColor = Color.White;
                    gradientPanel2.GradientTopColor = Color.White;

                    label1.Text = "Confirm Email";
                    label1.Location = new Point(160, 66);
                    textBox1.Text = "";
                    MessageBox.Show(code.ToString());
                    count++;
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
            
            else if(count == 1)
            {
                if (textBox1.Text == code.ToString())
                {
                    label1.Text = "Enter password";
                    label1.Location = new Point(145, 66);
                    textBox1.Text = "";

                    gradientPanel4.Location = new Point(226, 240);

                    textBox2.Visible = true;
                    gradientPanel5.Visible = true;

                    gradientPanel2.GradientBottomColor = Color.FromArgb(116, 209, 157);
                    gradientPanel2.GradientTopColor = Color.FromArgb(116, 209, 157);

                    gradientPanel3.GradientBottomColor = Color.White;
                    gradientPanel3.GradientTopColor = Color.White;
                    count++;
                }
                else
                {
                    MessageBox.Show("Invalid code!");
                }
            }
            else if (count == 2) {
                if (textBox1.Text == textBox2.Text && textBox1.Text.Length > 7)
                {
                    password = textBox1.Text;
                    using (NpgsqlConnection conn = ConnectDB())
                    {
                        NpgsqlCommand cmd = new NpgsqlCommand($"INSERT INTO users (email, password) VALUES ('{email}', '{password}')", conn);
                        NpgsqlDataReader reader = cmd.ExecuteReader();
                        MessageBox.Show("OK");
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid password!");
                }
            }
        }
    }
}
