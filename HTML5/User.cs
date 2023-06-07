using DotNetEnv;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Gmail.v1;
using Google.Apis.Services;
using Google.Apis.Util;
using Npgsql;
using System;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HTML5
{
    public class AuthManager
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public string Token { get; set; }

        public bool Login()
        {
            bool isAuth = false;

            Env.Load("..\\..\\.env");
            string pass = Environment.GetEnvironmentVariable("DB_PASS");

            byte[] messageBytes = Encoding.UTF8.GetBytes(Password);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashValue = sha256.ComputeHash(messageBytes);
                string hashString = BitConverter.ToString(hashValue).Replace("-", string.Empty);

                Password = hashString;
            }

            using (NpgsqlConnection conn = new NpgsqlConnection($"Server=localhost;Database=html5;User Id=postgres;Password={pass}"))
            {
                conn.Open();
                using (NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM users WHERE email = '{Email}' AND password = '{Password}' AND GoogleAuth = false", conn))
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

        public bool GoogleAuth()
        {
            string clientId = Env.GetString("CLIENT_ID");
            string clientSecret = Env.GetString("CLIENT_SECRET");

            string[] scopes = { GmailService.Scope.GmailReadonly };
            var credentials = GoogleWebAuthorizationBroker.AuthorizeAsync(
                new ClientSecrets
                {
                    ClientId = clientId,
                    ClientSecret = clientSecret,
                },
                scopes, "user", CancellationToken.None).Result;

            if (credentials.Token.IsExpired(SystemClock.Default))
                credentials.RefreshTokenAsync(CancellationToken.None).Wait();

            var service = new GmailService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credentials
            });
            var userInfoRequest = service.Users.GetProfile("me");
            var userInfo = userInfoRequest.Execute();
            string userEmail = userInfo.EmailAddress;

            var lines = File.ReadAllLines("..\\..\\.env");

            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].StartsWith("EMAIL="))
                    lines[i] = $"EMAIL={userEmail}";
                if (lines[i].StartsWith("GOOGLE_AUTH="))
                    lines[i] = "GOOGLE_AUTH=true";
                if (lines[i].StartsWith("TOKEN="))
                    lines[i] = $"TOKEN={credentials.Token.AccessToken}";
            }
            File.WriteAllLines("..\\..\\.env", lines);

            Email = userEmail;
            Token = credentials.Token.AccessToken;

            bool isAuth = false;
            Env.Load("..\\..\\.env");
            string pass = Environment.GetEnvironmentVariable("DB_PASS");

            NpgsqlConnection conn = new NpgsqlConnection($"Server=localhost;Database=html5;User Id=postgres;Password={pass}");
            conn.Open();
            NpgsqlCommand cmd = new NpgsqlCommand($"SELECT * FROM users WHERE email = '{Email}' AND password = '' AND GoogleAuth = true", conn);
            NpgsqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                isAuth = true;
            }
            else
            {
                reader.Close();
                NpgsqlCommand userReg = new NpgsqlCommand($"INSERT INTO users (email, password, googleauth) VALUES ('{Email}', '', 'true') RETURNING user_id", conn);
                int userId = (int)userReg.ExecuteScalar();

                NpgsqlCommand googleReg = new NpgsqlCommand($"INSERT INTO google_credentials (user_id, email, access_token) VALUES ({userId}, '{Email}', '{Token}')", conn);
                googleReg.ExecuteNonQuery();

                isAuth = true;
            }
            return isAuth;
        }
    }

}
