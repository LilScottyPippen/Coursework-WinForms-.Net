using Npgsql;
using System;
using System.Windows.Forms;

namespace HTML5
{
    public class AuthManager
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool Login()
        {
            bool isAuth = false;

            DotNetEnv.Env.Load("..\\..\\.env");
            string pass = Environment.GetEnvironmentVariable("DB_PASS");

            using (NpgsqlConnection conn = new NpgsqlConnection($"Server=localhost;Database=html5;User Id=postgres;Password={pass}"))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM users WHERE email = '{Email}' AND password = '{Password}'", conn))
                {
                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isAuth = true;
                        }
                        else
                        {
                            MessageBox.Show("Invalid email or password!");
                        }
                    }
                }
            }

            return isAuth;
        }
    }

}
