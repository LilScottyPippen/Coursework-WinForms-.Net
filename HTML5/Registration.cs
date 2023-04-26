using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HTML5
{
    public partial class Registration : Form
    {
        private int count;
        private int code;
        public Registration()
        {
            InitializeComponent();
        }

        private void labelConfirm_Click(object sender, EventArgs e)
        {

            if (count == 0)
            {
                count++;
                gradientPanel1.GradientBottomColor = Color.FromArgb(116, 209, 157);
                gradientPanel1.GradientTopColor = Color.FromArgb(116, 209, 157);

                gradientPanel2.GradientBottomColor = Color.White;
                gradientPanel2.GradientTopColor = Color.White;

                DotNetEnv.Env.Load("..\\..\\.env");
                string EmailSender = Environment.GetEnvironmentVariable("EMAIL_SENDER");
                string password = Environment.GetEnvironmentVariable("PASSWORD_SENDER");

                Random rand = new Random();

                string email = textBoxEmail.Text;
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

                label1.Text = "Confirm Email";
                textBoxEmail.Text = "";
                MessageBox.Show(code.ToString());
            }
            
            else if(count == 1)
            {
                if (textBoxEmail.Text == code.ToString())
                {
                    label1.Text = "Enter password";
                    textBoxEmail.Text = "";

                    gradientPanel2.GradientBottomColor = Color.FromArgb(116, 209, 157);
                    gradientPanel2.GradientTopColor = Color.FromArgb(116, 209, 157);

                    gradientPanel3.GradientBottomColor = Color.White;
                    gradientPanel3.GradientTopColor = Color.White;
                }
                else
                {
                    MessageBox.Show("Invalid code!");
                }
            }
        }
    }
}
